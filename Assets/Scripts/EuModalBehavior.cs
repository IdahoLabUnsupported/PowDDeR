
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
public class EuModalBehavior : MonoBehaviour {

	EconomicUnit euOrig;
	GameObject euObj;
	public EuModalController controller;
	public InputField nameField;
	public InputField descriptionField;
	public InputField latencyField;
	public Toggle activeToggle;

	string euNameNew;
	float euLatencyNew;
	string euDescriptionNew;
	bool euActiveNew = true;

	public void SetEu(GameObject objWithEu) {
		euObj = objWithEu;
		euOrig = objWithEu.GetComponent<EconomicUnit>();
		populateFields ();
	}

	void populateFields()
	{
		nameField.text = euOrig.euname;
		descriptionField.text = euOrig.description;
		latencyField.text = euOrig.latency.ToString();
		activeToggle.isOn = euOrig.active;
	}

	public void setEuName(InputField euNameField)
	{
		euNameNew = euNameField.text;
	}

	public void setEuDescription(InputField descriptionField)
	{
		euDescriptionNew = descriptionField.text;
	}

	public void setEuLatency(InputField latencyField)
	{
		euLatencyNew = float.Parse (latencyField.text);
	}

	public void setEuActive(bool active)
	{
		euActiveNew = active;
	}

	public void saveEconomicUnit()
	{
		euOrig.euname = euNameNew;
		euOrig.latency = euLatencyNew;
		euOrig.description = euDescriptionNew;
		euOrig.active = activeToggle.isOn;

		euObj.GetComponent<EconomicUnit>().saveEu(euOrig);
		euObj.GetComponentInChildren<Toggle> ().isOn = euOrig.active;
		controller.HideModal ();
	}

	public void cancelAddEu()
	{
		DestroyImmediate (euObj);
		controller.HideModal ();
	}

	public void cancelEu ()
	{
		
		controller.HideModal ();	
	}
}
