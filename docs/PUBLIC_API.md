# Public API Inventory

Assembly: `Dreamine.Gem.Abstractions`

This inventory is generated from the compiled Release assembly. It is an audit artifact, not an additional compatibility promise.

Exported types: **50**

## Types

### `public interface Dreamine.Gem.Abstractions.Interfaces.IGemAlarmService`

- `Dreamine.Gem.Abstractions.States.GemAlarmChangeStatus ChangeAlarm(System.UInt64 id, System.Boolean isSet)`
- `System.Boolean SetAlarm(System.UInt64 id, System.Boolean isSet)`
- `System.Boolean SetEnabled(System.UInt64 id, System.Boolean enabled)`
- `System.Boolean TryGetState(System.UInt64 id, out System.Boolean isSet)`
- `System.Collections.Generic.IReadOnlyList<System.UInt64> GetSetAlarmIds()`
- `System.Void Register(Dreamine.Gem.Abstractions.Model.GemAlarmDefinition definition)`

### `public interface Dreamine.Gem.Abstractions.Interfaces.IGemClockService`

- `System.DateTimeOffset GetUtcNow()`
- `System.String Format(System.Boolean fourDigitYear)`
- `System.Void SetUtcNow(System.DateTimeOffset value)`

### `public interface Dreamine.Gem.Abstractions.Interfaces.IGemEquipmentConstantService`

- `Dreamine.Gem.Abstractions.States.GemConstantSetStatus SetValue(System.UInt64 id, Dreamine.Secs.Abstractions.Model.SecsItem value, Dreamine.Gem.Abstractions.States.GemControlState controlState)`
- `System.Boolean TryGetValue(System.UInt64 id, out Dreamine.Secs.Abstractions.Model.SecsItem value)`
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
- `System.Boolean TryGet(System.String id, out Dreamine.Gem.Abstractions.Model.GemProcessProgram program)`
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

- `System.Boolean TryGetDefinition(System.UInt64 id, out Dreamine.Gem.Abstractions.Model.GemVariableDefinition definition)`
- `System.Collections.Generic.IReadOnlyList<Dreamine.Gem.Abstractions.Model.GemVariableDefinition> GetDefinitions(System.Nullable<Dreamine.Gem.Abstractions.States.GemVariableKind> kind)`
- `System.Threading.Tasks.ValueTask<Dreamine.Secs.Abstractions.Model.SecsItem> ReadAsync(System.UInt64 id, System.Threading.CancellationToken cancellationToken)`
- `System.Void Register(Dreamine.Gem.Abstractions.Model.GemVariableDefinition definition, System.Func<System.Threading.CancellationToken, System.Threading.Tasks.ValueTask<Dreamine.Secs.Abstractions.Model.SecsItem>> reader)`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemAlarmDefinition`

- `GemAlarmDefinition(System.UInt64 id, System.Byte code, System.String text, System.Boolean enabled)`
- `System.Boolean Enabled { get; }`
- `System.Byte Code { get; }`
- `System.String Text { get; }`
- `System.UInt64 Id { get; }`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemAlarmSnapshot`

- `Dreamine.Gem.Abstractions.Model.GemAlarmDefinition Definition { get; }`
- `GemAlarmSnapshot(Dreamine.Gem.Abstractions.Model.GemAlarmDefinition definition, System.Boolean enabled, System.Boolean isSet)`
- `System.Boolean Enabled { get; }`
- `System.Boolean IsSet { get; }`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemCollectionEventDefinition`

- `GemCollectionEventDefinition(System.UInt64 id, System.String name, System.Collections.Generic.IEnumerable<System.UInt64> reportIds, System.Boolean enabled)`
- `System.Boolean Enabled { get; }`
- `System.Collections.Generic.IReadOnlyList<System.UInt64> ReportIds { get; }`
- `System.String Name { get; }`
- `System.UInt64 Id { get; }`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemCollectionEventSnapshot`

- `Dreamine.Gem.Abstractions.Model.GemCollectionEventDefinition Definition { get; }`
- `GemCollectionEventSnapshot(Dreamine.Gem.Abstractions.Model.GemCollectionEventDefinition definition, System.Collections.Generic.IEnumerable<System.UInt64> reportIds, System.Boolean enabled)`
- `System.Boolean Enabled { get; }`
- `System.Collections.Generic.IReadOnlyList<System.UInt64> ReportIds { get; }`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemCommandResult`

- `Dreamine.Gem.Abstractions.States.GemCommandStatus Status { get; }`
- `GemCommandResult(Dreamine.Gem.Abstractions.States.GemCommandStatus status, System.String detail)`
- `System.String Detail { get; }`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemConstantBatchResult`

- `Dreamine.Gem.Abstractions.Model.GemConstantBatchStatus Status { get; }`
- `GemConstantBatchResult(Dreamine.Gem.Abstractions.Model.GemConstantBatchStatus status, System.Nullable<System.UInt64> failedId)`
- `System.Nullable<System.UInt64> FailedId { get; }`

### `public enum Dreamine.Gem.Abstractions.Model.GemConstantBatchStatus`

- `const Dreamine.Gem.Abstractions.Model.GemConstantBatchStatus Duplicate = 2`
- `const Dreamine.Gem.Abstractions.Model.GemConstantBatchStatus PolicyDenied = 4`
- `const Dreamine.Gem.Abstractions.Model.GemConstantBatchStatus Unknown = 1`
- `const Dreamine.Gem.Abstractions.Model.GemConstantBatchStatus Updated = 0`
- `const Dreamine.Gem.Abstractions.Model.GemConstantBatchStatus ValidationFailed = 3`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemEquipmentConstantDefinition`

- `Dreamine.Secs.Abstractions.Model.SecsItem DefaultValue { get; }`
- `Dreamine.Secs.Abstractions.Model.SecsItem MaximumValue { get; }`
- `Dreamine.Secs.Abstractions.Model.SecsItem MinimumValue { get; }`
- `GemEquipmentConstantDefinition(System.UInt64 id, System.String name, Dreamine.Secs.Abstractions.Model.SecsItem defaultValue, System.String description, System.String units, Dreamine.Secs.Abstractions.Model.SecsItem minimumValue, Dreamine.Secs.Abstractions.Model.SecsItem maximumValue)`
- `System.String Description { get; }`
- `System.String Name { get; }`
- `System.String Units { get; }`
- `System.UInt64 Id { get; }`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemEquipmentConstantProfileDefinition`

- `Dreamine.Gem.Abstractions.Model.GemEquipmentConstantDefinition Definition { get; }`
- `Dreamine.Secs.Abstractions.Model.SecsItemFormat Format { get; }`
- `GemEquipmentConstantProfileDefinition(Dreamine.Gem.Abstractions.Model.GemEquipmentConstantDefinition definition, Dreamine.Secs.Abstractions.Model.SecsItemFormat format, System.Func<Dreamine.Secs.Abstractions.Model.SecsItem, System.Boolean> validator, System.Collections.Generic.IEnumerable<Dreamine.Gem.Abstractions.States.GemControlState> allowedControlStates)`
- `System.Collections.Generic.IReadOnlyList<Dreamine.Gem.Abstractions.States.GemControlState> AllowedControlStates { get; }`
- `System.Func<Dreamine.Secs.Abstractions.Model.SecsItem, System.Boolean> Validator { get; }`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemEquipmentConstantSnapshot`

- `Dreamine.Gem.Abstractions.Model.GemEquipmentConstantDefinition Definition { get; }`
- `Dreamine.Secs.Abstractions.Model.SecsItem Value { get; }`
- `GemEquipmentConstantSnapshot(Dreamine.Gem.Abstractions.Model.GemEquipmentConstantDefinition definition, Dreamine.Secs.Abstractions.Model.SecsItem value)`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemEquipmentConstantUpdate`

- `Dreamine.Secs.Abstractions.Model.SecsItem Value { get; }`
- `GemEquipmentConstantUpdate(System.UInt64 id, Dreamine.Secs.Abstractions.Model.SecsItem value)`
- `System.UInt64 Id { get; }`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemEquipmentIdentity`

- `GemEquipmentIdentity(System.String modelNumber, System.String softwareRevision)`
- `System.String ModelNumber { get; }`
- `System.String SoftwareRevision { get; }`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemEventConfigurationResult`

- `Dreamine.Gem.Abstractions.Model.GemEventConfigurationStatus Status { get; }`
- `GemEventConfigurationResult(Dreamine.Gem.Abstractions.Model.GemEventConfigurationStatus status, System.Nullable<System.UInt64> failedId)`
- `System.Nullable<System.UInt64> FailedId { get; }`

### `public enum Dreamine.Gem.Abstractions.Model.GemEventConfigurationStatus`

- `const Dreamine.Gem.Abstractions.Model.GemEventConfigurationStatus Applied = 0`
- `const Dreamine.Gem.Abstractions.Model.GemEventConfigurationStatus Duplicate = 1`
- `const Dreamine.Gem.Abstractions.Model.GemEventConfigurationStatus ExistingLinks = 5`
- `const Dreamine.Gem.Abstractions.Model.GemEventConfigurationStatus ReportInUse = 6`
- `const Dreamine.Gem.Abstractions.Model.GemEventConfigurationStatus UnknownEvent = 4`
- `const Dreamine.Gem.Abstractions.Model.GemEventConfigurationStatus UnknownReport = 3`
- `const Dreamine.Gem.Abstractions.Model.GemEventConfigurationStatus UnknownVariable = 2`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemEventEnableUpdate`

- `GemEventEnableUpdate(System.UInt64 eventId, System.Boolean enabled)`
- `System.Boolean Enabled { get; }`
- `System.UInt64 EventId { get; }`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemEventReportLinkUpdate`

- `GemEventReportLinkUpdate(System.UInt64 eventId, System.Collections.Generic.IEnumerable<System.UInt64> reportIds)`
- `System.Collections.Generic.IReadOnlyList<System.UInt64> ReportIds { get; }`
- `System.UInt64 EventId { get; }`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemEventSnapshot`

- `GemEventSnapshot(System.UInt64 eventId, System.DateTimeOffset occurredAt, System.Collections.Generic.IDictionary<System.UInt64, Dreamine.Secs.Abstractions.Model.SecsItem> values)`
- `GemEventSnapshot(System.UInt64 eventId, System.DateTimeOffset occurredAt, System.Collections.Generic.IEnumerable<Dreamine.Gem.Abstractions.Model.GemReportValueSnapshot> reports)`
- `System.Collections.Generic.IReadOnlyDictionary<System.UInt64, Dreamine.Secs.Abstractions.Model.SecsItem> Values { get; }`
- `System.Collections.Generic.IReadOnlyList<Dreamine.Gem.Abstractions.Model.GemReportValueSnapshot> Reports { get; }`
- `System.DateTimeOffset OccurredAt { get; }`
- `System.UInt64 EventId { get; }`

### `public enum Dreamine.Gem.Abstractions.Model.GemIdentifierFamily`

- `const Dreamine.Gem.Abstractions.Model.GemIdentifierFamily Alarm = 6`
- `const Dreamine.Gem.Abstractions.Model.GemIdentifierFamily CollectionEvent = 0`
- `const Dreamine.Gem.Abstractions.Model.GemIdentifierFamily DataIdentifier = 7`
- `const Dreamine.Gem.Abstractions.Model.GemIdentifierFamily DataVariable = 4`
- `const Dreamine.Gem.Abstractions.Model.GemIdentifierFamily EquipmentConstant = 5`
- `const Dreamine.Gem.Abstractions.Model.GemIdentifierFamily Report = 1`
- `const Dreamine.Gem.Abstractions.Model.GemIdentifierFamily StatusVariable = 3`
- `const Dreamine.Gem.Abstractions.Model.GemIdentifierFamily Variable = 2`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemIdentifierFormatOverride`

- `Dreamine.Gem.Abstractions.Model.GemIdentifierFamily Family { get; }`
- `Dreamine.Secs.Abstractions.Model.SecsItemFormat Format { get; }`
- `GemIdentifierFormatOverride(Dreamine.Gem.Abstractions.Model.GemIdentifierFamily family, Dreamine.Secs.Abstractions.Model.SecsItemFormat format)`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemIdentifierFormatPolicy`

- `Dreamine.Secs.Abstractions.Model.SecsItem CreateItem(Dreamine.Gem.Abstractions.Model.GemIdentifierFamily family, System.UInt64 value)`
- `Dreamine.Secs.Abstractions.Model.SecsItemFormat GetFormat(Dreamine.Gem.Abstractions.Model.GemIdentifierFamily family)`
- `GemIdentifierFormatPolicy(System.Collections.Generic.IEnumerable<Dreamine.Gem.Abstractions.Model.GemIdentifierFormatOverride> overrides)`
- `System.Boolean IsValid(Dreamine.Gem.Abstractions.Model.GemIdentifierFamily family, System.UInt64 value)`
- `System.Collections.Generic.IReadOnlyDictionary<Dreamine.Gem.Abstractions.Model.GemIdentifierFamily, Dreamine.Secs.Abstractions.Model.SecsItemFormat> Formats { get; }`
- `System.UInt64 GetMaximum(Dreamine.Gem.Abstractions.Model.GemIdentifierFamily family)`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemProcessProgram`

- `GemProcessProgram(System.String id, System.ReadOnlySpan<System.Byte> body)`
- `System.ReadOnlyMemory<System.Byte> Body { get; }`
- `System.String Id { get; }`

### `public enum Dreamine.Gem.Abstractions.Model.GemProfileCapability`

- `const Dreamine.Gem.Abstractions.Model.GemProfileCapability Alarms = 3`
- `const Dreamine.Gem.Abstractions.Model.GemProfileCapability Clock = 6`
- `const Dreamine.Gem.Abstractions.Model.GemProfileCapability CollectionEventsAndReports = 4`
- `const Dreamine.Gem.Abstractions.Model.GemProfileCapability CommunicationAndControl = 0`
- `const Dreamine.Gem.Abstractions.Model.GemProfileCapability EquipmentConstants = 2`
- `const Dreamine.Gem.Abstractions.Model.GemProfileCapability RemoteCommands = 5`
- `const Dreamine.Gem.Abstractions.Model.GemProfileCapability StatusAndDataVariables = 1`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemRemoteCommandDefinition`

- `GemRemoteCommandDefinition(System.String name, System.Collections.Generic.IEnumerable<System.String> parameters)`
- `System.Collections.Generic.IReadOnlyList<System.String> Parameters { get; }`
- `System.String Name { get; }`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemRemoteCommandParameterDefinition`

- `Dreamine.Secs.Abstractions.Model.SecsItemFormat Format { get; }`
- `GemRemoteCommandParameterDefinition(System.String name, Dreamine.Secs.Abstractions.Model.SecsItemFormat format, System.Boolean required, System.Func<Dreamine.Secs.Abstractions.Model.SecsItem, System.Boolean> validator)`
- `System.Boolean Required { get; }`
- `System.Func<Dreamine.Secs.Abstractions.Model.SecsItem, System.Boolean> Validator { get; }`
- `System.String Name { get; }`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemRemoteCommandProfileDefinition`

- `GemRemoteCommandProfileDefinition(System.String name, System.Collections.Generic.IEnumerable<Dreamine.Gem.Abstractions.Model.GemRemoteCommandParameterDefinition> parameters)`
- `System.Collections.Generic.IReadOnlyList<Dreamine.Gem.Abstractions.Model.GemRemoteCommandParameterDefinition> Parameters { get; }`
- `System.String Name { get; }`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemRemoteCommandProfileEntry`

- `Dreamine.Gem.Abstractions.Model.GemRemoteCommandProfileDefinition Definition { get; }`
- `GemRemoteCommandProfileEntry(Dreamine.Gem.Abstractions.Model.GemRemoteCommandProfileDefinition definition, System.Func<System.Collections.Generic.IReadOnlyDictionary<System.String, Dreamine.Secs.Abstractions.Model.SecsItem>, System.Threading.CancellationToken, System.Threading.Tasks.ValueTask<Dreamine.Gem.Abstractions.Model.GemCommandResult>> handler)`
- `System.Func<System.Collections.Generic.IReadOnlyDictionary<System.String, Dreamine.Secs.Abstractions.Model.SecsItem>, System.Threading.CancellationToken, System.Threading.Tasks.ValueTask<Dreamine.Gem.Abstractions.Model.GemCommandResult>> Handler { get; }`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemReportDefinition`

- `GemReportDefinition(System.UInt64 id, System.Collections.Generic.IEnumerable<System.UInt64> variableIds)`
- `System.Collections.Generic.IReadOnlyList<System.UInt64> VariableIds { get; }`
- `System.UInt64 Id { get; }`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemReportValueSnapshot`

- `GemReportValueSnapshot(System.UInt64 reportId, System.Collections.Generic.IEnumerable<Dreamine.Gem.Abstractions.Model.GemVariableValueSnapshot> values)`
- `System.Collections.Generic.IReadOnlyList<Dreamine.Gem.Abstractions.Model.GemVariableValueSnapshot> Values { get; }`
- `System.UInt64 ReportId { get; }`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemVariableDefinition`

- `Dreamine.Gem.Abstractions.States.GemVariableKind Kind { get; }`
- `GemVariableDefinition(System.UInt64 id, System.String name, Dreamine.Gem.Abstractions.States.GemVariableKind kind, System.String description, System.String units)`
- `System.String Description { get; }`
- `System.String Name { get; }`
- `System.String Units { get; }`
- `System.UInt64 Id { get; }`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemVariableProfileDefinition`

- `Dreamine.Gem.Abstractions.Model.GemVariableDefinition Definition { get; }`
- `Dreamine.Secs.Abstractions.Model.SecsItemFormat Format { get; }`
- `GemVariableProfileDefinition(Dreamine.Gem.Abstractions.Model.GemVariableDefinition definition, Dreamine.Secs.Abstractions.Model.SecsItemFormat format, System.Func<System.Threading.CancellationToken, System.Threading.Tasks.ValueTask<Dreamine.Secs.Abstractions.Model.SecsItem>> reader)`
- `System.Func<System.Threading.CancellationToken, System.Threading.Tasks.ValueTask<Dreamine.Secs.Abstractions.Model.SecsItem>> Reader { get; }`

### `public sealed class Dreamine.Gem.Abstractions.Model.GemVariableValueSnapshot`

- `Dreamine.Secs.Abstractions.Model.SecsItem Value { get; }`
- `GemVariableValueSnapshot(System.UInt64 variableId, Dreamine.Secs.Abstractions.Model.SecsItem value)`
- `System.UInt64 VariableId { get; }`

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
