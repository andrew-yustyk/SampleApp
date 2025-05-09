using Xunit;

namespace SampleApp20240815.Tests.Unit;

public class DummyTests
{
    [Fact]
    public void Should_Assert_True()
    {
        const bool expectedValue = true;
        Assert.True(expectedValue);
    }

    [Fact]
    public void Should_Assert_False()
    {
        const bool unexpectedValue = false;
        Assert.False(unexpectedValue);
    }
}
