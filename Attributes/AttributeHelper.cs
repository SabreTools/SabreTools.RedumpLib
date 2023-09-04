using System;
using System.Linq;

namespace SabreTools.RedumpLib.Attributes
{
    public static class AttributeHelper<T>
    {
        /// <summary>
        /// Get the HumanReadableAttribute from a supported value
        /// </summary>
        /// <param name="value">Value to use</param>
        /// <returns>HumanReadableAttribute attached to the value</returns>
#if NET48
        public static HumanReadableAttribute GetAttribute(T value)
#else
        public static HumanReadableAttribute? GetAttribute(T value)
#endif
        {
            // Null value in, null value out
            if (value == null)
                return null;

            // Current enumeration type
            var enumType = typeof(T);
            if (Nullable.GetUnderlyingType(enumType) != null)
                enumType = Nullable.GetUnderlyingType(enumType);

            // If the value returns a null on ToString, just return null
#if NET48
            string valueStr = value?.ToString();
#else
            string? valueStr = value?.ToString();
#endif
            if (string.IsNullOrWhiteSpace(valueStr))
                return null;
            
            // Get the member info array
            var memberInfos = enumType?.GetMember(valueStr);
            if (memberInfos == null)
                return null;

            // Get the enum value info from the array, if possible
            var enumValueMemberInfo = memberInfos.FirstOrDefault(m => m.DeclaringType == enumType);
            if (enumValueMemberInfo == null)
                return null;
            
            // Try to get the relevant attribute
            var attributes = enumValueMemberInfo.GetCustomAttributes(typeof(HumanReadableAttribute), true);
            if (attributes == null)
                return null;
            
            // Return the first attribute, if possible
            return attributes.FirstOrDefault() as HumanReadableAttribute;
        }
    }
}