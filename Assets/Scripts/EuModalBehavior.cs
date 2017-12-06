using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EuModalBehavior : MonoBehaviour {

	EconomicUnit eu;
	GameObject euObj;
	public EuModalController controller;
	public InputField nameField;
	public InputField descriptionField;
	public InputField latencyField;


	public void SetEu(GameObject objWithEu) {
		euObj = objWithEu;
		eu = objWithEu.GetComponent<EconomicUnit>();
		populateFields ();
	}

	void populateFields()
	{
		nameField.text = eu.euname;
		descriptionField.text = eu.description;
		latencyField.text = eu.latency.ToString();

	}

	public void setEuName(InputField euNameField)
	{
		eu.euname = euNameField.text;
	}

	public void setEuDescription(InputField descriptionField)
	{
		eu.description = descriptionField.text;
	}

	public void setEuLatency(InputField latencyField)
	{
		eu.latency = float.Parse (latencyField.text);
	}

	public void saveEconomicUnit()
	{
		euObj.GetComponent<EconomicUnit>().saveEu(eu);
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
