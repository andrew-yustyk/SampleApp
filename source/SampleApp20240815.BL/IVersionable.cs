using System;

namespace SampleApp20240815.BL;

public interface IVersionable
{
    public DateTimeOffset Version { get; }
}
