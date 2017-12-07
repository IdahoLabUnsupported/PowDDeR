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

	string euNameNew;
	float euLatencyNew;
	string euDescriptionNew;

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

	public void saveEconomicUnit()
	{
		euOrig.euname = euNameNew;
		euOrig.latency = euLatencyNew;
		euOrig.description = euDescriptionNew;
		euObj.GetComponent<EconomicUnit>().saveEu(euOrig);
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
