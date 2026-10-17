// Copyright (c) Amin Amini.
// SPDX-License-Identifier: Apache-2.0
// Associated publication: https://doi.org/10.1109/BHI.2017.7897228

using System;

namespace KinectV2.GaitEventDetection
{
    public static class GaitGeometry
    {
        public static double CalculateKneeAngleDegrees(
            JointPosition hip,
            JointPosition knee,
            JointPosition ankle)
        {
            double firstX = knee.X - hip.X;
            double firstY = knee.Y - hip.Y;
            double firstZ = knee.Z - hip.Z;

            double secondX = knee.X - ankle.X;
            double secondY = knee.Y - ankle.Y;
            double secondZ = knee.Z - ankle.Z;

            double firstMagnitude = Math.Sqrt(
                firstX * firstX +
                firstY * firstY +
                firstZ * firstZ);

            double secondMagnitude = Math.Sqrt(
                secondX * secondX +
                secondY * secondY +
                secondZ * secondZ);

            if (firstMagnitude == 0.0 || secondMagnitude == 0.0)
            {
                throw new ArgumentException(
                    "Hip, knee and ankle positions must define two non-zero vectors.");
            }

            double cosine =
                (firstX * secondX + firstY * secondY + firstZ * secondZ) /
                (firstMagnitude * secondMagnitude);

            // Protect acos from small floating-point excursions outside [-1, 1].
            cosine = Math.Max(-1.0, Math.Min(1.0, cosine));

            return Math.Acos(cosine) * (180.0 / Math.PI);
        }

        public static double CalculateDistanceToFloor(
            JointPosition joint,
            FloorPlane floorPlane)
        {
            double denominator = Math.Sqrt(
                floorPlane.A * floorPlane.A +
                floorPlane.B * floorPlane.B +
                floorPlane.C * floorPlane.C);

            if (denominator == 0.0)
            {
                throw new ArgumentException(
                    "The floor-plane normal vector must be non-zero.",
                    "floorPlane");
            }

            double numerator =
                floorPlane.A * joint.X +
                floorPlane.B * joint.Y +
                floorPlane.C * joint.Z +
                floorPlane.D;

            return numerator / denominator;
        }
    }
}
