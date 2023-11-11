using System.Linq;
using UnityEngine;

namespace _Scripts.Utils.Renderers
{
	public class MaterialToRendererSetter : MonoBehaviour
	{
		[SerializeField] private Renderer rendererToSetMaterial;
		[SerializeField] private int[] materialsIndexes;
		
		private static readonly int Clr = Shader.PropertyToID("_Clr");
		private static readonly int Color = Shader.PropertyToID("_Color");

		private bool isPlayer;
		
		public void SetMaterial(Material material, bool isPlayer)
		{
			this.isPlayer = isPlayer;
			Material[] rendererMaterials = rendererToSetMaterial.materials;
			
			for (int i = 0; i < rendererMaterials.Length; i++)
			{
				if (materialsIndexes.Contains(i))
				{
					rendererMaterials[i] = material;
				}
			}

			rendererToSetMaterial.materials = rendererMaterials;
		}

		public void SetColor(Color color)
		{
			int propertyId = isPlayer ? Clr : Color;
			
			for (int i = 0; i < rendererToSetMaterial.materials.Length; i++)
			{
				if (materialsIndexes.Contains(i))
				{
					rendererToSetMaterial.materials[i].SetColor(propertyId, color);
				}
			}
		}
	}
}