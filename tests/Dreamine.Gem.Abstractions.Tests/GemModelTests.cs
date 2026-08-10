using Dreamine.Gem.Abstractions.Model;
using Dreamine.Gem.Abstractions.States;
using Dreamine.Secs.Abstractions.Model;
using Xunit;

namespace Dreamine.Gem.Abstractions.Tests;

public sealed class GemModelTests
{
    [Fact]
    public void VariableDefinitionRejectsZeroIdentifier() => Assert.Throws<ArgumentOutOfRangeException>(() => new GemVariableDefinition(0, "SV", GemVariableKind.Status));

    [Fact]
    public void EquipmentIdentityRejectsNonAsciiValues() => Assert.Throws<ArgumentException>(() => new GemEquipmentIdentity("장비", "1"));

    [Fact]
    public void ReportDefinitionCopiesAndValidatesIdentifiers()
    {
        ulong[] ids = [1, 2];
        var report = new GemReportDefinition(3, ids);
        ids[0] = 9;
        Assert.Equal(new ulong[] { 1, 2 }, report.VariableIds);
        Assert.Throws<ArgumentException>(() => new GemReportDefinition(4, new ulong[] { 1, 1 }));
    }

    [Fact]
    public void CollectionEventCopiesReportLinks()
    {
        ulong[] ids = [7];
        var collectionEvent = new GemCollectionEventDefinition(1, "Ready", ids);
        ids[0] = 8;
        Assert.Equal((ulong)7, collectionEvent.ReportIds[0]);
    }

    [Fact]
    public void RemoteCommandRequiresUniqueParameterNames() =>
        Assert.Throws<ArgumentException>(() => new GemRemoteCommandDefinition("START", new[] { "PORT", "PORT" }));

    [Fact]
    public void ProcessProgramDefensivelyCopiesBody()
    {
        byte[] body = [1, 2, 3];
        var program = new GemProcessProgram("P1", body);
        body[0] = 9;
        Assert.Equal((byte)1, program.Body.Span[0]);
    }

    [Fact]
    public void EventSnapshotCopiesDictionary()
    {
        var values = new Dictionary<ulong, SecsItem> { [1] = new SecsUInt8Item(2) };
        var snapshot = new GemEventSnapshot(4, DateTimeOffset.UnixEpoch, values);
        values.Clear();
        Assert.Single(snapshot.Values);
    }

    [Fact]
    public void CommandStatusIsExplicitlyDomainLevel() =>
        Assert.Equal(GemCommandStatus.InvalidParameter, new GemCommandResult(GemCommandStatus.InvalidParameter).Status);
}
