# Changelog

All notable changes to this cleaned research release will be documented here.

## [Unreleased]

### Added

- Focused reference implementation of the published knee-angle gait-event method.
- Reference implementation of the ankle-height comparison method.
- 170-degree knee threshold and 200 ms timing from the published method.
- 250 ms timing for the ankle-height comparison method.
- Configurable ankle-height threshold with the historical 0.14 m default.
- Deterministic geometry helpers independent of the Kinect SDK.
- Dependency-free smoke tests.
- Build, algorithm and provenance documentation.
- Apache-2.0 licensing and machine-readable citation metadata.

### Changed

- Extracted paper-specific algorithms from larger historical WPF research applications.
- Replaced prototype-era names and UI coupling with descriptive, testable classes.
- Removed unrelated FOG, fall-detection, laser-control, serial, UDP and Office/Excel functionality.
