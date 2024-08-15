using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace SampleApp20240815.DAL.Db.Configuration;

public class DateTimeOffsetUtcGenerator : ValueGenerator<DateTimeOffset>
{
    public override DateTimeOffset Next(EntityEntry entry)
    {
        return DateTimeOffset.UtcNow;
    }

    public override bool GeneratesTemporaryValues => true;
}
