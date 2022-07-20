using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace DAL.Extensions
{
    public static class DbContextExtensions1
    {
        public static void InitDateTimeConverter(this ModelBuilder modelBuilder)
        {
            ValueConverter dateTimeConverter = new ValueConverter<DateTime, DateTime>(
                value => value.ToUniversalTime(),
                value => DateTime.SpecifyKind(value, DateTimeKind.Utc));

            ValueConverter nullableDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
                value => value.HasValue ? value.GetValueOrDefault().ToUniversalTime() : value,
                value => value.HasValue ? DateTime.SpecifyKind(value.GetValueOrDefault(), DateTimeKind.Utc) : value);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime))
                    {
                        property.SetValueConverter(dateTimeConverter);
                    }

                    else if (property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(nullableDateTimeConverter);
                    }
                }
            }
        }
    }
}
