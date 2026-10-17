// Copyright (c) Amin Amini.
// SPDX-License-Identifier: Apache-2.0
// Author: Amin Amini
// Associated publication: https://doi.org/10.1109/BHI.2017.7897228

using System;

namespace KinectV2.GaitEventDetection
{
    /// <summary>
    /// Implements the knee-angle gait-event method described in the 2017 paper.
    /// </summary>
    public sealed class KneeAngleGaitEventDetector
    {
        public const double KneeAngleThresholdDegrees = 170.0;

        public static readonly TimeSpan MinimumOffDuration =
            TimeSpan.FromMilliseconds(200.0);

        private readonly LegState _leftState = new LegState();
        private readonly LegState _rightState = new LegState();

        public GaitEvent Update(
            Leg leg,
            JointPosition hip,
            JointPosition knee,
            JointPosition ankle,
            TimeSpan timestamp)
        {
            double kneeAngleDegrees =
                GaitGeometry.CalculateKneeAngleDegrees(hip, knee, ankle);

            LegState state = GetState(leg);

            if (!state.IsFootOff)
            {
                if (kneeAngleDegrees <= KneeAngleThresholdDegrees)
                {
                    state.IsFootOff = true;
                    state.FootOffStartedAt = timestamp;

                    return new GaitEvent(
                        leg,
                        GaitEventType.FootOff,
                        timestamp,
                        kneeAngleDegrees);
                }

                return null;
            }

            if (kneeAngleDegrees > KneeAngleThresholdDegrees)
            {
                TimeSpan offDuration = timestamp - state.FootOffStartedAt;
                state.IsFootOff = false;

                if (offDuration > MinimumOffDuration)
                {
                    return new GaitEvent(
                        leg,
                        GaitEventType.FootContact,
                        timestamp,
                        kneeAngleDegrees);
                }
            }

            return null;
        }

        public void Reset()
        {
            _leftState.Reset();
            _rightState.Reset();
        }

        private LegState GetState(Leg leg)
        {
            return leg == Leg.Left ? _leftState : _rightState;
        }

        private sealed class LegState
        {
            public bool IsFootOff { get; set; }

            public TimeSpan FootOffStartedAt { get; set; }

            public void Reset()
            {
                IsFootOff = false;
                FootOffStartedAt = TimeSpan.Zero;
            }
        }
    }
}
