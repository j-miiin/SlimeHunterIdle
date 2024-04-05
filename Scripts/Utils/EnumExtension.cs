using System;
using System.Collections.Generic;

public static class EnumExtension
{
    private static readonly Dictionary<Enum, string> _enumToStringCacheDic = new Dictionary<Enum, string>();

    public static string ToStringEx(this Enum value)
    {
        if (!_enumToStringCacheDic.ContainsKey(value)) _enumToStringCacheDic.Add(value, value.ToString());
        return _enumToStringCacheDic[value];
    }
}
