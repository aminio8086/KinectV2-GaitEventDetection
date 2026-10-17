# Historical source provenance

This repository was reconstructed from the author's private historical research code rather than from repository-name similarity alone.

## Proposed knee-angle method

Recovered from:

```text
Kinect4FOG_Original_Private/
└── Detectors/
    └── FOGDetector.xaml.cs
```

Relevant historical elements included:

- left and right knee-angle variables;
- Hip/Knee/Ankle vector construction;
- vector-angle calculation;
- 170-degree threshold;
- 200 ms minimum stride/off-phase timing;
- left/right step counters.

The same gait-event logic was later incorporated into the broader Kinect4FOG research application.

## Ankle-height comparison method

Recovered from:

```text
KinectHeightBasedStepDetect/
└── KinectHeightBasedStepDetect/
    └── Detectors/
        └── StepCounter.xaml.cs
```

Relevant historical elements included:

- Kinect floor-plane coefficients;
- ankle-to-plane distance calculation;
- 0.14 m active distance threshold;
- 250 ms minimum timing;
- left/right original and corrected ankle-distance counters.

The historical source also contained experimental distance-correction code and extensive data-recording/UI scaffolding. Those prototype concerns are intentionally excluded from the focused reference implementation.

## Refactoring policy

The cleaned release:

- preserves published thresholds and equations;
- preserves the active historical comparison threshold;
- separates scientific logic from WPF UI and Kinect acquisition;
- uses explicit units in names;
- removes dead/commented prototype code;
- does not silently add later Kinect4FOG algorithms to the 2017 method.
