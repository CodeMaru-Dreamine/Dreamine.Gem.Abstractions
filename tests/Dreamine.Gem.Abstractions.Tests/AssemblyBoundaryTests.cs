using Dreamine.Gem.Abstractions.Interfaces;
using Xunit;

namespace Dreamine.Gem.Abstractions.Tests;

public sealed class AssemblyBoundaryTests
{
    [Fact]
    public void RuntimeContractBelongsToExpectedAssembly() =>
        Assert.Equal("Dreamine.Gem.Abstractions", typeof(IGemRuntime).Assembly.GetName().Name);
}
