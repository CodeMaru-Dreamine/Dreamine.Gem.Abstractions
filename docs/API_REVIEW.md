# Public API review

Review date: 2026-08-12. Comparison baseline: the checked-in `1.0.0` source.
The release-reflection inventory is generated separately in
[PUBLIC_API.md](PUBLIC_API.md).

## Result

- Reference direction remains
  `Gem.Abstractions -> Secs.Abstractions`; no GEM implementation or concrete
  HSMS type is exposed by this assembly.
- The change is additive at source level. New profile primitives include
  `GemProfileCapability`, `GemIdentifierFamily`,
  `GemIdentifierFormatOverride`, and `GemIdentifierFormatPolicy`.
- New typed definitions include `GemVariableProfileDefinition`,
  `GemEquipmentConstantProfileDefinition`,
  `GemRemoteCommandParameterDefinition`,
  `GemRemoteCommandProfileDefinition`, and `GemRemoteCommandProfileEntry`.
- New operational result and snapshot models cover atomic equipment-constant
  updates, alarm state, collection-event configuration, event/report links,
  and ordered variable/report values.
- `GemEventSnapshot` adds an ordered report-value constructor and `Reports`
  view while retaining the existing value-dictionary constructor and `Values`
  view.
- Collection-bearing constructors snapshot input and expose read-only views.
  Delegates remain explicit application boundaries; they are not wire-support
  claims.

No final exported-type count is recorded here. `PUBLIC_API.md` is the release-
assembly reflection authority and is regenerated after the implementation is
frozen.

## Status and evidence

| Review item | Status | Evidence boundary |
|---|---|---|
| Additive public contract surface | `IMPLEMENTED_UNVERIFIED` | Source diff reviewed; final package consumption remains a separate gate. |
| Local model validation and immutability tests | `PASS` | Automated local tests only. |
| External simulator or production equipment | `NOT_RUN` | Not implied by API review. |
| E37.1 conformance | `BLOCKED_STANDARD` | Required licensed source is unavailable. |

## Version-pair risk

Both `Dreamine.Gem.Abstractions` and its consuming `Dreamine.Gem` project still
declare version `1.0.0`, even though their working public surfaces are additive.
Publishing or smoke-testing either candidate with a reused `1.0.0` identity can
resolve an older assembly from a NuGet cache and produce a mismatched pair. Use
one unique candidate version for both packages, a clean package cache, and a
local-feed-only consumer smoke before any publication decision.

## Next-version proposals

| Classification | Proposal | Reason |
|---|---|---|
| Source- and binary-breaking | Replace the ambiguous spool enqueue `bool` with a result enum. | A Boolean cannot distinguish acceptance, overwrite, disabled state, and rejection. |
| Source- and binary-breaking | Add explicit runtime ownership/disposal semantics to `IGemRuntime`. | Transport ownership remains caller-managed and is not expressible by the current interface. |
| Non-breaking candidate | Add capability descriptors to service contracts. | A domain service contract must not imply a corresponding wire mapping. |

Thread-safe registries protect their own state. User callbacks receive immutable
or read-only snapshots where defined, but callback side effects and multi-reader
physical sampling remain application responsibilities.
