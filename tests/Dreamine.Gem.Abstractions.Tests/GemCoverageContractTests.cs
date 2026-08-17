using Dreamine.Gem.Abstractions.Model;
using Dreamine.Gem.Abstractions.States;
using Dreamine.Secs.Abstractions.Model;
using Xunit;

namespace Dreamine.Gem.Abstractions.Tests;

public sealed class GemCoverageContractTests
{
    [Fact]
    public async Task CoreModelsPreserveValidatedImmutableValues()
    {
        var identity = new GemEquipmentIdentity("MODEL", "1.0");
        Assert.Equal("MODEL", identity.ModelNumber);
        Assert.Equal("1.0", identity.SoftwareRevision);

        var variable = new GemVariableDefinition(1, "Temperature", GemVariableKind.Status, "Process temperature", "C");
        Assert.Equal((ulong)1, variable.Id);
        Assert.Equal("Temperature", variable.Name);
        Assert.Equal(GemVariableKind.Status, variable.Kind);
        Assert.Equal("Process temperature", variable.Description);
        Assert.Equal("C", variable.Units);

        var minimum = new SecsUInt16Item(1);
        var maximum = new SecsUInt16Item(100);
        var constant = new GemEquipmentConstantDefinition(2, "Speed", new SecsUInt16Item(10), "Line speed", "rpm", minimum, maximum);
        Assert.Equal((ulong)2, constant.Id);
        Assert.Equal("Speed", constant.Name);
        Assert.Equal(minimum, constant.MinimumValue);
        Assert.Equal(maximum, constant.MaximumValue);
        Assert.Equal("Line speed", constant.Description);
        Assert.Equal("rpm", constant.Units);

        var report = new GemReportDefinition(3, [1, 2]);
        var collectionEvent = new GemCollectionEventDefinition(4, "Ready", [3], false);
        var alarm = new GemAlarmDefinition(5, 7, "Overheat", false);
        var command = new GemRemoteCommandDefinition("START", ["PORT"]);
        var result = new GemCommandResult(GemCommandStatus.Completed, "done");
        Assert.Equal(new ulong[] { 1, 2 }, report.VariableIds);
        Assert.Equal("Ready", collectionEvent.Name);
        Assert.False(collectionEvent.Enabled);
        Assert.Equal(new ulong[] { 3 }, collectionEvent.ReportIds);
        Assert.Equal((byte)7, alarm.Code);
        Assert.Equal("Overheat", alarm.Text);
        Assert.False(alarm.Enabled);
        Assert.Equal(new[] { "PORT" }, command.Parameters);
        Assert.Equal("done", result.Detail);

        var program = new GemProcessProgram("P1", [1, 2]);
        Assert.Equal("P1", program.Id);
        Assert.Equal(new byte[] { 1, 2 }, program.Body.ToArray());

        var valueProfile = new GemVariableProfileDefinition(variable, SecsItemFormat.UInt16, _ => ValueTask.FromResult<SecsItem>(new SecsUInt16Item(9)));
        Assert.Equal(variable, valueProfile.Definition);
        Assert.Equal(SecsItemFormat.UInt16, valueProfile.Format);
        Assert.IsType<SecsUInt16Item>(await valueProfile.Reader(CancellationToken.None));
    }

    [Fact]
    public async Task TypedProfileModelsExposeDefinitionsHandlersAndSnapshots()
    {
        var constant = new GemEquipmentConstantDefinition(1, "Speed", new SecsUInt8Item(4));
        var constantProfile = new GemEquipmentConstantProfileDefinition(
            constant,
            SecsItemFormat.UInt8,
            item => item is SecsUInt8Item,
            [GemControlState.OnlineRemote]);
        Assert.Equal(constant, constantProfile.Definition);
        Assert.Equal(SecsItemFormat.UInt8, constantProfile.Format);
        Assert.True(constantProfile.Validator!(new SecsUInt8Item(1)));
        Assert.Equal([GemControlState.OnlineRemote], constantProfile.AllowedControlStates);

        var parameter = new GemRemoteCommandParameterDefinition("PORT", SecsItemFormat.Ascii, false, item => item is SecsAsciiItem);
        var command = new GemRemoteCommandProfileDefinition("START", [parameter]);
        var entry = new GemRemoteCommandProfileEntry(command, (_, _) => ValueTask.FromResult(new GemCommandResult(GemCommandStatus.Completed)));
        Assert.Equal("PORT", parameter.Name);
        Assert.False(parameter.Required);
        Assert.True(parameter.Validator!(new SecsAsciiItem("P1")));
        Assert.Equal("START", command.Name);
        Assert.Equal(parameter, command.Parameters[0]);
        Assert.Equal(command, entry.Definition);
        Assert.Equal(GemCommandStatus.Completed, (await entry.Handler(new Dictionary<string, SecsItem>(), CancellationToken.None)).Status);

        var update = new GemEquipmentConstantUpdate(1, new SecsUInt8Item(8));
        var batch = new GemConstantBatchResult(GemConstantBatchStatus.Updated, 1);
        var constantSnapshot = new GemEquipmentConstantSnapshot(constant, update.Value);
        var alarmDefinition = new GemAlarmDefinition(2, 3, "Alarm");
        var alarm = new GemAlarmSnapshot(alarmDefinition, true, false);
        var eventDefinition = new GemCollectionEventDefinition(3, "Event", [4]);
        var eventSnapshot = new GemCollectionEventSnapshot(eventDefinition, [4], true);
        Assert.Equal((ulong)1, update.Id);
        Assert.Equal(GemConstantBatchStatus.Updated, batch.Status);
        Assert.Equal((ulong)1, batch.FailedId);
        Assert.Equal(constant, constantSnapshot.Definition);
        Assert.Equal(update.Value, constantSnapshot.Value);
        Assert.Equal(alarmDefinition, alarm.Definition);
        Assert.True(alarm.Enabled);
        Assert.False(alarm.IsSet);
        Assert.Equal(eventDefinition, eventSnapshot.Definition);
        Assert.Equal(new ulong[] { 4 }, eventSnapshot.ReportIds);
        Assert.True(eventSnapshot.Enabled);

        var variableValue = new GemVariableValueSnapshot(5, new SecsAsciiItem("V"));
        var reportValue = new GemReportValueSnapshot(6, [variableValue]);
        var configuration = new GemEventConfigurationResult(GemEventConfigurationStatus.Applied, 6);
        var link = new GemEventReportLinkUpdate(3, [6]);
        var enable = new GemEventEnableUpdate(3, false);
        Assert.Equal((ulong)5, variableValue.VariableId);
        Assert.Equal("V", Assert.IsType<SecsAsciiItem>(variableValue.Value).Value);
        Assert.Equal((ulong)6, reportValue.ReportId);
        Assert.Equal(variableValue, reportValue.Values[0]);
        Assert.Equal(GemEventConfigurationStatus.Applied, configuration.Status);
        Assert.Equal((ulong)6, configuration.FailedId);
        Assert.Equal((ulong)3, link.EventId);
        Assert.Equal(new ulong[] { 6 }, link.ReportIds);
        Assert.False(enable.Enabled);
    }

    [Fact]
    public void IdentifierPolicyCoversEveryUnsignedWidthAndRejectsInvalidInputs()
    {
        var policy = new GemIdentifierFormatPolicy(
        [
            new(GemIdentifierFamily.CollectionEvent, SecsItemFormat.UInt8),
            new(GemIdentifierFamily.Report, SecsItemFormat.UInt16),
            new(GemIdentifierFamily.Variable, SecsItemFormat.UInt32),
            new(GemIdentifierFamily.Alarm, SecsItemFormat.UInt64)
        ]);

        Assert.IsType<SecsUInt8Item>(policy.CreateItem(GemIdentifierFamily.CollectionEvent, 1));
        Assert.IsType<SecsUInt16Item>(policy.CreateItem(GemIdentifierFamily.Report, 1));
        Assert.IsType<SecsUInt32Item>(policy.CreateItem(GemIdentifierFamily.Variable, 1));
        Assert.IsType<SecsUInt64Item>(policy.CreateItem(GemIdentifierFamily.Alarm, 1));
        Assert.Equal(byte.MaxValue, policy.GetMaximum(GemIdentifierFamily.CollectionEvent));
        Assert.Equal(ushort.MaxValue, policy.GetMaximum(GemIdentifierFamily.Report));
        Assert.Equal(uint.MaxValue, policy.GetMaximum(GemIdentifierFamily.Variable));
        Assert.Equal(ulong.MaxValue, policy.GetMaximum(GemIdentifierFamily.Alarm));
        Assert.False(policy.IsValid(GemIdentifierFamily.Alarm, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => policy.GetFormat((GemIdentifierFamily)999));
        Assert.Throws<ArgumentException>(() => new GemIdentifierFormatPolicy(
            [new(GemIdentifierFamily.Report, SecsItemFormat.UInt8), new(GemIdentifierFamily.Report, SecsItemFormat.UInt16)]));
    }
}
