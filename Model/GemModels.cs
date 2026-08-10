using System.Collections.ObjectModel;
using Dreamine.Gem.Abstractions.States;
using Dreamine.Secs.Abstractions.Model;

namespace Dreamine.Gem.Abstractions.Model;

/// <summary>\if KO 온라인 식별 메시지에 사용하는 불변 장비 식별 정보입니다. \endif \if EN Represents immutable equipment identity used by online-identification messages. \endif</summary>
public sealed class GemEquipmentIdentity
{
    /// <summary>\if KO 장비 식별 정보를 만듭니다. \endif \if EN Creates equipment identity. \endif</summary>
    public GemEquipmentIdentity(string modelNumber, string softwareRevision)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(modelNumber); ArgumentException.ThrowIfNullOrWhiteSpace(softwareRevision);
        _ = new SecsAsciiItem(modelNumber); _ = new SecsAsciiItem(softwareRevision);
        ModelNumber = modelNumber; SoftwareRevision = softwareRevision;
    }
    /// <summary>\if KO 장비 모델 번호입니다. \endif \if EN Gets the equipment model number. \endif</summary>
    public string ModelNumber { get; }
    /// <summary>\if KO 소프트웨어 Revision입니다. \endif \if EN Gets the software revision. \endif</summary>
    public string SoftwareRevision { get; }
}

/// <summary>\if KO 불변 GEM 변수 정의입니다. \endif \if EN Represents an immutable GEM variable definition. \endif</summary>
public sealed class GemVariableDefinition
{
    /// <summary>\if KO 변수 정의를 만듭니다. \endif \if EN Creates a variable definition. \endif</summary>
    public GemVariableDefinition(ulong id, string name, GemVariableKind kind, string description = "", string units = "")
    {
        if (id == 0) throw new ArgumentOutOfRangeException(nameof(id));
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Id = id; Name = name; Kind = kind; Description = description ?? throw new ArgumentNullException(nameof(description)); Units = units ?? throw new ArgumentNullException(nameof(units));
    }
    /// <summary>\if KO 변수 식별자입니다. \endif \if EN Gets the variable identifier. \endif</summary>
    public ulong Id { get; }
    /// <summary>\if KO 변수 이름입니다. \endif \if EN Gets the variable name. \endif</summary>
    public string Name { get; }
    /// <summary>\if KO 변수 종류입니다. \endif \if EN Gets the variable kind. \endif</summary>
    public GemVariableKind Kind { get; }
    /// <summary>\if KO 설명입니다. \endif \if EN Gets the description. \endif</summary>
    public string Description { get; }
    /// <summary>\if KO 단위입니다. \endif \if EN Gets the units. \endif</summary>
    public string Units { get; }
}

/// <summary>\if KO 불변 장비 상수 정의입니다. \endif \if EN Represents an immutable equipment-constant definition. \endif</summary>
public sealed class GemEquipmentConstantDefinition
{
    /// <summary>\if KO 장비 상수 정의를 만듭니다. \endif \if EN Creates an equipment-constant definition. \endif</summary>
    public GemEquipmentConstantDefinition(ulong id, string name, SecsItem defaultValue, string description = "", string units = "", SecsItem? minimumValue = null, SecsItem? maximumValue = null)
    {
        if (id == 0) throw new ArgumentOutOfRangeException(nameof(id));
        ArgumentException.ThrowIfNullOrWhiteSpace(name); ArgumentNullException.ThrowIfNull(defaultValue);
        Id = id; Name = name; DefaultValue = defaultValue; Description = description ?? throw new ArgumentNullException(nameof(description)); Units = units ?? throw new ArgumentNullException(nameof(units)); MinimumValue = minimumValue; MaximumValue = maximumValue;
    }
    /// <summary>\if KO 상수 식별자입니다. \endif \if EN Gets the constant identifier. \endif</summary>
    public ulong Id { get; }
    /// <summary>\if KO 상수 이름입니다. \endif \if EN Gets the constant name. \endif</summary>
    public string Name { get; }
    /// <summary>\if KO 기본값입니다. \endif \if EN Gets the default value. \endif</summary>
    public SecsItem DefaultValue { get; }
    /// <summary>\if KO 선택적 최소값 메타데이터입니다. 실제 검증은 등록된 형식화 검증기가 수행합니다. \endif \if EN Gets optional minimum-value metadata; the registered typed validator performs enforcement. \endif</summary>
    public SecsItem? MinimumValue { get; }
    /// <summary>\if KO 선택적 최대값 메타데이터입니다. 실제 검증은 등록된 형식화 검증기가 수행합니다. \endif \if EN Gets optional maximum-value metadata; the registered typed validator performs enforcement. \endif</summary>
    public SecsItem? MaximumValue { get; }
    /// <summary>\if KO 설명입니다. \endif \if EN Gets the description. \endif</summary>
    public string Description { get; }
    /// <summary>\if KO 단위입니다. \endif \if EN Gets the units. \endif</summary>
    public string Units { get; }
}

/// <summary>\if KO 불변 보고서 정의입니다. \endif \if EN Represents an immutable report definition. \endif</summary>
public sealed class GemReportDefinition
{
    private readonly ReadOnlyCollection<ulong> _variableIds;
    /// <summary>\if KO 보고서 정의를 만듭니다. \endif \if EN Creates a report definition. \endif</summary>
    public GemReportDefinition(ulong id, IEnumerable<ulong> variableIds)
    {
        if (id == 0) throw new ArgumentOutOfRangeException(nameof(id)); ArgumentNullException.ThrowIfNull(variableIds);
        var values = variableIds.ToArray();
        if (values.Any(static value => value == 0) || values.Distinct().Count() != values.Length) throw new ArgumentException("Variable identifiers must be non-zero and unique.", nameof(variableIds));
        Id = id; _variableIds = Array.AsReadOnly(values);
    }
    /// <summary>\if KO 보고서 식별자입니다. \endif \if EN Gets the report identifier. \endif</summary>
    public ulong Id { get; }
    /// <summary>\if KO 보고서 변수 식별자입니다. \endif \if EN Gets the report variable identifiers. \endif</summary>
    public IReadOnlyList<ulong> VariableIds => _variableIds;
}

/// <summary>\if KO 불변 수집 이벤트 정의입니다. \endif \if EN Represents an immutable collection-event definition. \endif</summary>
public sealed class GemCollectionEventDefinition
{
    private readonly ReadOnlyCollection<ulong> _reportIds;
    /// <summary>\if KO 수집 이벤트 정의를 만듭니다. \endif \if EN Creates a collection-event definition. \endif</summary>
    public GemCollectionEventDefinition(ulong id, string name, IEnumerable<ulong>? reportIds = null, bool enabled = true)
    {
        if (id == 0) throw new ArgumentOutOfRangeException(nameof(id)); ArgumentException.ThrowIfNullOrWhiteSpace(name);
        var values = reportIds?.ToArray() ?? [];
        if (values.Any(static value => value == 0) || values.Distinct().Count() != values.Length) throw new ArgumentException("Report identifiers must be non-zero and unique.", nameof(reportIds));
        Id = id; Name = name; Enabled = enabled; _reportIds = Array.AsReadOnly(values);
    }
    /// <summary>\if KO 이벤트 식별자입니다. \endif \if EN Gets the event identifier. \endif</summary>
    public ulong Id { get; }
    /// <summary>\if KO 이벤트 이름입니다. \endif \if EN Gets the event name. \endif</summary>
    public string Name { get; }
    /// <summary>\if KO 초기 활성화 여부입니다. \endif \if EN Gets whether the event is initially enabled. \endif</summary>
    public bool Enabled { get; }
    /// <summary>\if KO 연결된 보고서 식별자입니다. \endif \if EN Gets linked report identifiers. \endif</summary>
    public IReadOnlyList<ulong> ReportIds => _reportIds;
}

/// <summary>\if KO 불변 알람 정의입니다. \endif \if EN Represents an immutable alarm definition. \endif</summary>
public sealed class GemAlarmDefinition
{
    /// <summary>\if KO 알람 정의를 만듭니다. \endif \if EN Creates an alarm definition. \endif</summary>
    public GemAlarmDefinition(ulong id, byte code, string text, bool enabled = true)
    {
        if (id == 0) throw new ArgumentOutOfRangeException(nameof(id)); ArgumentException.ThrowIfNullOrWhiteSpace(text);
        Id = id; Code = code; Text = text; Enabled = enabled;
    }
    /// <summary>\if KO 알람 식별자입니다. \endif \if EN Gets the alarm identifier. \endif</summary>
    public ulong Id { get; }
    /// <summary>\if KO 애플리케이션 알람 코드입니다. \endif \if EN Gets the application alarm code. \endif</summary>
    public byte Code { get; }
    /// <summary>\if KO 알람 설명입니다. \endif \if EN Gets the alarm text. \endif</summary>
    public string Text { get; }
    /// <summary>\if KO 초기 활성화 여부입니다. \endif \if EN Gets whether reporting is initially enabled. \endif</summary>
    public bool Enabled { get; }
}

/// <summary>\if KO 불변 원격 명령 정의입니다. \endif \if EN Represents an immutable remote-command definition. \endif</summary>
public sealed class GemRemoteCommandDefinition
{
    private readonly ReadOnlyCollection<string> _parameters;
    /// <summary>\if KO 원격 명령 정의를 만듭니다. \endif \if EN Creates a remote-command definition. \endif</summary>
    public GemRemoteCommandDefinition(string name, IEnumerable<string>? parameters = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        var values = parameters?.ToArray() ?? [];
        if (values.Any(string.IsNullOrWhiteSpace) || values.Distinct(StringComparer.Ordinal).Count() != values.Length) throw new ArgumentException("Parameter names must be non-empty and unique.", nameof(parameters));
        Name = name; _parameters = Array.AsReadOnly(values);
    }
    /// <summary>\if KO 명령 이름입니다. \endif \if EN Gets the command name. \endif</summary>
    public string Name { get; }
    /// <summary>\if KO 필수 매개변수 이름입니다. \endif \if EN Gets required parameter names. \endif</summary>
    public IReadOnlyList<string> Parameters => _parameters;
}

/// <summary>\if KO 원격 명령의 자체 도메인 결과입니다. \endif \if EN Represents an application-domain remote-command result. \endif</summary>
public sealed class GemCommandResult
{
    /// <summary>\if KO 결과를 만듭니다. \endif \if EN Creates a result. \endif</summary>
    public GemCommandResult(GemCommandStatus status, string detail = "") { Status = status; Detail = detail ?? throw new ArgumentNullException(nameof(detail)); }
    /// <summary>\if KO 상태입니다. \endif \if EN Gets the status. \endif</summary>
    public GemCommandStatus Status { get; }
    /// <summary>\if KO 일반화된 상세 정보입니다. \endif \if EN Gets generalized detail. \endif</summary>
    public string Detail { get; }
}

/// <summary>\if KO 불변 공정 프로그램입니다. \endif \if EN Represents an immutable process program. \endif</summary>
public sealed class GemProcessProgram
{
    private readonly byte[] _body;
    /// <summary>\if KO 공정 프로그램을 만듭니다. \endif \if EN Creates a process program. \endif</summary>
    public GemProcessProgram(string id, ReadOnlySpan<byte> body)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id); Id = id; _body = body.ToArray();
    }
    /// <summary>\if KO 프로그램 식별자입니다. \endif \if EN Gets the program identifier. \endif</summary>
    public string Id { get; }
    /// <summary>\if KO 불투명 프로그램 본문입니다. \endif \if EN Gets the opaque program body. \endif</summary>
    public ReadOnlyMemory<byte> Body => _body;
}

/// <summary>\if KO 발생한 수집 이벤트의 불변 스냅샷입니다. \endif \if EN Represents an immutable emitted-event snapshot. \endif</summary>
public sealed class GemEventSnapshot
{
    private readonly ReadOnlyDictionary<ulong, SecsItem> _values;
    /// <summary>\if KO 이벤트 스냅샷을 만듭니다. \endif \if EN Creates an event snapshot. \endif</summary>
    public GemEventSnapshot(ulong eventId, DateTimeOffset occurredAt, IDictionary<ulong, SecsItem> values)
    {
        if (eventId == 0) throw new ArgumentOutOfRangeException(nameof(eventId)); ArgumentNullException.ThrowIfNull(values);
        EventId = eventId; OccurredAt = occurredAt; _values = new ReadOnlyDictionary<ulong, SecsItem>(new Dictionary<ulong, SecsItem>(values));
    }
    /// <summary>\if KO 이벤트 식별자입니다. \endif \if EN Gets the event identifier. \endif</summary>
    public ulong EventId { get; }
    /// <summary>\if KO 발생 시각입니다. \endif \if EN Gets the occurrence time. \endif</summary>
    public DateTimeOffset OccurredAt { get; }
    /// <summary>\if KO 수집된 변수 값입니다. \endif \if EN Gets collected variable values. \endif</summary>
    public IReadOnlyDictionary<ulong, SecsItem> Values => _values;
}
