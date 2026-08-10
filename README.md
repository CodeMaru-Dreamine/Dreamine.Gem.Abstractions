# Dreamine.Gem.Abstractions

Provider-neutral GEM contracts and immutable domain models for Dreamine.

[➡️ 한국어 문서 보기](https://github.com/CodeMaru-Dreamine/Dreamine.Gem.Abstractions/blob/main/README_KO.md)

The package defines a SECS message transport boundary, typed variable,
constant, event/report, alarm, remote-command, process-program, clock, and
spool service contracts, state enums, and immutable definitions. It depends on
`Dreamine.Secs.Abstractions`, not on a concrete HSMS implementation.

The contracts do not claim that an implementation supports every GEM message
scenario or conforms to a specific SEMI revision.

## License

MIT.
