﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GenerationSettings : MonoBehaviour {

	public int totalTimeSeconds = 1700;
	public int timeStepsPerSecond = 10;
	public int polarSteps = 100;
	public Color color;
}