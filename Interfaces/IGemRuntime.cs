using Dreamine.Secs.Abstractions.Interfaces;

namespace Dreamine.Gem.Abstractions.Interfaces;

/// <summary>
/// \if KO
/// <para>구체 SECS 구현과 분리된 GEM 런타임의 최소 경계 계약입니다.</para>
/// \endif
/// \if EN
/// <para>Defines the minimal GEM runtime boundary decoupled from a concrete SECS implementation.</para>
/// \endif
/// </summary>
public interface IGemRuntime
{
    /// <summary>
    /// \if KO
    /// <para>GEM 런타임이 사용하는 공급자 독립 SECS 연결을 가져옵니다.</para>
    /// \endif
    /// \if EN
    /// <para>Gets the provider-independent SECS connection used by the GEM runtime.</para>
    /// \endif
    /// </summary>
    ISecsConnection SecsConnection { get; }
}
