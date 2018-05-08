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

			assetSave.palpha1 = asset.palpha1;
			assetSave.palpha2 = asset.palpha2;
			assetSave.palpha3 = asset.palpha3;
			assetSave.pbeta1 = asset.pbeta1;
			assetSave.pbeta2 = asset.pbeta2;
			assetSave.pbeta3 = asset.pbeta3;

			assetSave.qalpha1 = asset.qalpha1;
			assetSave.qalpha2 = asset.qalpha2;
			assetSave.qalpha3 = asset.qalpha3;
			assetSave.qbeta1 = asset.qbeta1;
			assetSave.qbeta2 = asset.qbeta2;
			assetSave.qbeta3 = asset.qbeta3;

			assetSave.load = asset.load;

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
