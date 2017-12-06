using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uCPf;

public class SettingsModalBehavior : MonoBehaviour {

	public SettingsModalController controller;
	public SetGenerationSettings settings;
	public InputField totalTimeField;
	public InputField timeStepsField;
	public InputField polarStepsField;
	public ColorPicker colorPicker;

	bool updateColorPicker = false;

	public void populateFields()
	{
		totalTimeField.text = settings.currentSettings.totalTimeSeconds.ToString();
		timeStepsField.text = settings.currentSettings.timeStepsPerSecond.ToString();
		polarStepsField.text = settings.currentSettings.polarSteps.ToString();
		colorPicker.color = settings.currentSettings.color;
		updateColorPicker = true;
	}

	public void setTotalTime(InputField totalTimeField)
	{
		settings.currentSettings.totalTimeSeconds = int.Parse(totalTimeField.text);
	}
	public void setTimeStepsPerSecond(InputField timeStepsField)
	{
		settings.currentSettings.timeStepsPerSecond = int.Parse(timeStepsField.text);
	}
	public void setPolarSteps(InputField polarStepsField)
	{
		settings.currentSettings.polarSteps = int.Parse(polarStepsField.text);
	}
		
	public void ok()
	{
		controller.HideModal ();
	}


	void LateUpdate()
	{
		if (updateColorPicker) {
			colorPicker.UpdateUI ();
			updateColorPicker = false;
		}
	}
}
