using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModalBehavior : MonoBehaviour {

	Asset asset;
	GameObject assetObj;
	public ModalController controller;
	public InputField nameField;
	public InputField latencyField;
	public InputField agilityPField;
	public InputField agilityQField;
	public InputField maxPField;
	public InputField maxQField;
	public InputField energyField;
	public Toggle activeToggle;

	// temporary holder for edited values
	public AssetSavable assetEdit;

	public void SetAsset(GameObject objWithAsset) {
		assetObj = objWithAsset;
		asset = objWithAsset.GetComponent<Asset>();
	
		populateFields ();
	}

	void populateFields()
	{
		nameField.text = asset.aname.ToString();
		latencyField.text = asset.latency.ToString();
		agilityPField.text = asset.agilityP.ToString();
		agilityQField.text = asset.agilityQ.ToString();
		maxPField.text = asset.maxP.ToString();
		maxQField.text = asset.maxQ.ToString();
		energyField.text = asset.energy.ToString();
		activeToggle.isOn = asset.active;
	}


	public void setAssetName(InputField assetNameField)
	{
		assetEdit.aname = assetNameField.text;
	}
	public void setAssetLatency(InputField assetLatencyField)
	{
		assetEdit.latency = float.Parse(assetLatencyField.text);
	}
	public void setAssetAgilityP(InputField assetAgilityPField)
	{
		assetEdit.agilityP = float.Parse(assetAgilityPField.text);
	}
	public void setAssetAgilityQ(InputField assetAgilityQField)
	{
		assetEdit.agilityQ = float.Parse(assetAgilityQField.text);
	}
	public void setAssetMaxP(InputField assetMaxPField)
	{
		assetEdit.maxP = float.Parse(assetMaxPField.text);
	}
	public void setAssetMaxQ(InputField assetMaxQField)
	{
		assetEdit.maxQ = float.Parse(assetMaxQField.text);
	}
	public void setAssetEnergy(InputField assetEnergyField)
	{
		assetEdit.energy = float.Parse(assetEnergyField.text);
	}
	public void setAssetActive(bool assetActive)
	{
		assetEdit.active = assetActive;
	}


	public void saveAsset()
	{
		asset.aname = assetEdit.aname;
		asset.latency = assetEdit.latency;
		asset.agilityP = assetEdit.agilityP;
		asset.agilityQ = assetEdit.agilityQ;
		asset.maxP = assetEdit.maxP;
		asset.maxQ = assetEdit.maxQ;
		asset.energy = assetEdit.energy;
		asset.active = activeToggle.isOn;

		assetObj.GetComponent<Asset>().saveAsset(asset);
		assetObj.GetComponentInChildren<Toggle> ().isOn = asset.active;
		controller.HideModal ();
	}

	public void cancelAddAsset()
	{
		DestroyImmediate (assetObj);
		controller.HideModal ();
	}

	public void cancelAsset ()
	{
		controller.HideModal ();	
	}
}
