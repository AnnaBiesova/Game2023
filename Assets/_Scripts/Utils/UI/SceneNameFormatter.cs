using System;
using UnityEngine;

namespace _Scripts.Utils.UI
{
	[Serializable]
	public class SceneNameFormatter
	{
		[Serializable]
		public enum eNameSize
		{
			none,
			toLower,
			loUpper,
		}
        
		public char separator = '_';
		public int namePartToDisplay = 0;
		public eNameSize size;

		public string GetFormattedName(string defaultName)
		{
			string sceneName = defaultName.Split(separator)[namePartToDisplay];

			switch (size)
			{
				case eNameSize.toLower:
					return sceneName.ToLower();
				case eNameSize.loUpper:
					return sceneName.ToUpper();
				case eNameSize.none:
					throw new ArgumentOutOfRangeException(paramName: "eNameSize", "Wrong text size!");
			}

			return null;
		}
	}
}