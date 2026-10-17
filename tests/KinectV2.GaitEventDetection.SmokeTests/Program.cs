// Copyright (c) Amin Amini.
// SPDX-License-Identifier: Apache-2.0
// Associated publication: https://doi.org/10.1109/BHI.2017.7897228

using System;

namespace KinectV2.GaitEventDetection.SmokeTests
{
    internal static class Program
    {
        private static int Main()
        {
            try
            {
                TestStraightLegAngle();
                TestKneeAngleEvents();
                TestKneeAngleShortEventRejected();
                TestAnkleDistance();
                TestAnkleHeightEvents();

                Console.WriteLine("All smoke tests passed.");
                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return 1;
            }
        }

        private static void TestStraightLegAngle()
        {
            JointPosition hip = new JointPosition(0.0, 1.0, 0.0);
            JointPosition knee = new JointPosition(0.0, 0.5, 0.0);
            JointPosition ankle = new JointPosition(0.0, 0.0, 0.0);

            double angle =
                GaitGeometry.CalculateKneeAngleDegrees(hip, knee, ankle);

            AssertNear(180.0, angle, 0.0001, "Straight-leg angle");
        }

        private static void TestKneeAngleEvents()
        {
            KneeAngleGaitEventDetector detector =
                new KneeAngleGaitEventDetector();

            JointPosition hip = new JointPosition(0.0, 1.0, 0.0);
            JointPosition knee = new JointPosition(0.0, 0.5, 0.0);
            JointPosition straightAnkle = new JointPosition(0.0, 0.0, 0.0);
            JointPosition bentAnkle = new JointPosition(0.25, 0.1, 0.0);

            AssertNull(
                detector.Update(
                    Leg.Left,
                    hip,
                    knee,
                    straightAnkle,
                    TimeSpan.Zero),
                "Straight leg should not trigger foot-off.");

            GaitEvent footOff =
                detector.Update(
                    Leg.Left,
                    hip,
                    knee,
                    bentAnkle,
                    TimeSpan.FromMilliseconds(100));

            AssertEvent(
                footOff,
                GaitEventType.FootOff,
                "Bent knee should trigger foot-off.");

            GaitEvent footContact =
                detector.Update(
                    Leg.Left,
                    hip,
                    knee,
                    straightAnkle,
                    TimeSpan.FromMilliseconds(350));

            AssertEvent(
                footContact,
                GaitEventType.FootContact,
                "Return after more than 200 ms should trigger foot contact.");
        }

        private static void TestKneeAngleShortEventRejected()
        {
            KneeAngleGaitEventDetector detector =
                new KneeAngleGaitEventDetector();

            JointPosition hip = new JointPosition(0.0, 1.0, 0.0);
            JointPosition knee = new JointPosition(0.0, 0.5, 0.0);
            JointPosition straightAnkle = new JointPosition(0.0, 0.0, 0.0);
            JointPosition bentAnkle = new JointPosition(0.25, 0.1, 0.0);

            detector.Update(
                Leg.Right,
                hip,
                knee,
                bentAnkle,
                TimeSpan.FromMilliseconds(100));

            AssertNull(
                detector.Update(
                    Leg.Right,
                    hip,
                    knee,
                    straightAnkle,
                    TimeSpan.FromMilliseconds(250)),
                "Return within 200 ms should be rejected.");
        }

        private static void TestAnkleDistance()
        {
            FloorPlane floor = new FloorPlane(0.0, 1.0, 0.0, 0.0);
            JointPosition ankle = new JointPosition(0.2, 0.14, 2.0);

            double distance =
                GaitGeometry.CalculateDistanceToFloor(ankle, floor);

            AssertNear(0.14, distance, 0.0001, "Ankle-to-floor distance");
        }

        private static void TestAnkleHeightEvents()
        {
            AnkleHeightGaitEventDetector detector =
                new AnkleHeightGaitEventDetector();

            FloorPlane floor = new FloorPlane(0.0, 1.0, 0.0, 0.0);

            AssertNull(
                detector.Update(
                    Leg.Left,
                    new JointPosition(0.0, 0.10, 0.0),
                    floor,
                    TimeSpan.Zero),
                "Low ankle height should not trigger foot-off.");

            GaitEvent footOff =
                detector.Update(
                    Leg.Left,
                    new JointPosition(0.0, 0.16, 0.0),
                    floor,
                    TimeSpan.FromMilliseconds(100));

            AssertEvent(
                footOff,
                GaitEventType.FootOff,
                "Raised ankle should trigger foot-off.");

            GaitEvent footContact =
                detector.Update(
                    Leg.Left,
                    new JointPosition(0.0, 0.10, 0.0),
                    floor,
                    TimeSpan.FromMilliseconds(400));

            AssertEvent(
                footContact,
                GaitEventType.FootContact,
                "Return after more than 250 ms should trigger foot contact.");
        }

        private static void AssertEvent(
            GaitEvent actual,
            GaitEventType expectedType,
            string message)
        {
            if (actual == null || actual.EventType != expectedType)
            {
                throw new InvalidOperationException(message);
            }
        }

        private static void AssertNull(object value, string message)
        {
            if (value != null)
            {
                throw new InvalidOperationException(message);
            }
        }

        private static void AssertNear(
            double expected,
            double actual,
            double tolerance,
            string message)
        {
            if (Math.Abs(expected - actual) > tolerance)
            {
                throw new InvalidOperationException(
                    message + ": expected " + expected + ", actual " + actual + ".");
            }
        }
    }
}
