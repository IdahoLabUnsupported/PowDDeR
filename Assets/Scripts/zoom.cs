using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoom : MonoBehaviour {

	public float scaleSens = 0.1f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Mouse ScrollWheel") > 0) { 
			transform.localScale = new Vector3 (transform.localScale.x + scaleSens, transform.localScale.y + scaleSens, transform.localScale.z + scaleSens);
		}

		if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			transform.localScale = new Vector3 (transform.localScale.x - scaleSens, transform.localScale.y - scaleSens, transform.localScale.z - scaleSens);
		}
	}
}
