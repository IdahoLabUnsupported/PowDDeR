﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateAround : MonoBehaviour {

	public float minX = -360.0f;
	public float maxX = 360.0f;

	public float minY = -45.0f;
	public float maxY = 45.0f;

	public float sensX = 10.0f;
	public float sensY = 10.0f;

	float rotSpeed = 20;

	public GameObject menu;
	public GameObject settingsMenu;

	bool enable = true;

	Quaternion initRot;

	void Start()
	{
		initRot = this.transform.rotation;

	}

	void Update ()
	{
		if (Input.GetMouseButton (0) && EventSystem.current.currentSelectedGameObject == null && enable && !menu.activeSelf && !settingsMenu.activeSelf) {
			float rotX = Input.GetAxis ("Mouse X") * rotSpeed * Mathf.Deg2Rad;
			float rotY = Input.GetAxis ("Mouse Y") * rotSpeed * Mathf.Deg2Rad;
			transform.RotateAround (Vector3.up, -rotX);
			transform.RotateAround (Vector3.right, rotY);
		}
	}

	public void resetRotation()
	{
		this.transform.rotation = initRot;
	}

	public void disableRotation()
	{
		enable = false;
	}

	public void enableRotation()
	{
		enable = true;
	}
}
 