using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGenerationSettings : MonoBehaviour {

	public GenerationSettings currentSettings;

	public void setSettings(GenerationSettings inSettings)
	{
		currentSettings = inSettings;
	}

	public GenerationSettings GetSettings()
	{
		return currentSettings;
	}
}
