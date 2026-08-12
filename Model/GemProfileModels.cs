using System.Collections.ObjectModel;
using Dreamine.Gem.Abstractions.States;
using Dreamine.Secs.Abstractions.Model;

namespace Dreamine.Gem.Abstractions.Model;

/// <summary>\if KO E30-0611 파생 v1 장비 프로필의 닫힌 기능 집합입니다. \endif \if EN Defines the closed capability set of the E30-0611-derived v1 equipment profile. \endif</summary>
public enum GemProfileCapability
{
    /// <summary>\if KO 통신 및 제어 상태입니다. \endif \if EN Communication and control state. \endif</summary>
    CommunicationAndControl,
    /// <summary>\if KO 상태·데이터 변수입니다. \endif \if EN Status and data variables. \endif</summary>
    StatusAndDataVariables,
    /// <summary>\if KO 장비 상수입니다. \endif \if EN Equipment constants. \endif</summary>
    EquipmentConstants,
    /// <summary>\if KO 알람입니다. \endif \if EN Alarms. \endif</summary>
    Alarms,
    /// <summary>\if KO 수집 이벤트 및 보고서입니다. \endif \if EN Collection events and reports. \endif</summary>
    CollectionEventsAndReports,
    /// <summary>\if KO 원격 명령입니다. \endif \if EN Remote commands. \endif</summary>
    RemoteCommands,
    /// <summary>\if KO 논리 시계입니다. \endif \if EN Logical clock. \endif</summary>
    Clock
}

/// <summary>\if KO 프로필에서 폭을 독립 설정하는 GEM 식별자 계열입니다. \endif \if EN Defines GEM identifier families whose widths are independently configured by a profile. \endif</summary>
public enum GemIdentifierFamily
{
    /// <summary>\if KO CEID입니다. \endif \if EN Collection-event identifier (CEID). \endif</summary>
    CollectionEvent,
    /// <summary>\if KO RPTID입니다. \endif \if EN Report identifier (RPTID). \endif</summary>
    Report,
    /// <summary>\if KO 일반 VID입니다. \endif \if EN General variable identifier (VID). \endif</summary>
    Variable,
    /// <summary>\if KO SVID입니다. \endif \if EN Status-variable identifier (SVID). \endif</summary>
    StatusVariable,
    /// <summary>\if KO DVID입니다. \endif \if EN Data-variable identifier (DVID). \endif</summary>
    DataVariable,
    /// <summary>\if KO ECID입니다. \endif \if EN Equipment-constant identifier (ECID). \endif</summary>
    EquipmentConstant,
    /// <summary>\if KO ALID입니다. \endif \if EN Alarm identifier (ALID). \endif</summary>
    Alarm,
    /// <summary>\if KO DATAID입니다. \endif \if EN Data identifier (DATAID). \endif</summary>
    DataIdentifier
}

/// <summary>\if KO 한 식별자 계열의 SECS 정수 형식 override입니다. \endif \if EN Represents a SECS integer-format override for one identifier family. \endif</summary>
public sealed class GemIdentifierFormatOverride
{
    /// <summary>\if KO override를 만듭니다. \endif \if EN Creates an override. \endif</summary>
    public GemIdentifierFormatOverride(GemIdentifierFamily family, SecsItemFormat format)
    {
        Family = family;
        Format = format;
    }

    /// <summary>\if KO 식별자 계열입니다. \endif \if EN Gets the identifier family. \endif</summary>
    public GemIdentifierFamily Family { get; }

    /// <summary>\if KO SECS Item 형식입니다. \endif \if EN Gets the SECS item format. \endif</summary>
    public SecsItemFormat Format { get; }
}

/// <summary>\if KO GEM 식별자 계열별 U1/U2/U4/U8 형식과 범위를 고정합니다. 지정하지 않은 계열은 U4입니다. \endif \if EN Freezes U1/U2/U4/U8 formats and ranges by GEM identifier family; unspecified families default to U4. \endif</summary>
public sealed class GemIdentifierFormatPolicy
{
    private readonly ReadOnlyDictionary<GemIdentifierFamily, SecsItemFormat> _formats;

    /// <summary>\if KO 기본 U4 정책에 선택적 override를 적용합니다. \endif \if EN Applies optional overrides to the default U4 policy. \endif</summary>
    public GemIdentifierFormatPolicy(IEnumerable<GemIdentifierFormatOverride>? overrides = null)
    {
        var values = Enum.GetValues<GemIdentifierFamily>()
            .ToDictionary(static family => family, static _ => SecsItemFormat.UInt32);
        var seen = new HashSet<GemIdentifierFamily>();
        foreach (var item in overrides ?? [])
        {
            ArgumentNullException.ThrowIfNull(item);
            if (!Enum.IsDefined(item.Family)) throw new ArgumentOutOfRangeException(nameof(overrides), "An identifier family is not defined.");
            EnsureUnsignedIdentifierFormat(item.Format, nameof(overrides));
            if (!seen.Add(item.Family)) throw new ArgumentException("An identifier family is overridden more than once.", nameof(overrides));
            values[item.Family] = item.Format;
        }
        _formats = new(values);
    }

    /// <summary>\if KO 모든 계열을 enum 순서로 포함하는 읽기 전용 형식 스냅샷입니다. \endif \if EN Gets a read-only format snapshot containing every family in enum order. \endif</summary>
    public IReadOnlyDictionary<GemIdentifierFamily, SecsItemFormat> Formats => _formats;

    /// <summary>\if KO 계열의 형식을 반환합니다. \endif \if EN Gets the format for a family. \endif</summary>
    public SecsItemFormat GetFormat(GemIdentifierFamily family)
    {
        if (!_formats.TryGetValue(family, out var format)) throw new ArgumentOutOfRangeException(nameof(family));
        return format;
    }

    /// <summary>\if KO 값이 계열의 양의 범위 안인지 확인합니다. \endif \if EN Returns whether a value is positive and within the configured family range. \endif</summary>
    public bool IsValid(GemIdentifierFamily family, ulong value) => value > 0 && value <= GetMaximum(family);

    /// <summary>\if KO 계열 형식으로 단일 값 SECS Item을 만듭니다. \endif \if EN Creates a single-value SECS item in the configured family format. \endif</summary>
    public SecsItem CreateItem(GemIdentifierFamily family, ulong value)
    {
        if (!IsValid(family, value)) throw new ArgumentOutOfRangeException(nameof(value), value, "The identifier does not fit the configured format.");
        return GetFormat(family) switch
        {
            SecsItemFormat.UInt8 => new SecsUInt8Item((byte)value),
            SecsItemFormat.UInt16 => new SecsUInt16Item((ushort)value),
            SecsItemFormat.UInt32 => new SecsUInt32Item((uint)value),
            SecsItemFormat.UInt64 => new SecsUInt64Item(value),
            _ => throw new InvalidOperationException("The identifier format policy contains an unsupported format.")
        };
    }

    /// <summary>\if KO 계열에서 허용하는 최댓값을 반환합니다. \endif \if EN Gets the maximum value supported by a family. \endif</summary>
    public ulong GetMaximum(GemIdentifierFamily family) => GetFormat(family) switch
    {
        SecsItemFormat.UInt8 => byte.MaxValue,
        SecsItemFormat.UInt16 => ushort.MaxValue,
        SecsItemFormat.UInt32 => uint.MaxValue,
        SecsItemFormat.UInt64 => ulong.MaxValue,
        _ => throw new InvalidOperationException("The identifier format policy contains an unsupported format.")
    };

    internal static void EnsureConcreteFormat(SecsItemFormat format, string parameterName)
    {
        if (!Enum.IsDefined(format)) throw new ArgumentOutOfRangeException(parameterName, format, "The SECS item format is not defined.");
    }

    private static void EnsureUnsignedIdentifierFormat(SecsItemFormat format, string parameterName)
    {
        if (format is not (SecsItemFormat.UInt8 or SecsItemFormat.UInt16 or SecsItemFormat.UInt32 or SecsItemFormat.UInt64))
            throw new ArgumentException("GEM identifiers support only U1, U2, U4, or U8.", parameterName);
    }
}

/// <summary>\if KO 형식과 판독기를 포함한 불변 변수 프로필 정의입니다. \endif \if EN Represents an immutable variable profile definition with its format and reader. \endif</summary>
public sealed class GemVariableProfileDefinition
{
    /// <summary>\if KO 변수 프로필 정의를 만듭니다. \endif \if EN Creates a variable profile definition. \endif</summary>
    public GemVariableProfileDefinition(GemVariableDefinition definition, SecsItemFormat format, Func<CancellationToken, ValueTask<SecsItem>> reader)
    {
        Definition = definition ?? throw new ArgumentNullException(nameof(definition));
        GemIdentifierFormatPolicy.EnsureConcreteFormat(format, nameof(format));
        Format = format;
        Reader = reader ?? throw new ArgumentNullException(nameof(reader));
    }

    /// <summary>\if KO 변수 메타데이터입니다. \endif \if EN Gets the variable metadata. \endif</summary>
    public GemVariableDefinition Definition { get; }

    /// <summary>\if KO 값의 정확한 SECS Item 형식입니다. \endif \if EN Gets the exact SECS item format of the value. \endif</summary>
    public SecsItemFormat Format { get; }

    /// <summary>\if KO 값 판독기입니다. \endif \if EN Gets the value reader. \endif</summary>
    public Func<CancellationToken, ValueTask<SecsItem>> Reader { get; }
}

/// <summary>\if KO 형식, 검증기 및 허용 제어 상태를 포함한 불변 장비 상수 프로필 정의입니다. \endif \if EN Represents an immutable equipment-constant profile definition with format, validator, and allowed control states. \endif</summary>
public sealed class GemEquipmentConstantProfileDefinition
{
    private readonly ReadOnlyCollection<GemControlState> _allowedControlStates;

    /// <summary>\if KO 장비 상수 프로필 정의를 만듭니다. \endif \if EN Creates an equipment-constant profile definition. \endif</summary>
    public GemEquipmentConstantProfileDefinition(
        GemEquipmentConstantDefinition definition,
        SecsItemFormat format,
        Func<SecsItem, bool>? validator = null,
        IEnumerable<GemControlState>? allowedControlStates = null)
    {
        Definition = definition ?? throw new ArgumentNullException(nameof(definition));
        GemIdentifierFormatPolicy.EnsureConcreteFormat(format, nameof(format));
        if (definition.DefaultValue.Format != format) throw new ArgumentException("The default value does not match the declared format.", nameof(format));
        Format = format;
        Validator = validator;
        var states = (allowedControlStates ?? Enum.GetValues<GemControlState>()).ToArray();
        if (states.Length == 0 || states.Any(static state => !Enum.IsDefined(state)) || states.Distinct().Count() != states.Length)
            throw new ArgumentException("Allowed control states must be non-empty, defined, and unique.", nameof(allowedControlStates));
        _allowedControlStates = Array.AsReadOnly(states);
    }

    /// <summary>\if KO 장비 상수 메타데이터입니다. \endif \if EN Gets the equipment-constant metadata. \endif</summary>
    public GemEquipmentConstantDefinition Definition { get; }

    /// <summary>\if KO 값의 정확한 SECS Item 형식입니다. \endif \if EN Gets the exact SECS item format of the value. \endif</summary>
    public SecsItemFormat Format { get; }

    /// <summary>\if KO 선택적 값 검증기입니다. \endif \if EN Gets the optional value validator. \endif</summary>
    public Func<SecsItem, bool>? Validator { get; }

    /// <summary>\if KO 변경을 허용하는 제어 상태의 순서 보존 스냅샷입니다. \endif \if EN Gets an order-preserving snapshot of control states that permit changes. \endif</summary>
    public IReadOnlyList<GemControlState> AllowedControlStates => _allowedControlStates;
}

/// <summary>\if KO 원격 명령 매개변수의 정확한 형식, 필수 여부 및 검증기를 정의합니다. \endif \if EN Defines the exact format, requirement, and validator of a remote-command parameter. \endif</summary>
public sealed class GemRemoteCommandParameterDefinition
{
    /// <summary>\if KO 매개변수 정의를 만듭니다. \endif \if EN Creates a parameter definition. \endif</summary>
    public GemRemoteCommandParameterDefinition(string name, SecsItemFormat format, bool required = true, Func<SecsItem, bool>? validator = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        GemIdentifierFormatPolicy.EnsureConcreteFormat(format, nameof(format));
        Name = name;
        Format = format;
        Required = required;
        Validator = validator;
    }

    /// <summary>\if KO 매개변수 이름입니다. \endif \if EN Gets the parameter name. \endif</summary>
    public string Name { get; }

    /// <summary>\if KO 정확한 SECS Item 형식입니다. \endif \if EN Gets the exact SECS item format. \endif</summary>
    public SecsItemFormat Format { get; }

    /// <summary>\if KO 필수 매개변수인지 여부입니다. \endif \if EN Gets whether the parameter is required. \endif</summary>
    public bool Required { get; }

    /// <summary>\if KO 선택적 값 검증기입니다. \endif \if EN Gets the optional value validator. \endif</summary>
    public Func<SecsItem, bool>? Validator { get; }
}

/// <summary>\if KO 형식화된 매개변수를 포함한 불변 원격 명령 프로필 정의입니다. Wire ACK 계약이 아닙니다. \endif \if EN Represents an immutable typed remote-command profile definition; it is not a wire ACK contract. \endif</summary>
public sealed class GemRemoteCommandProfileDefinition
{
    private readonly ReadOnlyCollection<GemRemoteCommandParameterDefinition> _parameters;

    /// <summary>\if KO 원격 명령 프로필 정의를 만듭니다. \endif \if EN Creates a remote-command profile definition. \endif</summary>
    public GemRemoteCommandProfileDefinition(string name, IEnumerable<GemRemoteCommandParameterDefinition>? parameters = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        var values = parameters?.ToArray() ?? [];
        if (values.Any(static value => value is null) || values.Select(static value => value.Name).Distinct(StringComparer.Ordinal).Count() != values.Length)
            throw new ArgumentException("Parameter definitions must be non-null and have unique ordinal names.", nameof(parameters));
        Name = name;
        _parameters = Array.AsReadOnly(values);
    }

    /// <summary>\if KO 명령 이름입니다. \endif \if EN Gets the command name. \endif</summary>
    public string Name { get; }

    /// <summary>\if KO 선언 순서를 보존하는 매개변수 스냅샷입니다. \endif \if EN Gets an order-preserving parameter snapshot. \endif</summary>
    public IReadOnlyList<GemRemoteCommandParameterDefinition> Parameters => _parameters;
}

/// <summary>\if KO 형식화된 정의와 실행기를 묶는 불변 원격 명령 프로필 항목입니다. \endif \if EN Represents an immutable remote-command profile entry pairing a typed definition and handler. \endif</summary>
public sealed class GemRemoteCommandProfileEntry
{
    /// <summary>\if KO 프로필 항목을 만듭니다. \endif \if EN Creates a profile entry. \endif</summary>
    public GemRemoteCommandProfileEntry(
        GemRemoteCommandProfileDefinition definition,
        Func<IReadOnlyDictionary<string, SecsItem>, CancellationToken, ValueTask<GemCommandResult>> handler)
    {
        Definition = definition ?? throw new ArgumentNullException(nameof(definition));
        Handler = handler ?? throw new ArgumentNullException(nameof(handler));
    }

    /// <summary>\if KO 형식화된 명령 정의입니다. \endif \if EN Gets the typed command definition. \endif</summary>
    public GemRemoteCommandProfileDefinition Definition { get; }

    /// <summary>\if KO 명령 실행기입니다. \endif \if EN Gets the command handler. \endif</summary>
    public Func<IReadOnlyDictionary<string, SecsItem>, CancellationToken, ValueTask<GemCommandResult>> Handler { get; }
}

/// <summary>\if KO 장비 상수 일괄 변경 항목입니다. \endif \if EN Represents one equipment-constant batch update. \endif</summary>
public sealed class GemEquipmentConstantUpdate
{
    /// <summary>\if KO 변경 항목을 만듭니다. \endif \if EN Creates an update. \endif</summary>
    public GemEquipmentConstantUpdate(ulong id, SecsItem value)
    {
        if (id == 0) throw new ArgumentOutOfRangeException(nameof(id));
        Id = id;
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>\if KO ECID입니다. \endif \if EN Gets the equipment-constant identifier. \endif</summary>
    public ulong Id { get; }

    /// <summary>\if KO 적용할 값입니다. \endif \if EN Gets the value to apply. \endif</summary>
    public SecsItem Value { get; }
}

/// <summary>\if KO 장비 상수 일괄 변경의 자체 도메인 결과입니다. Wire ACK가 아닙니다. \endif \if EN Defines application-domain equipment-constant batch results; these are not wire ACK values. \endif</summary>
public enum GemConstantBatchStatus
{
    /// <summary>\if KO 모든 값이 원자적으로 적용되었습니다. \endif \if EN All values were applied atomically. \endif</summary>
    Updated,
    /// <summary>\if KO 알 수 없는 ECID가 있습니다. \endif \if EN An ECID is unknown. \endif</summary>
    Unknown,
    /// <summary>\if KO 같은 ECID가 중복되었습니다. \endif \if EN An ECID was duplicated. \endif</summary>
    Duplicate,
    /// <summary>\if KO 형식 또는 값 검증에 실패했습니다. \endif \if EN Format or value validation failed. \endif</summary>
    ValidationFailed,
    /// <summary>\if KO 현재 제어 상태 정책이 거부했습니다. \endif \if EN Current control-state policy denied the update. \endif</summary>
    PolicyDenied
}

/// <summary>\if KO 장비 상수 일괄 변경 결과입니다. \endif \if EN Represents an equipment-constant batch result. \endif</summary>
public sealed class GemConstantBatchResult
{
    /// <summary>\if KO 결과를 만듭니다. \endif \if EN Creates a result. \endif</summary>
    public GemConstantBatchResult(GemConstantBatchStatus status, ulong? failedId = null)
    {
        Status = status;
        FailedId = failedId;
    }

    /// <summary>\if KO 결과 상태입니다. \endif \if EN Gets the result status. \endif</summary>
    public GemConstantBatchStatus Status { get; }

    /// <summary>\if KO 실패한 ECID입니다. \endif \if EN Gets the failed ECID, when applicable. \endif</summary>
    public ulong? FailedId { get; }
}

/// <summary>\if KO 장비 상수 정의와 현재값의 불변 스냅샷입니다. \endif \if EN Represents an immutable equipment-constant definition/value snapshot. \endif</summary>
public sealed class GemEquipmentConstantSnapshot
{
    /// <summary>\if KO 스냅샷을 만듭니다. \endif \if EN Creates a snapshot. \endif</summary>
    public GemEquipmentConstantSnapshot(GemEquipmentConstantDefinition definition, SecsItem value)
    {
        Definition = definition ?? throw new ArgumentNullException(nameof(definition));
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>\if KO 불변 정의입니다. \endif \if EN Gets the immutable definition. \endif</summary>
    public GemEquipmentConstantDefinition Definition { get; }

    /// <summary>\if KO 스냅샷 시점의 값입니다. \endif \if EN Gets the value at snapshot time. \endif</summary>
    public SecsItem Value { get; }
}

/// <summary>\if KO 알람 정의, enable 및 set 상태의 불변 스냅샷입니다. \endif \if EN Represents an immutable alarm definition, enablement, and set-state snapshot. \endif</summary>
public sealed class GemAlarmSnapshot
{
    /// <summary>\if KO 스냅샷을 만듭니다. \endif \if EN Creates a snapshot. \endif</summary>
    public GemAlarmSnapshot(GemAlarmDefinition definition, bool enabled, bool isSet)
    {
        Definition = definition ?? throw new ArgumentNullException(nameof(definition));
        Enabled = enabled;
        IsSet = isSet;
    }

    /// <summary>\if KO 불변 정의입니다. \endif \if EN Gets the immutable definition. \endif</summary>
    public GemAlarmDefinition Definition { get; }

    /// <summary>\if KO 보고 활성화 여부입니다. \endif \if EN Gets whether reporting is enabled. \endif</summary>
    public bool Enabled { get; }

    /// <summary>\if KO 알람 설정 여부입니다. \endif \if EN Gets whether the alarm is set. \endif</summary>
    public bool IsSet { get; }
}

/// <summary>\if KO 수집 이벤트의 동적 연결·enable 상태 스냅샷입니다. \endif \if EN Represents a snapshot of a collection event's dynamic links and enablement. \endif</summary>
public sealed class GemCollectionEventSnapshot
{
    private readonly ReadOnlyCollection<ulong> _reportIds;

    /// <summary>\if KO 스냅샷을 만듭니다. \endif \if EN Creates a snapshot. \endif</summary>
    public GemCollectionEventSnapshot(GemCollectionEventDefinition definition, IEnumerable<ulong> reportIds, bool enabled)
    {
        Definition = definition ?? throw new ArgumentNullException(nameof(definition));
        ArgumentNullException.ThrowIfNull(reportIds);
        _reportIds = Array.AsReadOnly(reportIds.ToArray());
        Enabled = enabled;
    }

    /// <summary>\if KO 불변 이벤트 정의입니다. \endif \if EN Gets the immutable event definition. \endif</summary>
    public GemCollectionEventDefinition Definition { get; }

    /// <summary>\if KO 현재 연결된 RPTID의 순서 보존 스냅샷입니다. \endif \if EN Gets an order-preserving snapshot of currently linked RPTIDs. \endif</summary>
    public IReadOnlyList<ulong> ReportIds => _reportIds;

    /// <summary>\if KO 이벤트 활성화 여부입니다. \endif \if EN Gets whether the event is enabled. \endif</summary>
    public bool Enabled { get; }
}

/// <summary>\if KO 보고서 안의 VID와 값입니다. \endif \if EN Represents a VID and value within one report. \endif</summary>
public sealed class GemVariableValueSnapshot
{
    /// <summary>\if KO 값 스냅샷을 만듭니다. \endif \if EN Creates a value snapshot. \endif</summary>
    public GemVariableValueSnapshot(ulong variableId, SecsItem value)
    {
        if (variableId == 0) throw new ArgumentOutOfRangeException(nameof(variableId));
        VariableId = variableId;
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>\if KO VID입니다. \endif \if EN Gets the VID. \endif</summary>
    public ulong VariableId { get; }

    /// <summary>\if KO 수집 값입니다. \endif \if EN Gets the collected value. \endif</summary>
    public SecsItem Value { get; }
}

/// <summary>\if KO RPTID별 순서 보존 값 스냅샷입니다. \endif \if EN Represents an order-preserving value snapshot for one RPTID. \endif</summary>
public sealed class GemReportValueSnapshot
{
    private readonly ReadOnlyCollection<GemVariableValueSnapshot> _values;

    /// <summary>\if KO 보고서 값 스냅샷을 만듭니다. \endif \if EN Creates a report-value snapshot. \endif</summary>
    public GemReportValueSnapshot(ulong reportId, IEnumerable<GemVariableValueSnapshot> values)
    {
        if (reportId == 0) throw new ArgumentOutOfRangeException(nameof(reportId));
        ArgumentNullException.ThrowIfNull(values);
        var snapshot = values.ToArray();
        if (snapshot.Any(static value => value is null)) throw new ArgumentException("Report values cannot contain null.", nameof(values));
        ReportId = reportId;
        _values = Array.AsReadOnly(snapshot);
    }

    /// <summary>\if KO RPTID입니다. \endif \if EN Gets the RPTID. \endif</summary>
    public ulong ReportId { get; }

    /// <summary>\if KO 보고서 정의 순서를 보존하는 VID/값 스냅샷입니다. \endif \if EN Gets the VID/value snapshot preserving report-definition order. \endif</summary>
    public IReadOnlyList<GemVariableValueSnapshot> Values => _values;
}

/// <summary>\if KO 이벤트·보고서 일괄 구성의 자체 도메인 결과입니다. Wire ACK 값이 아닙니다. \endif \if EN Defines application-domain event/report batch-configuration results; these are not wire ACK values. \endif</summary>
public enum GemEventConfigurationStatus
{
    /// <summary>\if KO 전체 변경이 적용되었습니다. \endif \if EN The complete change was applied. \endif</summary>
    Applied,
    /// <summary>\if KO 입력 또는 기존 정의와 중복됩니다. \endif \if EN An input or existing definition is duplicated. \endif</summary>
    Duplicate,
    /// <summary>\if KO 알 수 없는 VID가 있습니다. \endif \if EN A VID is unknown. \endif</summary>
    UnknownVariable,
    /// <summary>\if KO 알 수 없는 RPTID가 있습니다. \endif \if EN An RPTID is unknown. \endif</summary>
    UnknownReport,
    /// <summary>\if KO 알 수 없는 CEID가 있습니다. \endif \if EN A CEID is unknown. \endif</summary>
    UnknownEvent,
    /// <summary>\if KO 기존 보고서 연결이 있어 새 구성 적용이 거부되었습니다. \endif \if EN Existing report links caused the new configuration to be rejected. \endif</summary>
    ExistingLinks,
    /// <summary>\if KO 삭제 대상 보고서가 이벤트에서 사용 중입니다. \endif \if EN A report selected for deletion is linked by an event. \endif</summary>
    ReportInUse
}

/// <summary>\if KO 이벤트·보고서 일괄 구성 결과입니다. \endif \if EN Represents an event/report batch-configuration result. \endif</summary>
public sealed class GemEventConfigurationResult
{
    /// <summary>\if KO 결과를 만듭니다. \endif \if EN Creates a result. \endif</summary>
    public GemEventConfigurationResult(GemEventConfigurationStatus status, ulong? failedId = null)
    {
        Status = status;
        FailedId = failedId;
    }

    /// <summary>\if KO 결과 상태입니다. \endif \if EN Gets the result status. \endif</summary>
    public GemEventConfigurationStatus Status { get; }

    /// <summary>\if KO 실패와 관련된 ID입니다. \endif \if EN Gets the ID associated with a failure. \endif</summary>
    public ulong? FailedId { get; }
}

/// <summary>\if KO 한 CEID의 RPTID 연결을 교체하는 불변 항목입니다. \endif \if EN Represents an immutable replacement of RPTID links for one CEID. \endif</summary>
public sealed class GemEventReportLinkUpdate
{
    private readonly ReadOnlyCollection<ulong> _reportIds;

    /// <summary>\if KO 연결 교체 항목을 만듭니다. 빈 목록은 해당 이벤트의 연결을 모두 제거합니다. \endif \if EN Creates a link replacement; an empty list removes every link from the event. \endif</summary>
    public GemEventReportLinkUpdate(ulong eventId, IEnumerable<ulong> reportIds)
    {
        if (eventId == 0) throw new ArgumentOutOfRangeException(nameof(eventId));
        ArgumentNullException.ThrowIfNull(reportIds);
        var values = reportIds.ToArray();
        if (values.Any(static id => id == 0) || values.Distinct().Count() != values.Length)
            throw new ArgumentException("Report IDs must be positive and unique.", nameof(reportIds));
        EventId = eventId;
        _reportIds = Array.AsReadOnly(values);
    }

    /// <summary>\if KO CEID입니다. \endif \if EN Gets the CEID. \endif</summary>
    public ulong EventId { get; }

    /// <summary>\if KO 새 RPTID 순서입니다. \endif \if EN Gets the new RPTID order. \endif</summary>
    public IReadOnlyList<ulong> ReportIds => _reportIds;
}

/// <summary>\if KO 한 CEID의 enable 변경 항목입니다. \endif \if EN Represents one CEID enablement update. \endif</summary>
public sealed class GemEventEnableUpdate
{
    /// <summary>\if KO enable 변경 항목을 만듭니다. \endif \if EN Creates an enablement update. \endif</summary>
    public GemEventEnableUpdate(ulong eventId, bool enabled)
    {
        if (eventId == 0) throw new ArgumentOutOfRangeException(nameof(eventId));
        EventId = eventId;
        Enabled = enabled;
    }

    /// <summary>\if KO CEID입니다. \endif \if EN Gets the CEID. \endif</summary>
    public ulong EventId { get; }

    /// <summary>\if KO 적용할 enable 상태입니다. \endif \if EN Gets the enablement to apply. \endif</summary>
    public bool Enabled { get; }
}
