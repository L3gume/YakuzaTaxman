using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

	public Text text;

	public void OnPointerEnter(PointerEventData eventData)
	{
		text.color = Color.white;
	}
	
	public void OnPointerExit(PointerEventData eventData)
	{
		Color col = new Color();
		ColorUtility.TryParseHtmlString("#D02222FF", out col);
		text.color = col;
	}
	
}
