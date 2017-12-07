using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EuLinker : MonoBehaviour {

	// this script registers the correct asset from the parent to send to the modal window and show the modal window for economic unit editing

	// Use this for initialization
	void Start () {
		
		Button b = GetComponent<Button> ();
		b.onClick.AddListener (() => transform.root.GetComponent<EuModalController> ().ShowModal (transform.parent.gameObject));
	}
}

