using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Utils.WeightedRandom
{
	public static class WeightedRandom
	{
		[Serializable]
		public class OptionAndWeight<T>
		{
			public T option;
			public float weight;

			public OptionAndWeight(T option, float weight)
			{
				this.option = option;
				this.weight = weight;
			}
		}
		
		public static T GetRandomWeightedObject<T>(List<OptionAndWeight<T>> optionAndWeights, bool removeOption = false)
		{
			float totalWeight = 0f;

			foreach (OptionAndWeight<T> option in optionAndWeights)
			{
				totalWeight += option.weight;
			}
			
			float randomValue = Random.Range(0f, totalWeight);

			for (int i = optionAndWeights.Count - 1; i >= 0; i--)
			{
				OptionAndWeight<T> option = optionAndWeights[i];
				
				if (randomValue < option.weight)
				{
					if(removeOption) optionAndWeights.RemoveAt(i);
					return option.option;
				}

				randomValue -= option.weight;
			}
			
			return default;
		}
	}
}