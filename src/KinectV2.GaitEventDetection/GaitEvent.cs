// Copyright (c) Amin Amini.
// SPDX-License-Identifier: Apache-2.0
// Associated publication: https://doi.org/10.1109/BHI.2017.7897228

using System;

namespace KinectV2.GaitEventDetection
{
    public sealed class GaitEvent
    {
        public GaitEvent(
            Leg leg,
            GaitEventType eventType,
            TimeSpan timestamp,
            double measurement)
        {
            Leg = leg;
            EventType = eventType;
            Timestamp = timestamp;
            Measurement = measurement;
        }

        public Leg Leg { get; private set; }

        public GaitEventType EventType { get; private set; }

        public TimeSpan Timestamp { get; private set; }

        public double Measurement { get; private set; }
    }
}
