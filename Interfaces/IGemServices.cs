using Dreamine.Gem.Abstractions.Model;
using Dreamine.Gem.Abstractions.States;
using Dreamine.Secs.Abstractions.Model;

namespace Dreamine.Gem.Abstractions.Interfaces;

/// <summary>\if KO GEM 변수 카탈로그 계약입니다. \endif \if EN Defines the GEM variable catalog contract. \endif</summary>
public interface IGemVariableCatalog
{
    /// <summary>\if KO 변수와 비동기 값 판독기를 등록합니다. \endif \if EN Registers a variable and asynchronous value reader. \endif</summary>
    void Register(GemVariableDefinition definition, Func<CancellationToken, ValueTask<SecsItem>> reader);
    /// <summary>\if KO 등록된 변수 정의를 조회합니다. \endif \if EN Gets a registered variable definition. \endif</summary>
    bool TryGetDefinition(ulong id, out GemVariableDefinition? definition);
    /// <summary>\if KO 현재 변수 값을 읽습니다. \endif \if EN Reads the current variable value. \endif</summary>
    ValueTask<SecsItem> ReadAsync(ulong id, CancellationToken cancellationToken = default);
    /// <summary>\if KO 정의의 안정적인 스냅샷을 반환합니다. \endif \if EN Returns a stable definition snapshot. \endif</summary>
    IReadOnlyList<GemVariableDefinition> GetDefinitions(GemVariableKind? kind = null);
}

/// <summary>\if KO 장비 상수 서비스 계약입니다. \endif \if EN Defines the equipment-constant service contract. \endif</summary>
public interface IGemEquipmentConstantService
{
    /// <summary>\if KO 상수와 선택적 검증기를 등록합니다. \endif \if EN Registers a constant and optional validator. \endif</summary>
    void Register(GemEquipmentConstantDefinition definition, Func<SecsItem, bool>? validator = null, Func<GemControlState, bool>? statePolicy = null);
    /// <summary>\if KO 상수 값을 조회합니다. \endif \if EN Gets a constant value. \endif</summary>
    bool TryGetValue(ulong id, out SecsItem? value);
    /// <summary>\if KO 검증 후 상수 값을 변경합니다. \endif \if EN Changes a constant value after validation. \endif</summary>
    bool TrySetValue(ulong id, SecsItem value);
    /// <summary>\if KO 값·제어 상태 정책을 검증하고 상세 자체 결과를 반환합니다. \endif \if EN Validates value and control-state policy and returns a detailed application result. \endif</summary>
    GemConstantSetStatus SetValue(ulong id, SecsItem value, GemControlState controlState);
}

/// <summary>\if KO 이벤트·보고서 서비스 계약입니다. \endif \if EN Defines the event/report service contract. \endif</summary>
public interface IGemEventReportService
{
    /// <summary>\if KO 보고서 정의를 등록합니다. \endif \if EN Registers a report definition. \endif</summary>
    void DefineReport(GemReportDefinition report);
    /// <summary>\if KO 수집 이벤트 정의를 등록합니다. \endif \if EN Registers a collection-event definition. \endif</summary>
    void DefineEvent(GemCollectionEventDefinition collectionEvent);
    /// <summary>\if KO 이벤트에 보고서를 연결합니다. \endif \if EN Links a report to an event. \endif</summary>
    bool LinkReport(ulong eventId, ulong reportId);
    /// <summary>\if KO 이벤트에서 보고서 연결을 제거합니다. \endif \if EN Unlinks a report from an event. \endif</summary>
    bool UnlinkReport(ulong eventId, ulong reportId);
    /// <summary>\if KO 연결되지 않은 보고서를 삭제합니다. \endif \if EN Deletes an unlinked report. \endif</summary>
    bool DeleteReport(ulong reportId);
    /// <summary>\if KO 이벤트 활성화 상태를 변경합니다. \endif \if EN Changes event enablement. \endif</summary>
    bool SetEnabled(ulong eventId, bool enabled);
    /// <summary>\if KO 안정적인 보고서 연결 스냅샷을 기준으로 이벤트 값을 수집합니다. 외부 판독기 전체가 물리적으로 원자적이라는 뜻은 아닙니다. \endif \if EN Collects event values against a stable report-link snapshot; external readers are not implied to be physically atomic as a group. \endif</summary>
    ValueTask<GemEventSnapshot?> CollectAsync(ulong eventId, CancellationToken cancellationToken = default);
}

/// <summary>\if KO 알람 서비스 계약입니다. \endif \if EN Defines the alarm service contract. \endif</summary>
public interface IGemAlarmService
{
    /// <summary>\if KO 알람 정의를 등록합니다. \endif \if EN Registers an alarm definition. \endif</summary>
    void Register(GemAlarmDefinition definition);
    /// <summary>\if KO 알람 보고 활성화를 변경합니다. \endif \if EN Changes alarm reporting enablement. \endif</summary>
    bool SetEnabled(ulong id, bool enabled);
    /// <summary>\if KO 알람 설정 상태를 변경합니다. \endif \if EN Changes the alarm-set state. \endif</summary>
    bool SetAlarm(ulong id, bool isSet);
    /// <summary>\if KO 중복 변경을 구분하는 상세 자체 결과로 알람 상태를 변경합니다. \endif \if EN Changes alarm state with an application result that distinguishes duplicates. \endif</summary>
    GemAlarmChangeStatus ChangeAlarm(ulong id, bool isSet);
    /// <summary>\if KO 알람의 설정 상태를 조회합니다. \endif \if EN Gets whether an alarm is set. \endif</summary>
    bool TryGetState(ulong id, out bool isSet);
    /// <summary>\if KO 현재 설정된 알람 ID 스냅샷을 반환합니다. \endif \if EN Returns a snapshot of currently set alarm IDs. \endif</summary>
    IReadOnlyList<ulong> GetSetAlarmIds();
}

/// <summary>\if KO 원격 명령 서비스 계약입니다. \endif \if EN Defines the remote-command service contract. \endif</summary>
public interface IGemRemoteCommandService
{
    /// <summary>\if KO 명령 정의와 실행기를 등록합니다. \endif \if EN Registers a command definition and handler. \endif</summary>
    void Register(GemRemoteCommandDefinition definition, Func<IReadOnlyDictionary<string, SecsItem>, CancellationToken, ValueTask<GemCommandResult>> handler);
    /// <summary>\if KO 명령을 제한 시간 안에 실행합니다. \endif \if EN Executes a command within a timeout. \endif</summary>
    ValueTask<GemCommandResult> ExecuteAsync(string name, IReadOnlyDictionary<string, SecsItem> parameters, TimeSpan timeout, CancellationToken cancellationToken = default);
}

/// <summary>\if KO 공정 프로그램 서비스 계약입니다. \endif \if EN Defines the process-program service contract. \endif</summary>
public interface IGemProcessProgramService
{
    /// <summary>\if KO 프로그램을 추가하거나 교체합니다. \endif \if EN Adds or replaces a program. \endif</summary>
    void Put(GemProcessProgram program);
    /// <summary>\if KO 프로그램을 조회합니다. \endif \if EN Gets a program. \endif</summary>
    bool TryGet(string id, out GemProcessProgram? program);
    /// <summary>\if KO 프로그램을 삭제합니다. \endif \if EN Deletes a program. \endif</summary>
    bool Delete(string id);
    /// <summary>\if KO 프로그램 식별자 스냅샷을 반환합니다. \endif \if EN Returns a program-identifier snapshot. \endif</summary>
    IReadOnlyList<string> GetIds();
}

/// <summary>\if KO GEM 시계 서비스 계약입니다. \endif \if EN Defines the GEM clock service contract. \endif</summary>
public interface IGemClockService
{
    /// <summary>\if KO 현재 시간을 반환합니다. \endif \if EN Gets the current time. \endif</summary>
    DateTimeOffset GetUtcNow();
    /// <summary>\if KO 시계 오프셋을 변경합니다. \endif \if EN Changes the clock offset. \endif</summary>
    void SetUtcNow(DateTimeOffset value);
    /// <summary>\if KO E5 호환 12자리 또는 16자리 ASCII 시각을 만듭니다. \endif \if EN Formats E5-compatible 12- or 16-character ASCII time. \endif</summary>
    string Format(bool fourDigitYear = true);
}

/// <summary>\if KO 제한 용량 메모리 스풀 계약입니다. \endif \if EN Defines a bounded in-memory spool contract. \endif</summary>
public interface IGemSpoolService
{
    /// <summary>\if KO 현재 스풀 상태입니다. \endif \if EN Gets the current spool state. \endif</summary>
    GemSpoolState State { get; }
    /// <summary>\if KO 저장된 메시지 수입니다. \endif \if EN Gets the stored-message count. \endif</summary>
    int Count { get; }
    /// <summary>\if KO 스풀 적재를 시작합니다. \endif \if EN Starts spooling. \endif</summary>
    void Start();
    /// <summary>\if KO 메시지를 적재합니다. 용량 초과 시 가장 오래된 항목을 덮어씁니다. \endif \if EN Enqueues a message, overwriting the oldest item at capacity. \endif</summary>
    bool Enqueue(SecsMessage message);
    /// <summary>\if KO 적재된 메시지를 순서대로 전송합니다. \endif \if EN Transmits stored messages in order. \endif</summary>
    Task DrainAsync(Func<SecsMessage, CancellationToken, Task> sender, CancellationToken cancellationToken = default);
    /// <summary>\if KO 적재된 메시지를 제거합니다. \endif \if EN Purges stored messages. \endif</summary>
    void Purge();
}
