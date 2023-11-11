using System.Collections.Generic;
using System.Linq;

namespace _Scripts.MonoBehaviours.Patterns.EasyObjectPool.Core
{
	public static class PoolsNames
	{
		public static string MONEY_ON_LEVEL = "MONEY_ON_LEVEL";
		public static string MONEY_BLAST_FX = "MONEY_BLAST_FX";
		public static string WRITE_ON_WALL_TEXT = "WRITE_ON_WALL_TEXT";
		public static string OTHER_CHARACTERS = "OTHER_CHARACTERS";
		
		public static string LEFT_THOUGHT_BLOCK_UI = "LEFT_THOUGHT_BLOCK_UI";
		public static string RIGHT_THOUGHT_BLOCK_UI = "RIGHT_THOUGHT_BLOCK_UI";
		public static string THOUGHT_DUMMY_UI = "THOUGHT_DUMMY_UI";


		public static IEnumerable<string> GetAllPoolsNames()
		{
			return typeof(PoolsNames).GetFields().Select(fieldInfo => fieldInfo.Name);
		}
	}

	public static class MapBlockToPoolName
	{
		/*private static Dictionary<eMinigObject, string> blocksMap = new Dictionary<eMinigObject, string>()
		{
			{ eMinigObject.wood, PoolsNames.WOOD_BLOCKS },
			{ eMinigObject.stone, PoolsNames.STONE_BLOCKS },
			{ eMinigObject.iron, PoolsNames.IRON_BLOCKS },
			{ eMinigObject.gold, PoolsNames.GOLD_BLOCKS },
			{ eMinigObject.diamond, PoolsNames.DIAMOND_BLOCKS }
		};

		public static string GetPoolName(eMinigObject objectType)
		{
			if (blocksMap.ContainsKey(objectType) == false)
			{
				Debug.LogError("Block type is not represented in dictionary of pool names");
				return String.Empty;
			}

			return blocksMap[objectType];
		}*/
	}
}