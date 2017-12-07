using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
public class Saver : MonoBehaviour {
	public GameObject assetContainer;
	public GameObject euContainer;
	public SetGenerationSettings settings;
	string path = "Assets/Resources/";

	public void save()
	{
		string jsonData;

		//delete all files in directory before proceeding
		DirectoryInfo directory = new DirectoryInfo (path);
		FileInfo[] info = directory.GetFiles ();

		for (int i = 0; i < info.Length; i++) {
			info [i].Delete();
		}

		foreach (Transform child in assetContainer.transform)
		{
			//save all the assets
			Asset asset = child.GetComponent<Asset> ();
			AssetSavable assetSave = new AssetSavable ();

			assetSave.nameIndex = asset.nameIndex;
			assetSave.aname = asset.aname;
			assetSave.latency = asset.latency;  
			assetSave.agilityP = asset.agilityP;  
			assetSave.agilityQ = asset.agilityQ;
			assetSave.maxP = asset.maxP;
			assetSave.maxQ = asset.maxQ;
			assetSave.energy = asset.energy;
			assetSave.active = asset.active;

			//Convert to Json
			jsonData = JsonUtility.ToJson(assetSave);

			//Save Json string
			string fileName = "device"+assetSave.aname;
			File.WriteAllText(path+fileName, jsonData);
		}

		// save all the economic units
		foreach (Transform child in euContainer.transform)
		{
			EconomicUnit eu = child.GetComponent<EconomicUnit> ();
			EuSavable euSave = new EuSavable ();
			euSave.assets = new List<string> ();
			euSave.qty = new List<int> ();
			euSave.euname = eu.euname;
			euSave.active = eu.active;
			euSave.latency = eu.latency;

			Transform listingContainer = child.Find ("Scroll View/Viewport/Content");

			// save each device and the quantity
			foreach (Transform listing in listingContainer) {
				euSave.assets.Add (listing.GetComponentInChildren<Text>().text);
				euSave.qty.Add (int.Parse(listing.GetComponentInChildren<InputField>().text));
			}

			//Convert to Json
			jsonData = JsonUtility.ToJson(euSave);

			//Save Json string
			File.WriteAllText(path+"eu"+euSave.euname, jsonData);
		}

		// save generation settings
		jsonData = JsonUtility.ToJson(settings.GetSettings());

		//save Json string
		File.WriteAllText(path+"settings",jsonData);
	}
}
