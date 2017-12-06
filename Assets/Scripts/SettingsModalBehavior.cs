using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uCPf;

public class SettingsModalBehavior : MonoBehaviour {

	public SettingsModalController controller;
	public GenerationSettings settings;
	public InputField totalTimeField;
	public InputField timeStepsField;
	public InputField polarStepsField;
	public ColorPicker colorPicker;

	public void populateFields()
	{
		totalTimeField.text = settings.totalTimeSeconds.ToString();
		timeStepsField.text = settings.timeStepsPerSecond.ToString();
		polarStepsField.text = settings.polarSteps.ToString();

		colorPicker.color = settings.color;
		colorPicker.UpdateUI ();
	}

	public void setTotalTime(InputField totalTimeField)
	{
		settings.totalTimeSeconds = int.Parse(totalTimeField.text);
	}
	public void setTimeStepsPerSecond(InputField timeStepsField)
	{
		settings.timeStepsPerSecond = int.Parse(timeStepsField.text);
	}
	public void setPolarSteps(InputField polarStepsField)
	{
		settings.polarSteps = int.Parse(polarStepsField.text);
	}
		
	public void ok()
	{
		controller.HideModal ();
	}

}
