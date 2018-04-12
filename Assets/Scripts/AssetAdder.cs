using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AssetAdder : MonoBehaviour {

	public ModalController modalController;
	public GameObject devicePrefab;

	public void AddAsset()
	{
		GameObject newDevice = Instantiate (devicePrefab, transform);
		newDevice.GetComponent<Asset> ().active = true;
		//open modal window linking new prefab 
		modalController.ShowModal(newDevice, true);

	}

	public void LoadAsset(AssetSavable savedAsset)
	{
		GameObject newDevice = Instantiate (devicePrefab,transform);

		// sets the object name in unity's hierarchy
		newDevice.name = savedAsset.aname;

		Asset assetComponent = newDevice.GetComponent<Asset> ();

		// sets the asset parameters
		assetComponent.aname = savedAsset.aname;
		assetComponent.latency = savedAsset.latency;
		assetComponent.agilityP = savedAsset.agilityP;
		assetComponent.agilityQ = savedAsset.agilityQ;
		assetComponent.maxP = savedAsset.maxP;
		assetComponent.maxQ = savedAsset.maxQ;
		assetComponent.energy = savedAsset.energy;
		assetComponent.nameIndex = savedAsset.nameIndex;
		assetComponent.alpha1 = savedAsset.alpha1;
		assetComponent.alpha2 = savedAsset.alpha2;
		assetComponent.alpha3 = savedAsset.alpha3;

		assetComponent.beta1 = savedAsset.beta1;
		assetComponent.beta2 = savedAsset.beta2;
		assetComponent.beta3 = savedAsset.beta3;

		assetComponent.setActive(savedAsset.active);

		newDevice.GetComponentInChildren<Text> ().text = savedAsset.aname;

	}
}
