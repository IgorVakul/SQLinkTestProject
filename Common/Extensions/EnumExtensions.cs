using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class EnumExtensions
    {
        public static string? GetDisplayName(this ErrorCodes enu)
        {
            var attr = GetDisplayAttribute(enu);
            return attr != null ? attr.Name : enu.ToString();
        }

        public static string? GetDescription(this ErrorCodes enu)
        {
            var attr = GetDisplayAttribute(enu);
            return attr != null ? attr.Description : enu.ToString();
        }

        private static DisplayAttribute? GetDisplayAttribute(object value)
        {
            DisplayAttribute? Result = null;
            if (value != null)
            {
                string? fieldName = value?.ToString();
                if (!string.IsNullOrEmpty(fieldName))
                {
                    FieldInfo? field = value?.GetType()?.GetField(fieldName);
                    Result = field?.GetCustomAttribute<DisplayAttribute>();
                }
            }
            return Result;
        }
    }
}
