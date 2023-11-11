using Cinemachine;

namespace _Scripts.Utils.Camera
{
	public static class CinemachineVirtualCameraExtensions
	{
		public static void CopyComponentSettings<T>(this CinemachineVirtualCamera source, CinemachineVirtualCamera target)
			where T : CinemachineComponentBase
		{
			T sourceComponent = source.GetCinemachineComponent<T>();
			T targetComponent = target.AddCinemachineComponent<T>();

			if (sourceComponent != null && targetComponent != null)
			{
				System.Type type = typeof(T);
				System.Reflection.FieldInfo[] fields = type.GetFields();
				foreach (System.Reflection.FieldInfo field in fields)
				{
					field.SetValue(targetComponent, field.GetValue(sourceComponent));
				}

				var props = type.GetProperties();
				foreach (var prop in props)
				{
					if (!prop.CanWrite || !prop.CanWrite || prop.Name == "name") continue;
					prop.SetValue(targetComponent, prop.GetValue(sourceComponent, null), null);
				}
			}
		}
	}
}