using UnityEngine;
using System.Reflection;
using System.ComponentModel;
using System; // これが必要です

public static class EnumExtensions
{
    public static string GetDescription<T>(this T enumValue) where T : Enum
    {
        FieldInfo field = enumValue.GetType().GetField(enumValue.ToString());

        if (field != null)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
        }
        return enumValue.ToString(); // Description属性がない場合はToString()の結果を返す
    }
}