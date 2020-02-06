using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Saturno.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static string ToName(this Enum value)
        {
            if (value == null)
                return string.Empty;

            var member = value.GetType()
                    .GetMember(value.ToString());

            var enumerador = member.FirstOrDefault();

            if (enumerador == null)
                return string.Empty;

            return enumerador.GetCustomAttribute<DisplayAttribute>().Name;
        }
    }
}
