# Algorithm correspondence

## Proposed knee-angle method

For each leg, let:

- `H` be the hip position;
- `K` be the knee position;
- `A` be the ankle position.

The recovered research implementation calculates the internal knee angle using the two vectors that meet at the knee:

```text
v1 = K - H
v2 = K - A
theta = acos((v1 · v2) / (|v1| |v2|))
```

This orientation produces approximately 180 degrees for a straight leg, matching the behaviour described in the paper.

The event state machine uses the published values:

- foot-off phase begins when `theta <= 170°`;
- when the angle returns above 170°, a foot contact is accepted only if the off phase lasted **more than 200 ms**.

The historical source implemented this timing with `Stopwatch` objects. The cleaned reference accepts timestamps, which makes the same state transition deterministic and testable.

## Ankle-height comparison method

The comparison method calculates the perpendicular distance from the ankle point `(x, y, z)` to the Kinect floor plane:

```text
distance = |A*x + B*y + C*z + D| / sqrt(A² + B² + C²)
```

The historical comparison source used a 0.14 m ankle-height threshold and a minimum duration of more than 250 ms.

Because the paper describes the comparison against a pre-measured ankle height, the cleaned implementation makes the ankle threshold configurable while preserving 0.14 m as the historical default.

## Scope

The cleaned classes implement the gait-event algorithms only. They do not include unrelated later research functionality such as freezing-of-gait classification, fall alerts, laser cueing, Arduino control or remote monitoring.
