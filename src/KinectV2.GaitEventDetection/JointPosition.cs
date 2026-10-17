// Copyright (c) Amin Amini.
// SPDX-License-Identifier: Apache-2.0
// Associated publication: https://doi.org/10.1109/BHI.2017.7897228

namespace KinectV2.GaitEventDetection
{
    public struct JointPosition
    {
        public JointPosition(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double X { get; private set; }

        public double Y { get; private set; }

        public double Z { get; private set; }
    }
}
