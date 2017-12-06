using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EconomicUnit : MonoBehaviour {

	public GameObject euAssetPrefab;
	public Transform assetContainer;
	public Text label;

	public string euname;
	public float latency;
	public string description;

	//the asset list from asset list
	public Dictionary<string, Asset> assets = new Dictionary<string, Asset> ();

	//all the child asset listing of this economic unit
	private Dictionary <string, GameObject> assetObjects = new Dictionary<string, GameObject> ();

	public void saveEu(EconomicUnit eu)
	{
		euname = eu.euname;
		latency = eu.latency;

		label.text = euname;
	}
		
	public void addAsset(ref GameObject assetObj, int qty)
	{
		Asset asset = assetObj.GetComponent<Asset> ();
		assets.Add (asset.aname, asset);

		//register to this asset to receive any changes that happen to it
		assetObj.GetComponent<Asset> ().onChanged.AddListener(updateAsset);

		// create the economic unic device and make it a child of this economic unit
		GameObject newEuDevice = Instantiate (euAssetPrefab,assetContainer);
		newEuDevice.GetComponentInChildren<Text> ().text = asset.aname;

		// copy the asset from the device listing to this listing
		newEuDevice.GetComponent<Asset> ().saveAssetLoader (asset);

		// register the deletion action 
		newEuDevice.GetComponentInChildren<Button> ().onClick.AddListener (()=>deleteAsset(newEuDevice.GetComponentInChildren<Text> ().text ));

		// set the qty
		if (qty >= 0) {
			newEuDevice.GetComponentInChildren<InputField> ().text = qty.ToString();
		}

		// add a reference to the list of devices that this economic unit has so it can later be deleted if the asset gets deleted from the device listing
		assetObjects.Add (asset.aname, newEuDevice);
	}

	public void deleteAsset(string assetName)
	{
		if (assetObjects.ContainsKey (assetName) && assets.ContainsKey (assetName)) {

			Destroy (assetObjects [assetName]);
			assetObjects.Remove (assetName);
			assets.Remove (assetName);
		}
	}

	public void deleteEconomicUnity()
	{
		DestroyImmediate (this.gameObject);
	}

	public void updateAsset(string aname, GameObject asset, string oldName)
	{
		if (aname != null) {
			if (asset == null) {
				deleteAsset (aname);

			} else if(oldName != aname && oldName != null){
				deleteAsset (oldName);
				addAsset (ref asset, -1);
			}else{
				assets [aname] = asset.GetComponent<Asset>();
				assetObjects [aname] = asset;
			}
		}
	}

}
