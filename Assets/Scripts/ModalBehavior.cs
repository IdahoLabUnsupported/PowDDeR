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
	}


	public void setAssetName(InputField assetNameField)
	{
		asset.aname = assetNameField.text;
	}
	public void setAssetLatency(InputField assetLatencyField)
	{
		asset.latency = float.Parse(assetLatencyField.text);
	}
	public void setAssetAgilityP(InputField assetAgilityPField)
	{
		asset.agilityP = float.Parse(assetAgilityPField.text);
	}
	public void setAssetAgilityQ(InputField assetAgilityQField)
	{
		asset.agilityQ = float.Parse(assetAgilityQField.text);
	}
	public void setAssetMaxP(InputField assetMaxPField)
	{
		asset.maxP = float.Parse(assetMaxPField.text);
	}
	public void setAssetMaxQ(InputField assetMaxQField)
	{
		asset.maxQ = float.Parse(assetMaxQField.text);
	}
	public void setAssetEnergy(InputField assetEnergyField)
	{
		asset.energy = float.Parse(assetEnergyField.text);
	}


	public void saveAsset()
	{
		assetObj.GetComponent<Asset>().saveAsset(asset);
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
