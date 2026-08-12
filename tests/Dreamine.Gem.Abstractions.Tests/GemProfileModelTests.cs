using Dreamine.Gem.Abstractions.Model;
using Dreamine.Secs.Abstractions.Model;
using Xunit;

namespace Dreamine.Gem.Abstractions.Tests;

public sealed class GemProfileModelTests
{
    [Fact]
    public void IdentifierFormatsDefaultToU4AndValidateOverrides()
    {
        var policy = new GemIdentifierFormatPolicy(
        [
            new(GemIdentifierFamily.Alarm, SecsItemFormat.UInt8),
            new(GemIdentifierFamily.Report, SecsItemFormat.UInt64)
        ]);

        Assert.Equal(SecsItemFormat.UInt32, policy.GetFormat(GemIdentifierFamily.CollectionEvent));
        Assert.Equal(SecsItemFormat.UInt8, policy.GetFormat(GemIdentifierFamily.Alarm));
        Assert.Equal(SecsItemFormat.UInt64, policy.GetFormat(GemIdentifierFamily.Report));
        Assert.Equal(SecsItemFormat.UInt32, policy.GetFormat(GemIdentifierFamily.DataIdentifier));
        Assert.IsType<SecsUInt8Item>(policy.CreateItem(GemIdentifierFamily.Alarm, byte.MaxValue));
        Assert.Throws<ArgumentOutOfRangeException>(() => policy.CreateItem(GemIdentifierFamily.Alarm, 256));
        Assert.Throws<ArgumentException>(() => new GemIdentifierFormatPolicy(
            [new(GemIdentifierFamily.Alarm, SecsItemFormat.Ascii)]));
    }

    [Fact]
    public void IdentifierFormatSnapshotIsCompleteOrderedAndReadOnly()
    {
        var policy = new GemIdentifierFormatPolicy();

        Assert.Equal(Enum.GetValues<GemIdentifierFamily>(), policy.Formats.Keys);
        Assert.Throws<NotSupportedException>(() =>
            ((IDictionary<GemIdentifierFamily, SecsItemFormat>)policy.Formats)
                .Add((GemIdentifierFamily)999, SecsItemFormat.UInt32));
    }

    [Fact]
    public void TypedRemoteCommandParametersAreImmutableAndConcrete()
    {
        var parameter = new GemRemoteCommandParameterDefinition(
            "PORT",
            SecsItemFormat.Ascii,
            required: false,
            item => item is SecsAsciiItem ascii && ascii.Value.Length <= 8);
        var definition = new GemRemoteCommandProfileDefinition("START", [parameter]);

        Assert.False(definition.Parameters[0].Required);
        Assert.Equal(SecsItemFormat.Ascii, definition.Parameters[0].Format);
        Assert.Throws<ArgumentException>(() => new GemRemoteCommandProfileDefinition(
            "START",
            [parameter, new GemRemoteCommandParameterDefinition("PORT", SecsItemFormat.Ascii)]));
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new GemRemoteCommandParameterDefinition("X", (SecsItemFormat)63));
    }

    [Fact]
    public void StructuredEventSnapshotPreservesReportAndVariableOrder()
    {
        var first = new GemReportValueSnapshot(10,
        [
            new GemVariableValueSnapshot(2, new SecsUInt8Item(2)),
            new GemVariableValueSnapshot(1, new SecsUInt8Item(1))
        ]);
        var second = new GemReportValueSnapshot(11,
        [
            new GemVariableValueSnapshot(1, new SecsUInt8Item(1))
        ]);

        var snapshot = new GemEventSnapshot(20, DateTimeOffset.UnixEpoch, [first, second]);

        Assert.Equal(new ulong[] { 10, 11 }, snapshot.Reports.Select(static report => report.ReportId));
        Assert.Equal(new ulong[] { 2, 1 }, snapshot.Reports[0].Values.Select(static value => value.VariableId));
        Assert.Equal(2, snapshot.Reports.Sum(static report => report.Values.Count(static value => value.VariableId == 1)));
        Assert.Equal(new ulong[] { 1, 2 }, snapshot.Values.Keys.OrderBy(static id => id));
    }
}
