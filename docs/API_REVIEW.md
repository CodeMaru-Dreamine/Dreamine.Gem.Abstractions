# Public API review

Review date: 2026-08-10. Baseline: 1.0.0 source. See [PUBLIC_API.md](PUBLIC_API.md).

## Result

- Reference direction is `Gem.Abstractions -> Secs.Abstractions`; no GEM or HSMS implementation type is exposed and no cycle exists.
- Definitions are immutable and collection-bearing constructors snapshot their input.
- Async service boundaries use `Async` names and accept `CancellationToken`; time-sensitive services expose a clock/transport boundary.
- No public signature or binary surface changed in this pass.

## Next-version proposals

| Classification | Proposal | Reason |
|---|---|---|
| Source- and binary-breaking | Replace ambiguous spool enqueue `bool` with a result enum | The current value means overwrite/acceptance only by documentation. |
| Source- and binary-breaking | Define explicit runtime ownership/disposal semantics in `IGemRuntime` | The current runtime does not own its supplied transport, but the interface cannot communicate that lifecycle. |
| Non-breaking candidate | Add service capability descriptors | A service contract does not mean every corresponding wire message is mapped. |

Collection-event snapshots are stable with respect to report links and variable definitions; external variable readers cannot be sampled as one physical atomic transaction.
