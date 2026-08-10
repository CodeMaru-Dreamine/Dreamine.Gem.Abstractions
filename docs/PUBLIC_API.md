# Public API Inventory

Assembly: `Dreamine.Gem.Abstractions`

This inventory is generated from the compiled Release assembly. It is an audit artifact, not an additional compatibility promise.

Exported types: **29**

## Types

### `public interface Dreamine.Gem.Abstractions.Interfaces.IGemAlarmService`

- `Dreamine.Gem.Abstractions.States.GemAlarmChangeStatus ChangeAlarm(System.UInt64 id, System.Boolean isSet)`
- `System.Boolean SetAlarm(System.UInt64 id, System.Boolean isSet)`
- `System.Boolean SetEnabled(System.UInt64 id, System.Boolean enabled)`
- `System.Boolean TryGetState(System.UInt64 id, System.Boolean& isSet)`
- `System.Collections.Generic.IReadOnlyList<System.UInt64> GetSetAlarmIds()`
- `System.Void Register(Dreamine.Gem.Abstractions.Model.GemAlarmDefinition definition)`

### `public interface Dreamine.Gem.Abstractions.Interfaces.IGemClockService`

- `System.DateTimeOffset GetUtcNow()`
- `System.String Format(System.Boolean fourDigitYear)`
- `System.Void SetUtcNow(System.DateTimeOffset value)`

### `public interface Dreamine.Gem.Abstractions.Interfaces.IGemEquipmentConstantService`

- `Dreamine.Gem.Abstractions.States.GemConstantSetStatus SetValue(System.UInt64 id, Dreamine.Secs.Abstractions.Model.SecsItem value, Dreamine.Gem.Abstractions.States.GemControlState controlState)`
- `System.Boolean TryGetValue(System.UInt64 id, Dreamine.Secs.Abstractions.Model.SecsItem& value)`
- `System.Boolean TrySetValue(System.UInt64 id, Dreamine.Secs.Abstractions.Model.SecsItem value)`
- `System.Void Register(Dreamine.Gem.Abstractions.Model.GemEquipmentConstantDefinition definition, System.Func<Dreamine.Secs.Abstractions.Model.SecsItem, System.Boolean> validator, System.Func<Dreamine.Gem.Abstractions.States.GemControlState, System.Boolean> statePolicy)`

### `public interface Dreamine.Gem.Abstractions.Interfaces.IGemEventReportService`

- `System.Boolean DeleteReport(System.UInt64 reportId)`
- `System.Boolean LinkReport(System.UInt64 eventId, System.UInt64 reportId)`
- `System.Boolean SetEnabled(System.UInt64 eventId, System.Boolean enabled)`
- `System.Boolean UnlinkReport(System.UInt64 eventId, System.UInt64 reportId)`
- `System.Threading.Tasks.ValueTask<Dreamine.Gem.Abstractions.Model.GemEventSnapshot> CollectAsync(System.UInt64 eventId, System.Threading.CancellationToken cancellationToken)`
- `System.Void DefineEvent(Dreamine.Gem.Abstractions.Model.GemCollectionEventDefinition collectionEvent)`
- `System.Void DefineReport(Dreamine.Gem.Abstractions.Model.GemReportDefinition report)`

### `public interface Dreamine.Gem.Abstractions.Interfaces.IGemMessageTransport`

- `Dreamine.Secs.Abstractions.Interfaces.ISecsConnection Connection { get; }`
- `Dreamine.Secs.Abstractions.Model.SecsSessionId SessionId { get; }`
- `Dreamine.Secs.Abstractions.Model.SecsSystemBytes AllocateSystemBytes()`
- `System.Threading.Tasks.Task SendAsync(Dreamine.Secs.Abstractions.Model.SecsMessage message, System.Threading.CancellationToken cancellationToken)`
- `System.Threading.Tasks.Task<Dreamine.Secs.Abstractions.Model.SecsMessage> RequestAsync(Dreamine.Secs.Abstractions.Model.SecsMessage message, System.Threading.CancellationToken cancellationToken)`
- `event System.EventHandler<Dreamine.Secs.Abstractions.Model.SecsMessage> MessageReceived`

### `public interface Dreamine.Gem.Abstractions.Interfaces.IGemProcessProgramService`

- `System.Boolean Delete(System.String id)`
- `System.Boolean TryGet(System.String id, Dreamine.Gem.Abstractions.Model.GemProcessProgram& program)`
- `System.Collections.Generic.IReadOnlyList<System.String> GetIds()`
- `System.Void Put(Dreamine.Gem.Abstractions.Model.GemProcessProgram program)`

### `public interface Dreamine.Gem.Abstractions.Interfaces.IGemRemoteCommandService`

- `System.Threading.Tasks.ValueTask<Dreamine.Gem.Abstractions.Model.GemCommandResult> ExecuteAsync(System.String name, System.Collections.Generic.IReadOnlyDictionary<System.String, Dreamine.Secs.Abstractions.Model.SecsItem> parameters, System.TimeSpan timeout, System.Threading.CancellationToken cancellationToken)`
- `System.Void Register(Dreamine.Gem.Abstractions.Model.GemRemoteCommandDefinition definition, System.Func<System.Collections.Generic.IReadOnlyDictionary<System.String, Dreamine.Secs.Abstractions.Model.SecsItem>, System.Threading.CancellationToken, System.Threading.Tasks.ValueTask<Dreamine.Gem.Abstractions.Model.GemCommandResult>> handler)`

### `public interface Dreamine.Gem.Abstractions.Interfaces.IGemRuntime`

- `Dreamine.Secs.Abstractions.Interfaces.ISecsConnection SecsConnection { get; }`

### `public interface Dreamine.Gem.Abstractions.Interfaces.IGemSpoolService`

- `Dreamine.Gem.Abstractions.States.GemSpoolState State { get; }`
- `System.Boolean Enqueue(Dreamine.Secs.Abstractions.Model.SecsMessage message)`
- `System.Int32 Count { get; }`
- `System.Threading.Tasks.Task DrainAsync(System.Func<Dreamine.Secs.Abstractions.Model.SecsMessage, System.Threading.CancellationToken, System.Threading.Tasks.Task> sender, System.Threading.CancellationToken cancellationToken)`
- `System.Void Purge()`
- `System.Void Start()`

### `public interface Dreamine.Gem.Abstractions.Interfaces.IGemVariableCatalog`

- `System.Boolean TryGetDefinition(System.UInt64 id, Dreamine.Gem.Abstractions.Model.GemVariableDefinition& definition)`
- `System.Collections.Generic.IReadOnlyList<Dreamine.Gem.Abstractions.Model.GemVariableDefinition> GetDefinitions(System.Nullable<Dreamine.Gem.Abstractions.States.GemVariableKind> kind)`
- `System.Threading.Tasks.ValueTask<Dreamine.Secs.Abstractions.Model.SecsItem> ReadAsync(System.UInt64 id, System.Threading.CancellationToken cancellationToken)`
- `System.Void Register(Dreamine.Gem.Abstractions.Model.GemVariableDefinition definition, System.Func<System.Threading.CancellationToken, System.Threading.Tasks.ValueTask<Dreamine.Secs.Abstractions.Model.SecsItem>> reader)`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemAlarmDefinition`

- `GemAlarmDefinition(System.UInt64 id, System.Byte code, System.String text, System.Boolean enabled)`
- `System.Boolean Enabled { get; }`
- `System.Byte Code { get; }`
- `System.String Text { get; }`
- `System.UInt64 Id { get; }`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemCollectionEventDefinition`

- `GemCollectionEventDefinition(System.UInt64 id, System.String name, System.Collections.Generic.IEnumerable<System.UInt64> reportIds, System.Boolean enabled)`
- `System.Boolean Enabled { get; }`
- `System.Collections.Generic.IReadOnlyList<System.UInt64> ReportIds { get; }`
- `System.String Name { get; }`
- `System.UInt64 Id { get; }`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemCommandResult`

- `Dreamine.Gem.Abstractions.States.GemCommandStatus Status { get; }`
- `GemCommandResult(Dreamine.Gem.Abstractions.States.GemCommandStatus status, System.String detail)`
- `System.String Detail { get; }`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemEquipmentConstantDefinition`

- `Dreamine.Secs.Abstractions.Model.SecsItem DefaultValue { get; }`
- `Dreamine.Secs.Abstractions.Model.SecsItem MaximumValue { get; }`
- `Dreamine.Secs.Abstractions.Model.SecsItem MinimumValue { get; }`
- `GemEquipmentConstantDefinition(System.UInt64 id, System.String name, Dreamine.Secs.Abstractions.Model.SecsItem defaultValue, System.String description, System.String units, Dreamine.Secs.Abstractions.Model.SecsItem minimumValue, Dreamine.Secs.Abstractions.Model.SecsItem maximumValue)`
- `System.String Description { get; }`
- `System.String Name { get; }`
- `System.String Units { get; }`
- `System.UInt64 Id { get; }`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemEquipmentIdentity`

- `GemEquipmentIdentity(System.String modelNumber, System.String softwareRevision)`
- `System.String ModelNumber { get; }`
- `System.String SoftwareRevision { get; }`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemEventSnapshot`

- `GemEventSnapshot(System.UInt64 eventId, System.DateTimeOffset occurredAt, System.Collections.Generic.IDictionary<System.UInt64, Dreamine.Secs.Abstractions.Model.SecsItem> values)`
- `System.Collections.Generic.IReadOnlyDictionary<System.UInt64, Dreamine.Secs.Abstractions.Model.SecsItem> Values { get; }`
- `System.DateTimeOffset OccurredAt { get; }`
- `System.UInt64 EventId { get; }`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemProcessProgram`

- `GemProcessProgram(System.String id, System.ReadOnlySpan<System.Byte> body)`
- `System.ReadOnlyMemory<System.Byte> Body { get; }`
- `System.String Id { get; }`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemRemoteCommandDefinition`

- `GemRemoteCommandDefinition(System.String name, System.Collections.Generic.IEnumerable<System.String> parameters)`
- `System.Collections.Generic.IReadOnlyList<System.String> Parameters { get; }`
- `System.String Name { get; }`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemReportDefinition`

- `GemReportDefinition(System.UInt64 id, System.Collections.Generic.IEnumerable<System.UInt64> variableIds)`
- `System.Collections.Generic.IReadOnlyList<System.UInt64> VariableIds { get; }`
- `System.UInt64 Id { get; }`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemVariableDefinition`

- `Dreamine.Gem.Abstractions.States.GemVariableKind Kind { get; }`
- `GemVariableDefinition(System.UInt64 id, System.String name, Dreamine.Gem.Abstractions.States.GemVariableKind kind, System.String description, System.String units)`
- `System.String Description { get; }`
- `System.String Name { get; }`
- `System.String Units { get; }`
- `System.UInt64 Id { get; }`

### `public enum Dreamine.Gem.Abstractions.States.GemAlarmChangeStatus`

- `const Dreamine.Gem.Abstractions.States.GemAlarmChangeStatus Changed = 0`
- `const Dreamine.Gem.Abstractions.States.GemAlarmChangeStatus NoChange = 1`
- `const Dreamine.Gem.Abstractions.States.GemAlarmChangeStatus Unknown = 2`

### `public enum Dreamine.Gem.Abstractions.States.GemCommandStatus`

- `const Dreamine.Gem.Abstractions.States.GemCommandStatus Completed = 0`
- `const Dreamine.Gem.Abstractions.States.GemCommandStatus Failed = 3`
- `const Dreamine.Gem.Abstractions.States.GemCommandStatus InvalidParameter = 2`
- `const Dreamine.Gem.Abstractions.States.GemCommandStatus NotAllowed = 1`

### `public enum Dreamine.Gem.Abstractions.States.GemCommunicationState`

- `const Dreamine.Gem.Abstractions.States.GemCommunicationState Disabled = 0`
- `const Dreamine.Gem.Abstractions.States.GemCommunicationState EnabledCommunicating = 2`
- `const Dreamine.Gem.Abstractions.States.GemCommunicationState EnabledNotCommunicating = 1`

### `public enum Dreamine.Gem.Abstractions.States.GemConstantSetStatus`

- `const Dreamine.Gem.Abstractions.States.GemConstantSetStatus PolicyDenied = 3`
- `const Dreamine.Gem.Abstractions.States.GemConstantSetStatus Unknown = 1`
- `const Dreamine.Gem.Abstractions.States.GemConstantSetStatus Updated = 0`
- `const Dreamine.Gem.Abstractions.States.GemConstantSetStatus ValidationFailed = 2`

### `public enum Dreamine.Gem.Abstractions.States.GemControlState`

- `const Dreamine.Gem.Abstractions.States.GemControlState AttemptOnline = 1`
- `const Dreamine.Gem.Abstractions.States.GemControlState EquipmentOffline = 0`
- `const Dreamine.Gem.Abstractions.States.GemControlState HostOffline = 2`
- `const Dreamine.Gem.Abstractions.States.GemControlState OnlineLocal = 3`
- `const Dreamine.Gem.Abstractions.States.GemControlState OnlineRemote = 4`

### `public enum Dreamine.Gem.Abstractions.States.GemEstablishmentState`

- `const Dreamine.Gem.Abstractions.States.GemEstablishmentState None = 0`
- `const Dreamine.Gem.Abstractions.States.GemEstablishmentState WaitCrFromHost = 1`
- `const Dreamine.Gem.Abstractions.States.GemEstablishmentState WaitCra = 2`
- `const Dreamine.Gem.Abstractions.States.GemEstablishmentState WaitDelay = 3`

### `public enum Dreamine.Gem.Abstractions.States.GemProcessingState`

- `const Dreamine.Gem.Abstractions.States.GemProcessingState Executing = 4`
- `const Dreamine.Gem.Abstractions.States.GemProcessingState Idle = 1`
- `const Dreamine.Gem.Abstractions.States.GemProcessingState Initializing = 0`
- `const Dreamine.Gem.Abstractions.States.GemProcessingState Paused = 5`
- `const Dreamine.Gem.Abstractions.States.GemProcessingState Ready = 3`
- `const Dreamine.Gem.Abstractions.States.GemProcessingState Setup = 2`

### `public enum Dreamine.Gem.Abstractions.States.GemSpoolState`

- `const Dreamine.Gem.Abstractions.States.GemSpoolState Disabled = 0`
- `const Dreamine.Gem.Abstractions.States.GemSpoolState Spooling = 1`
- `const Dreamine.Gem.Abstractions.States.GemSpoolState Transmitting = 2`

### `public enum Dreamine.Gem.Abstractions.States.GemVariableKind`

- `const Dreamine.Gem.Abstractions.States.GemVariableKind Data = 1`
- `const Dreamine.Gem.Abstractions.States.GemVariableKind Status = 0`
