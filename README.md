# Dreamine.Gem.Abstractions

Provider-neutral GEM contracts and immutable domain models for Dreamine.

[➡️ 한국어 문서 보기](README_KO.md)

The package defines the SECS message-transport boundary, state and service
contracts, and immutable values used by `Dreamine.Gem`. It depends on
`Dreamine.Secs.Abstractions`; it does not depend on a concrete HSMS provider or
on the GEM implementation package.

## Profile-ready contracts

The additive profile surface provides:

- typed variable, equipment-constant, alarm, report, collection-event, remote-
  command, process-program, clock, and spool models;
- independent U1/U2/U4/U8 policies for CEID, RPTID, VID, SVID, DVID, ECID,
  ALID, and DATAID;
- immutable profile definitions, snapshots, ordered report values, atomic
  equipment-constant result models, and event-configuration result models;
- typed remote-command parameter definitions and context-independent handlers.

These are contracts and domain values. They do not by themselves enable a
wire dialogue or claim support for every GEM capability.

## Evidence boundary

| Scope | Status | Meaning |
|---|---|---|
| Public contract surface | `IMPLEMENTED_UNVERIFIED` | The additive source surface is available; release-package and external interoperability evidence remain separate. |
| Local unit evidence for immutable models and validation | `PASS` | Local automated tests cover the profile-model rules; this is not field evidence. |
| External simulator or production equipment | `NOT_RUN` | No external result is inferred from contract tests. |
| E37.1 conformance | `BLOCKED_STANDARD` | The required licensed revision is unavailable; this package makes no E37.1 claim. |

The target sources for the implementation package are E30-0611 and E5-0813.
They are not represented as current-revision conformance, certification, or
interoperability evidence.

See [the public API review](docs/API_REVIEW.md) and the generated
[public API inventory](docs/PUBLIC_API.md).

## Versioning note

`1.0.0` is the initial public package version. Consume it together with the
matching `Dreamine.Gem` `1.0.0` package, and clear older local-feed artifacts
from the package cache before validating the published pair.

## License

MIT.
