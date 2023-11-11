using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPressTutorial : MonoBehaviour, IPointerDownHandler
{
	[SerializeField] private GameObject pressTutorial;
	
	[NonSerialized] private bool alreadyPressed;
	
	private string prefsName => String.Concat(name, pressTutorial.name, "_tutorialPassed");
	
	
	protected virtual void OnEnable()
	{
		if (PlayerPrefs.GetInt(prefsName, 0) == 1)
		{
			alreadyPressed = true;
			pressTutorial.SetActive(false);
		}
		else
		{
			pressTutorial.SetActive(true);
		}
	}

	private void OnDisable()
	{
		pressTutorial.SetActive(false);
	}

	public virtual void OnPointerDown(PointerEventData eventData)
	{
		if(alreadyPressed == true || gameObject.activeInHierarchy == false) return;
		PlayerPrefs.SetInt(prefsName, 1);
		pressTutorial.SetActive(false);
	}
}
