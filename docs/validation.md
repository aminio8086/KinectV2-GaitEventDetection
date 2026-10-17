# Validation status

This release separates three kinds of evidence: publication evidence, historical source-code evidence and release-level static checks.

## Publication-confirmed behaviour

The associated BHI 2017 paper states that:

- foot-off is identified when the knee angle decreases past the experimentally selected 170-degree threshold;
- foot contact is accepted when the same leg returns after **more than 200 ms**;
- the comparison method uses ankle distance to the ground;
- the comparison method uses a delay of **more than 250 ms**;
- the proposed knee-angle method was evaluated against the ankle-distance method.

Associated publication:

https://doi.org/10.1109/BHI.2017.7897228

## Historical source-code evidence

The recovered knee-angle implementation contains:

- a 170-degree active knee-angle threshold;
- a 200 ms active minimum timing threshold;
- Hip/Knee/Ankle vector construction;
- left and right knee-angle calculation;
- step counting on return above the knee-angle threshold.

The recovered ankle-height comparison implementation contains:

- an active 0.14 m ankle-height threshold;
- an active 250 ms minimum timing threshold;
- Kinect floor-plane coefficients;
- ankle-to-floor distance calculation;
- independent left and right timing/counters.

The historical ankle source also declared 0.115 m and 900 ms constants, but those values were not used by the active gait-analysis methods and are intentionally excluded from the cleaned reference implementation.

## Release-level checks

The cleaned release includes smoke tests for:

- straight-leg geometry;
- knee-angle foot-off transition;
- knee-angle foot-contact transition;
- rejection at exactly 200 ms;
- acceptance after more than 200 ms;
- ankle-to-floor distance;
- ankle-height foot-off transition;
- ankle-height foot-contact transition;
- rejection at exactly 250 ms;
- acceptance after more than 250 ms.

Project-reference and source-path checks have also been performed on the release branch.

## Not yet validated

The cleaned reference library has not been run against live Microsoft Kinect v2 hardware in this release environment.

A formal hardware-validation pass should therefore still confirm:

- live Kinect v2 joint acquisition;
- mapping of Kinect floor-plane coefficients;
- expected event timing at approximately 30 frames per second;
- behaviour with occluded or low-confidence lower-body joints.

The paper reports the original experimental validation; this repository does not claim to reproduce those experiments automatically.
