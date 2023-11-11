using System;
using System.Reflection;
using UnityEngine;

public static class ComponentExtensions
{
	public static bool TryCopyComponent<T>(Transform fromTransform, Transform toTransform) where T : Component
	{
		if (fromTransform.TryGetComponent(out T tFrom))
		{
			if (toTransform.TryGetComponent(out T tTo))
			{
				Type copyType = typeof(T);

				PropertyInfo[] propertyInfos = copyType.GetProperties();
				FieldInfo[] fieldInfos = copyType.GetFields();

				foreach (PropertyInfo propertyInfo in propertyInfos)
				{
					if (propertyInfo.CanWrite)
					{
						propertyInfo.SetValue(tTo, propertyInfo.GetValue(tFrom, null));
					}
				}

				foreach (FieldInfo fieldInfo in fieldInfos)
				{
					fieldInfo.SetValue(tTo, fieldInfo.GetValue(tFrom));
				}

				return true;
			}

			return false;
		}

		return false;
	}
}