using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace SampleApp20240815.DAL.Db.Configuration;

public class DateTimeOffsetUtcPermanentGenerator : ValueGenerator<DateTimeOffset>
{
    public sealed override bool GeneratesTemporaryValues => false;

    public override DateTimeOffset Next(EntityEntry entry)
    {
        return DateTimeOffset.UtcNow;
    }
}

public class DateTimeOffsetUtcTemporaryGenerator : ValueGenerator<DateTimeOffset>
{
    public sealed override bool GeneratesTemporaryValues => true;

    public override DateTimeOffset Next(EntityEntry entry)
    {
        return DateTimeOffset.UtcNow;
    }
}
