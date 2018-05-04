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

		assetComponent.palpha1 = savedAsset.palpha1;
		assetComponent.palpha2 = savedAsset.palpha2;
		assetComponent.palpha3 = savedAsset.palpha3;

		assetComponent.pbeta1 = savedAsset.pbeta1;
		assetComponent.pbeta2 = savedAsset.pbeta2;
		assetComponent.pbeta3 = savedAsset.pbeta3;

		assetComponent.qalpha1 = savedAsset.qalpha1;
		assetComponent.qalpha2 = savedAsset.qalpha2;
		assetComponent.qalpha3 = savedAsset.qalpha3;

		assetComponent.qbeta1 = savedAsset.qbeta1;
		assetComponent.qbeta2 = savedAsset.qbeta2;
		assetComponent.qbeta3 = savedAsset.qbeta3;

		assetComponent.setActive(savedAsset.active);

		newDevice.GetComponentInChildren<Text> ().text = savedAsset.aname;

	}
}
