using Dreamine.Secs.Abstractions.Interfaces;
using Dreamine.Secs.Abstractions.Model;

namespace Dreamine.Gem.Abstractions.Interfaces;

/// <summary>\if KO GEM 메시지 계층이 사용하는 SECS 전송 경계입니다. \endif \if EN Defines the SECS transport boundary used by the GEM message layer. \endif</summary>
public interface IGemMessageTransport
{
    /// <summary>\if KO 기반 연결 생명주기입니다. \endif \if EN Gets the underlying connection lifecycle. \endif</summary>
    ISecsConnection Connection { get; }
    /// <summary>\if KO 메시지 세션 식별자입니다. \endif \if EN Gets the message session identifier. \endif</summary>
    SecsSessionId SessionId { get; }
    /// <summary>\if KO 수신한 primary 메시지를 알립니다. \endif \if EN Raised when a primary message is received. \endif</summary>
    event EventHandler<SecsMessage>? MessageReceived;
    /// <summary>\if KO 응답 없는 메시지를 보냅니다. \endif \if EN Sends a message without awaiting a reply. \endif</summary>
    Task SendAsync(SecsMessage message, CancellationToken cancellationToken = default);
    /// <summary>\if KO primary를 보내고 상관된 secondary를 기다립니다. \endif \if EN Sends a primary and awaits its correlated secondary. \endif</summary>
    Task<SecsMessage> RequestAsync(SecsMessage message, CancellationToken cancellationToken = default);
    /// <summary>\if KO 새 System Bytes를 할당합니다. \endif \if EN Allocates new system bytes. \endif</summary>
    SecsSystemBytes AllocateSystemBytes();
}
