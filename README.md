# Dreamine.Gem.Abstractions

[![CI](https://github.com/CodeMaru-Dreamine/Dreamine.Gem.Abstractions/actions/workflows/ci.yml/badge.svg)](https://github.com/CodeMaru-Dreamine/Dreamine.Gem.Abstractions/actions/workflows/ci.yml)
[![Quality Gate](https://sonarcloud.io/api/project_badges/measure?project=CodeMaru-Dreamine_Dreamine.Gem.Abstractions&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=CodeMaru-Dreamine_Dreamine.Gem.Abstractions) [![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=CodeMaru-Dreamine_Dreamine.Gem.Abstractions&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=CodeMaru-Dreamine_Dreamine.Gem.Abstractions) [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=CodeMaru-Dreamine_Dreamine.Gem.Abstractions&metric=coverage)](https://sonarcloud.io/summary/new_code?id=CodeMaru-Dreamine_Dreamine.Gem.Abstractions)
[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/CodeMaru-Dreamine/Dreamine.Gem.Abstractions/blob/main/LICENSE) [![.NET 8](https://img.shields.io/badge/.NET-8-512BD4.svg?logo=dotnet)](https://dotnet.microsoft.com/download/dotnet/8.0) [![NuGet](https://img.shields.io/nuget/v/Dreamine.Gem.Abstractions?logo=nuget&label=nuget)](https://www.nuget.org/packages/Dreamine.Gem.Abstractions) [![NuGet downloads](https://img.shields.io/nuget/dt/Dreamine.Gem.Abstractions?logo=nuget&label=downloads)](https://www.nuget.org/packages/Dreamine.Gem.Abstractions) [![Docs](https://img.shields.io/badge/Docs-README-2496ED.svg)](https://github.com/CodeMaru-Dreamine/Dreamine.Gem.Abstractions#readme)

Provider-neutral GEM contracts and immutable domain models for Dreamine.

[➡️ 한국어 문서 보기](https://github.com/CodeMaru-Dreamine/Dreamine.Gem.Abstractions/blob/main/README_KO.md)

## Install

```powershell
dotnet add package Dreamine.Gem.Abstractions
```

Choose this package for provider-neutral GEM contracts and immutable models. Applications that need the typed runtime and the implemented E30-derived dialogue subset should start with [`Dreamine.Gem`](https://www.nuget.org/packages/Dreamine.Gem).

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

See [the public API review](https://github.com/CodeMaru-Dreamine/Dreamine.Gem.Abstractions/blob/main/docs/API_REVIEW.md) and the generated
[public API inventory](https://github.com/CodeMaru-Dreamine/Dreamine.Gem.Abstractions/blob/main/docs/PUBLIC_API.md).

## Versioning note

`1.0.1` is the current stabilization release. Consume it together with the
matching `Dreamine.Gem` `1.0.1` package, and clear older local-feed artifacts
from the package cache before validating the published pair.

## License

MIT.
