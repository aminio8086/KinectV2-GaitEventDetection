# Building

## Requirements

- Windows, Linux or macOS with a compiler capable of targeting .NET Framework 4.6, or Visual Studio on Windows.
- For live Kinect v2 integration: Windows plus Microsoft Kinect for Windows SDK v2.

The reference algorithm library itself has **no Kinect SDK dependency**.

## Visual Studio

1. Open `KinectV2-GaitEventDetection.sln`.
2. Build the solution in Debug or Release mode.
3. Run `KinectV2.GaitEventDetection.SmokeTests`.

## Live Kinect v2 integration

Map Kinect values into the reference types:

- `Microsoft.Kinect.CameraSpacePoint` → `JointPosition`
- `BodyFrame.FloorClipPlane` → `FloorPlane`

For the knee-angle method, provide Hip, Knee and Ankle positions for each leg.

For the ankle-height baseline, provide the Ankle position plus the current Kinect floor plane.

The reference code intentionally keeps sensor acquisition separate from event-detection logic.
