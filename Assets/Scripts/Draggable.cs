using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public static GameObject draggedItem;
	Vector3 startPosition;
	public static GameObject originalItem;

	public void OnBeginDrag(PointerEventData eventData)
	{
		originalItem = gameObject;
		draggedItem = Instantiate(gameObject,GameObject.Find("Menu").transform);
		draggedItem.GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		draggedItem.transform.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		//GetComponent<CanvasGroup> ().blocksRaycasts = true;
		Destroy (draggedItem);
	}
}
