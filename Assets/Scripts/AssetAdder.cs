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
		assetComponent.active = savedAsset.active;
		assetComponent.nameIndex = savedAsset.nameIndex;

		newDevice.GetComponentInChildren<Text> ().text = savedAsset.aname;

	}
}
