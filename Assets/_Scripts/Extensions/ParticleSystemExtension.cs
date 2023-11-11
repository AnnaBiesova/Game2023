using UnityEngine;

namespace _Scripts.Extensions
{
	public static class ParticleSystemExtension
	{
		public static void Activate(this ParticleSystem system, bool state)
		{
			if(system == null) return;

			switch (state)
			{
				case true:
					system.gameObject.SetActive(true);
					system.Play();
					break;
				case false:
					system.Stop();
					break;
			}
		}

		public static void Activate(this ParticleSystem[] systems, bool state)
		{
			if(systems == null || systems.Length == 0) return;

			foreach (ParticleSystem system in systems)
			{
				Activate(system, state);
			}
		}
	}
}