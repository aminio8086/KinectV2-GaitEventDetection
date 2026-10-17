# KinectV2 Gait Event Detection

Reference C# implementation of the gait-event detection methods associated with:

> A. Amini, K. Banitsas and S. Hosseinzadeh,  
> **"A New Technique for Foot-Off and Foot Contact Detection in a Gait Cycle Based on the Knee Joint Angle Using Microsoft Kinect v2"**,  
> 2017 IEEE EMBS International Conference on Biomedical & Health Informatics (BHI), pp. 153–156.  
> https://doi.org/10.1109/BHI.2017.7897228

This repository is a cleaned, focused archival release reconstructed from the original research source code. It separates the paper's **proposed knee-angle method** from the **ankle-height comparison method** instead of carrying unrelated later Parkinson's, freezing-of-gait, fall-detection, laser-control or networking features.

## Methods included

### Knee-angle method

The proposed method uses the relative 3-D angle formed by the hip, knee and ankle joints.

The recovered implementation uses:

- knee angle threshold: **170 degrees**;
- minimum off-phase duration before accepting foot contact: **more than 200 ms**.

A straight leg is approximately 180 degrees. A foot-off phase begins when the knee angle drops to or below the threshold. A foot contact is accepted when the leg returns above the threshold after more than 200 ms.

### Ankle-height baseline

The comparison method uses the ankle joint's perpendicular distance from the Kinect floor plane.

The historical comparison implementation uses:

- default ankle-height threshold: **0.14 m**;
- minimum off-phase duration before accepting foot contact: **more than 250 ms**.

The threshold is configurable because the paper describes the ankle method relative to a pre-measured ankle height.

## Repository structure

```text
src/
└── KinectV2.GaitEventDetection/
    ├── GaitGeometry.cs
    ├── GaitEvent.cs
    ├── GaitEventType.cs
    ├── JointPosition.cs
    ├── FloorPlane.cs
    ├── Leg.cs
    ├── KneeAngleGaitEventDetector.cs
    └── AnkleHeightGaitEventDetector.cs

tests/
└── KinectV2.GaitEventDetection.SmokeTests/
    └── Program.cs

docs/
├── algorithm.md
└── provenance.md
```

## Design of this release

The historical research code was embedded in larger WPF/Kinect applications. This release extracts the two paper-specific algorithms into small deterministic classes so that the scientific logic can be inspected and tested without requiring a Kinect sensor.

The algorithms accept joint coordinates and floor-plane values as inputs. A Kinect v2 acquisition layer can supply those values directly from `Microsoft.Kinect.Joint.Position` and `BodyFrame.FloorClipPlane`.

This separation intentionally keeps the reference implementation focused on the published gait-event methodology.

## Building

The solution targets **.NET Framework 4.6**, matching the generation of Visual C#/Kinect v2 tooling used by the original research.

Open:

```text
KinectV2-GaitEventDetection.sln
```

or build with MSBuild from a Visual Studio Developer Command Prompt.

See [BUILDING.md](BUILDING.md).

## Validation

A small dependency-free smoke-test executable is included. It checks:

- straight-leg angle geometry;
- knee-angle foot-off and foot-contact transitions;
- 200 ms rejection/acceptance behaviour;
- ankle-to-floor distance calculation;
- 250 ms ankle-baseline timing.

The release still requires hardware validation if it is connected to a live Kinect v2 acquisition application.

## Historical source provenance

The paper-specific logic was recovered from two private historical repositories:

- `Kinect4FOG_Original_Private` — knee-angle gait-event logic later reused in the larger Kinect4FOG project.
- `KinectHeightBasedStepDetect` — ankle-height/floor-distance comparison implementation.

See [docs/provenance.md](docs/provenance.md).

## Citation

If you use this software in academic work, please cite the associated paper:

> Amini, A., Banitsas, K. and Hosseinzadeh, S. (2017).  
> "A New Technique for Foot-Off and Foot Contact Detection in a Gait Cycle Based on the Knee Joint Angle Using Microsoft Kinect v2."  
> *2017 IEEE EMBS International Conference on Biomedical & Health Informatics (BHI)*, 153–156.  
> https://doi.org/10.1109/BHI.2017.7897228

A machine-readable `CITATION.cff` is included.

## Licence

Copyright (c) Amin Amini.

The cleaned reference implementation is distributed under the Apache License 2.0. See [LICENSE](LICENSE) and [NOTICE](NOTICE).

The associated IEEE paper is a separate copyrighted publication and is **not** redistributed in this repository.
