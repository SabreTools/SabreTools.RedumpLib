using System;
#if NET40_OR_GREATER || NETCOREAPP
using System.Linq;
#endif

namespace SabreTools.RedumpLib.Attributes
{
    public static class AttributeHelper<T>
    {
        /// <summary>
        /// Get the HumanReadableAttribute from a supported value
        /// </summary>
        /// <param name="value">Value to use</param>
        /// <returns>HumanReadableAttribute attached to the value</returns>
        public static HumanReadableAttribute? GetAttribute(T value)
        {
            // Null value in, null value out
            if (value == null)
                return null;

            // Current enumeration type
            var enumType = typeof(T);
            if (Nullable.GetUnderlyingType(enumType) != null)
                enumType = Nullable.GetUnderlyingType(enumType);

            // If the value returns a null on ToString, just return null
            string? valueStr = value?.ToString();
            if (string.IsNullOrEmpty(valueStr))
                return null;

            // Get the member info array
            var memberInfos = enumType?.GetMember(valueStr);
            if (memberInfos == null)
                return null;

            // Get the enum value info from the array, if possible
#if NET20 || NET35
            System.Reflection.MemberInfo? enumValueMemberInfo = null;
            foreach (var m in memberInfos)
            {
                if (m.DeclaringType != enumType)
                    continue;

                enumValueMemberInfo = m;
                break;
            }
#else
            var enumValueMemberInfo = memberInfos.FirstOrDefault(m => m.DeclaringType == enumType);
#endif
            if (enumValueMemberInfo == null)
                return null;

            // Try to get the relevant attribute
            var attributes = enumValueMemberInfo.GetCustomAttributes(typeof(HumanReadableAttribute), true);
            if (attributes == null || attributes.Length == 0)
                return null;

            // Return the first attribute, if possible
            return attributes[0] as HumanReadableAttribute;
        }
    }
}