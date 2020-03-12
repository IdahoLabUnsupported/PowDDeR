
/*
© 2020 Battelle Energy Alliance, LLC
ALL RIGHTS RESERVED

Prepared by Battelle Energy Alliance, LLC
Under Contract No.DE-AC07-05ID14517
With the U. S.Department of Energy

NOTICE:  This computer software was prepared by Battelle Energy
Alliance, LLC, hereinafter the Contractor, under Contract
No.AC07-05ID14517 with the United States (U.S.) Department of
Energy (DOE).  The Government is granted for itself and others acting on
its behalf a nonexclusive, paid-up, irrevocable worldwide license in this
data to reproduce, prepare derivative works, and perform publicly and
display publicly, by or on behalf of the Government.There is provision for
the possible extension of the term of this license.Subsequent to that
period or any extension granted, the Government is granted for itself and
others acting on its behalf a nonexclusive, paid-up, irrevocable worldwide
license in this data to reproduce, prepare derivative works, distribute
copies to the public, perform publicly and display publicly, and to permit
others to do so.The specific term of the license can be identified by
inquiry made to Contractor or DOE.NEITHER THE UNITED STATES NOR THE UNITED
STATES DEPARTMENT OF ENERGY, NOR CONTRACTOR MAKES ANY WARRANTY, EXPRESS OR
IMPLIED, OR ASSUMES ANY LIABILITY OR RESPONSIBILITY FOR THE USE, ACCURACY,
COMPLETENESS, OR USEFULNESS OR ANY INFORMATION, APPARATUS, PRODUCT, OR
PROCESS DISCLOSED, OR REPRESENTS THAT ITS USE WOULD NOT INFRINGE PRIVATELY
OWNED RIGHTS.

Authors:
Tim McJunkin
Craig Rieger
Thomas Szewczyk
James Money
Randall Reese
*/



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
	public ColorPicker colorPickerBg;
	public ColorPicker colorPickerLine;

	bool updateColorPicker = false;
	bool updateColorPickerBg = false;
	bool updateColorPickerLine = false;

	public void populateFields()
	{
		totalTimeField.text = settings.currentSettings.totalTimeSeconds.ToString();
		timeStepsField.text = settings.currentSettings.timeStepsPerSecond.ToString();
		polarStepsField.text = settings.currentSettings.polarSteps.ToString();
		colorPicker.color = settings.currentSettings.color;
		if (settings.currentSettings.bgColor != null) {
			colorPickerBg.color = settings.currentSettings.bgColor;
		} else {
			colorPickerBg.color = Color.black;
		}

		if (settings.currentSettings.lineColor != null) {
			colorPickerLine.color = settings.currentSettings.lineColor;
		} else {
			colorPickerLine.color = Color.yellow;
		}
		// apparently to update color picker, it has to happen in late update
		updateColorPicker = true;
		updateColorPickerBg = true;
		updateColorPickerLine = true;
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
		if (updateColorPickerBg) {
			colorPickerBg.UpdateUI ();
			updateColorPickerBg = false;
		}
		if (updateColorPickerLine) {
			colorPickerBg.UpdateUI ();
			updateColorPickerLine = false;
		}
	}
}
