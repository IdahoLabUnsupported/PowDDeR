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

	public InputField pAlpha1Field;
	public InputField pAlpha2Field;
	public InputField pAlpha3Field;
	public InputField pBeta1Field;
	public InputField pBeta2Field;
	public InputField pBeta3Field;

	public InputField qAlpha1Field;
	public InputField qAlpha2Field;
	public InputField qAlpha3Field;
	public InputField qBeta1Field;
	public InputField qBeta2Field;
	public InputField qBeta3Field;

	public Toggle activeToggle;

	public Toggle loadToggle;

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

		pAlpha1Field.text = asset.palpha1.ToString();
		pAlpha2Field.text = asset.palpha2.ToString();
		pAlpha3Field.text = asset.palpha3.ToString();
		pBeta1Field.text = asset.pbeta1.ToString();
		pBeta2Field.text = asset.pbeta2.ToString();
		pBeta3Field.text = asset.pbeta3.ToString();

		qAlpha1Field.text = asset.qalpha1.ToString();
		qAlpha2Field.text = asset.qalpha2.ToString();
		qAlpha3Field.text = asset.qalpha3.ToString();
		qBeta1Field.text = asset.qbeta1.ToString();
		qBeta2Field.text = asset.qbeta2.ToString();
		qBeta3Field.text = asset.qbeta3.ToString();

		loadToggle.isOn = asset.load;
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

	// p transfer
	public void setAssetPAlpha1(InputField assetAlpha1Field)
	{
		assetEdit.palpha1 = float.Parse(assetAlpha1Field.text);
	}
	public void setAssetPAlpha2(InputField assetAlpha2Field)
	{
		assetEdit.palpha2 = float.Parse(assetAlpha2Field.text);
	}
	public void setAssetPAlpha3(InputField assetAlpha3Field)
	{
		assetEdit.palpha3 = float.Parse(assetAlpha3Field.text);
	}

	public void setAssetPBeta1(InputField assetBeta1Field)
	{
		assetEdit.pbeta1 = float.Parse(assetBeta1Field.text);
	}
	public void setAssetPBeta2(InputField assetBeta2Field)
	{
		assetEdit.pbeta2 = float.Parse(assetBeta2Field.text);
	}
	public void setAssetPBeta3(InputField assetBeta3Field)
	{
		assetEdit.pbeta3 = float.Parse(assetBeta3Field.text);
	}


	//q transfer
	public void setAssetQAlpha1(InputField assetAlpha1Field)
	{
		assetEdit.qalpha1 = float.Parse(assetAlpha1Field.text);
	}
	public void setAssetQAlpha2(InputField assetAlpha2Field)
	{
		assetEdit.qalpha2 = float.Parse(assetAlpha2Field.text);
	}
	public void setAssetQAlpha3(InputField assetAlpha3Field)
	{
		assetEdit.qalpha3 = float.Parse(assetAlpha3Field.text);
	}

	public void setAssetQBeta1(InputField assetBeta1Field)
	{
		assetEdit.qbeta1 = float.Parse(assetBeta1Field.text);
	}
	public void setAssetQBeta2(InputField assetBeta2Field)
	{
		assetEdit.qbeta2 = float.Parse(assetBeta2Field.text);
	}
	public void setAssetQBeta3(InputField assetBeta3Field)
	{
		assetEdit.qbeta3 = float.Parse(assetBeta3Field.text);
	}

	public void setAssetLoad(bool assetLoad)
	{
		assetEdit.load = assetLoad;
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

		asset.palpha1 = assetEdit.palpha1;
		asset.palpha2 = assetEdit.palpha2;
		asset.palpha3 = assetEdit.palpha3;
		asset.pbeta1 = assetEdit.pbeta1;
		asset.pbeta2  = assetEdit.pbeta2;
		asset.pbeta3  = assetEdit.pbeta3;

		asset.qalpha1 = assetEdit.qalpha1;
		asset.qalpha2 = assetEdit.qalpha2;
		asset.qalpha3 = assetEdit.qalpha3;
		asset.qbeta1 = assetEdit.qbeta1;
		asset.qbeta2 = assetEdit.qbeta2;
		asset.qbeta3 = assetEdit.qbeta3;

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
