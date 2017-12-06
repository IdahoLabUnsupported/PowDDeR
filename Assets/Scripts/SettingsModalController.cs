using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsModalController : MonoBehaviour {

	public GameObject modal;

	public void ShowModal(){
		modal.SetActive (true);
		modal.GetComponent<SettingsModalBehavior> ().populateFields();
	}

	public void HideModal(){
		modal.SetActive (false);
	}

}
