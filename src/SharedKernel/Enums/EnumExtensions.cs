using SharedKernel;
using System.ComponentModel;

namespace System
{
    public static class EnumExtensions
    {
        /// <summary>
        /// If the value is marked with <see cref="DescriptionAttribute"/>, return that description.
        /// Otherwise return the name of enum value.
        /// </summary>
        /// <param name="enumValue">Enum value to process.</param>
        /// <returns>The description or name of enum value.</returns>
        public static string GetNameOrDescription(this Enum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());

            if (field.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && 
                attributes.Length > 0)
                return attributes[0].Description;

            return enumValue.ToString();
        }

        /// <summary>
        /// Convert an enum value to an <see cref="EnumDto"/> object.
        /// </summary>
        /// <param name="enumValue">Enum value to convert.</param>
        /// <returns></returns>
        public static EnumDto ToEnumDto(this Enum enumValue)
        {
            return new EnumDto(enumValue);
        }

        public static List<EnumDto> ToEnumDtoList<TEnum>(this TEnum enumValue)
            where TEnum : struct, Enum
        {
            return Enum.GetValues<TEnum>()
                .Select(x => new EnumDto(x))
                .ToList();

        }
    }
}