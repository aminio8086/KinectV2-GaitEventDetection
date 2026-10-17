// Copyright (c) Amin Amini.
// SPDX-License-Identifier: Apache-2.0
// Author: Amin Amini
// Associated publication: https://doi.org/10.1109/BHI.2017.7897228

using System;

namespace KinectV2.GaitEventDetection
{
    /// <summary>
    /// Implements the ankle-height comparison method used in the 2017 study.
    /// </summary>
    public sealed class AnkleHeightGaitEventDetector
    {
        public const double HistoricalDefaultThresholdMetres = 0.14;

        public static readonly TimeSpan MinimumOffDuration =
            TimeSpan.FromMilliseconds(250.0);

        private readonly LegState _leftState = new LegState();
        private readonly LegState _rightState = new LegState();

        public AnkleHeightGaitEventDetector()
            : this(HistoricalDefaultThresholdMetres)
        {
        }

        public AnkleHeightGaitEventDetector(double ankleHeightThresholdMetres)
        {
            if (ankleHeightThresholdMetres <= 0.0)
            {
                throw new ArgumentOutOfRangeException(
                    "ankleHeightThresholdMetres",
                    "The ankle-height threshold must be greater than zero.");
            }

            AnkleHeightThresholdMetres = ankleHeightThresholdMetres;
        }

        public double AnkleHeightThresholdMetres { get; private set; }

        public GaitEvent Update(
            Leg leg,
            JointPosition ankle,
            FloorPlane floorPlane,
            TimeSpan timestamp)
        {
            double distanceMetres =
                GaitGeometry.CalculateDistanceToFloor(ankle, floorPlane);

            LegState state = GetState(leg);

            if (!state.IsFootOff)
            {
                if (distanceMetres >= AnkleHeightThresholdMetres)
                {
                    state.IsFootOff = true;
                    state.FootOffStartedAt = timestamp;

                    return new GaitEvent(
                        leg,
                        GaitEventType.FootOff,
                        timestamp,
                        distanceMetres);
                }

                return null;
            }

            if (distanceMetres < AnkleHeightThresholdMetres)
            {
                TimeSpan offDuration = timestamp - state.FootOffStartedAt;
                state.IsFootOff = false;

                if (offDuration > MinimumOffDuration)
                {
                    return new GaitEvent(
                        leg,
                        GaitEventType.FootContact,
                        timestamp,
                        distanceMetres);
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
