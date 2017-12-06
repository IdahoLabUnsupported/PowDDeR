using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ModalController : MonoBehaviour {
	public GameObject modal;
	public Button cancelButton;

	public void ShowModal(GameObject assetObj, bool deleteOnCancel = false){
		modal.SetActive (true);
		modal.GetComponent<ModalBehavior> ().SetAsset (assetObj);

		if (deleteOnCancel) {

			cancelButton.onClick.AddListener (() => modal.GetComponent<ModalBehavior> ().cancelAddAsset ());
	
		} else {
			cancelButton.onClick.AddListener (() => modal.GetComponent<ModalBehavior> ().cancelAsset ());
		}
	}
		
	public void HideModal(){
		cancelButton.onClick.RemoveAllListeners ();
		modal.SetActive (false);
	}


}
