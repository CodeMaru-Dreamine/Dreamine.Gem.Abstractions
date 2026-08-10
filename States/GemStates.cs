namespace Dreamine.Gem.Abstractions.States;

/// <summary>\if KO GEM 통신 상태입니다. \endif \if EN Defines GEM communication states. \endif</summary>
public enum GemCommunicationState
{
    /// <summary>\if KO 통신 기능이 비활성화되었습니다. \endif \if EN Communication is disabled. \endif</summary>
    Disabled,
    /// <summary>\if KO 활성화되었지만 통신이 수립되지 않았습니다. \endif \if EN Enabled but not communicating. \endif</summary>
    EnabledNotCommunicating,
    /// <summary>\if KO 통신이 수립되었습니다. \endif \if EN Communication is established. \endif</summary>
    EnabledCommunicating
}

/// <summary>\if KO 통신 수립 하위 상태입니다. \endif \if EN Defines communication-establishment substates. \endif</summary>
public enum GemEstablishmentState
{
    /// <summary>\if KO 수립 시도가 없습니다. \endif \if EN No establishment attempt is active. \endif</summary>
    None,
    /// <summary>\if KO 호스트의 통신 요청을 기다립니다. \endif \if EN Waiting for a communication request from the host. \endif</summary>
    WaitCrFromHost,
    /// <summary>\if KO 통신 요청 확인을 기다립니다. \endif \if EN Waiting for communication-request acknowledgement. \endif</summary>
    WaitCra,
    /// <summary>\if KO 다음 재시도 지연을 기다립니다. \endif \if EN Waiting for the retry delay. \endif</summary>
    WaitDelay
}

/// <summary>\if KO GEM 제어 상태입니다. \endif \if EN Defines GEM control states. \endif</summary>
public enum GemControlState
{
    /// <summary>\if KO 장비 오프라인입니다. \endif \if EN Equipment offline. \endif</summary>
    EquipmentOffline,
    /// <summary>\if KO 온라인 전환을 시도합니다. \endif \if EN Attempting to go online. \endif</summary>
    AttemptOnline,
    /// <summary>\if KO 호스트 오프라인입니다. \endif \if EN Host offline. \endif</summary>
    HostOffline,
    /// <summary>\if KO 온라인 로컬입니다. \endif \if EN Online local. \endif</summary>
    OnlineLocal,
    /// <summary>\if KO 온라인 원격입니다. \endif \if EN Online remote. \endif</summary>
    OnlineRemote
}

/// <summary>\if KO 장비 처리 상태입니다. \endif \if EN Defines equipment processing states. \endif</summary>
public enum GemProcessingState
{
    /// <summary>\if KO 초기화 중입니다. \endif \if EN Initializing. \endif</summary>
    Initializing,
    /// <summary>\if KO 유휴 상태입니다. \endif \if EN Idle. \endif</summary>
    Idle,
    /// <summary>\if KO 공정 설정 중입니다. \endif \if EN Setting up a process. \endif</summary>
    Setup,
    /// <summary>\if KO 실행 준비 상태입니다. \endif \if EN Ready to execute. \endif</summary>
    Ready,
    /// <summary>\if KO 실행 중입니다. \endif \if EN Executing. \endif</summary>
    Executing,
    /// <summary>\if KO 일시 정지되었습니다. \endif \if EN Paused. \endif</summary>
    Paused
}

/// <summary>\if KO GEM 변수 종류입니다. \endif \if EN Defines GEM variable kinds. \endif</summary>
public enum GemVariableKind
{
    /// <summary>\if KO 상태 변수입니다. \endif \if EN Status variable. \endif</summary>
    Status,
    /// <summary>\if KO 데이터 변수입니다. \endif \if EN Data variable. \endif</summary>
    Data
}

/// <summary>\if KO 원격 명령 실행 결과입니다. Wire ACK 값이 아닙니다. \endif \if EN Defines remote-command execution results; these are not wire ACK values. \endif</summary>
public enum GemCommandStatus
{
    /// <summary>\if KO 명령을 완료했습니다. \endif \if EN The command completed. \endif</summary>
    Completed,
    /// <summary>\if KO 명령이 현재 상태에서 허용되지 않습니다. \endif \if EN The command is not allowed in the current state. \endif</summary>
    NotAllowed,
    /// <summary>\if KO 매개변수가 유효하지 않습니다. \endif \if EN A parameter is invalid. \endif</summary>
    InvalidParameter,
    /// <summary>\if KO 실행에 실패했습니다. \endif \if EN Execution failed. \endif</summary>
    Failed
}

/// <summary>\if KO 스풀 저장소의 동작 상태입니다. \endif \if EN Defines spool-store operating states. \endif</summary>
public enum GemSpoolState
{
    /// <summary>\if KO 스풀 기능이 비활성화되었습니다. \endif \if EN Spooling is disabled. \endif</summary>
    Disabled,
    /// <summary>\if KO 메시지를 스풀에 적재합니다. \endif \if EN Messages are being spooled. \endif</summary>
    Spooling,
    /// <summary>\if KO 적재된 메시지를 전송합니다. \endif \if EN Spooled messages are being transmitted. \endif</summary>
    Transmitting
}

/// <summary>\if KO 장비 상수 변경의 자체 도메인 결과입니다. Wire ACK가 아닙니다. \endif \if EN Defines application-domain equipment-constant update results; these are not wire ACK values. \endif</summary>
public enum GemConstantSetStatus
{
    /// <summary>\if KO 값이 변경되었습니다. \endif \if EN The value was updated. \endif</summary>
    Updated,
    /// <summary>\if KO 상수를 찾을 수 없습니다. \endif \if EN The constant is unknown. \endif</summary>
    Unknown,
    /// <summary>\if KO 값 검증에 실패했습니다. \endif \if EN Value validation failed. \endif</summary>
    ValidationFailed,
    /// <summary>\if KO 현재 제어 상태 정책이 변경을 거부했습니다. \endif \if EN Current control-state policy denied the update. \endif</summary>
    PolicyDenied
}

/// <summary>\if KO 알람 상태 변경의 자체 도메인 결과입니다. Wire ACK가 아닙니다. \endif \if EN Defines application-domain alarm-change results; these are not wire ACK values. \endif</summary>
public enum GemAlarmChangeStatus
{
    /// <summary>\if KO 상태가 변경되었습니다. \endif \if EN State changed. \endif</summary>
    Changed,
    /// <summary>\if KO 이미 요청 상태였습니다. \endif \if EN The requested state was already active. \endif</summary>
    NoChange,
    /// <summary>\if KO 알람을 찾을 수 없습니다. \endif \if EN The alarm is unknown. \endif</summary>
    Unknown
}
