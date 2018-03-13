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
