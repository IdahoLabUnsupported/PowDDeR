using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EuModalController : MonoBehaviour {

	public GameObject modal;
	public Button cancelButton;

	public void ShowModal(GameObject euObj, bool deleteOnCancel = false){
		modal.SetActive (true);
		modal.GetComponent<EuModalBehavior> ().SetEu (euObj);

		if (deleteOnCancel) {
			cancelButton.onClick.AddListener (() => modal.GetComponent<EuModalBehavior> ().cancelAddEu ());

		} else {
			cancelButton.onClick.AddListener (() => modal.GetComponent<EuModalBehavior> ().cancelEu ());
		}
	}

	public void HideModal(){
		cancelButton.onClick.RemoveAllListeners ();
		modal.SetActive (false);
	}


}
