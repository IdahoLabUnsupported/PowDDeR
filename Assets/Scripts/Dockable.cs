using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dockable : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler {

	public GameObject euDevicePrefab;
	Color startColor; 
	public Transform euDeviceContainer;

	public void OnPointerEnter( PointerEventData eventData)
	{
		if (Draggable.draggedItem != null) {
			startColor = GetComponent<Image> ().color;
			GetComponent<Image> ().color = Color.green;
		}
	}

	public void OnDrop (PointerEventData eventData)
	{
		if (Draggable.draggedItem != null) {
			GetComponent<Image> ().color = startColor;
			GetComponent<EconomicUnit> ().addAsset (ref Draggable.originalItem, 1);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (Draggable.draggedItem != null) {
			GetComponent<Image> ().color = startColor;
		}
	}
}
