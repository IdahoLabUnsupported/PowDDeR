using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Loader : MonoBehaviour {
	string path = "Assets/Resources/";
	public EuAdder euAdder;
	public AssetAdder assetAdder;
	public GenerationSettings settings;
	void Awake()
	{
		Load ();
	}

	public void Load()
	{
		DirectoryInfo directory = new DirectoryInfo (path);
		FileInfo[] info = directory.GetFiles ();

		//sort by file name
		Array.Sort(info, delegate(FileInfo f1, FileInfo f2) {
			return f1.Name.CompareTo(f2.Name);
		});


		// load all devices first
		for (int i = 0; i < info.Length; i++) {
			string json = File.ReadAllText (info[i].FullName);

			if(info [i].Name.Contains ("device")&& !info [i].Extension.Contains ("meta"))
			{
				assetAdder.LoadAsset (JsonUtility.FromJson<AssetSavable> (json));

			} else if(info [i].Name.Contains ("eu") && !info [i].Extension.Contains ("meta")){

				euAdder.LoadEconomicUnit(JsonUtility.FromJson<EuSavable> (json));

			} else if(info [i].Name.Contains ("settings") && !info [i].Extension.Contains ("meta")){

				settings = JsonUtility.FromJson<GenerationSettings> (json);
			}
		}

		// then load economic units
		for (int i = 0; i < info.Length; i++) {
			string json = File.ReadAllText (info[i].FullName);

			if (info [i].Name.Contains ("eu") && !info [i].Extension.Contains ("meta")) {

				euAdder.LoadEconomicUnit(JsonUtility.FromJson<EuSavable> (json));
			}
		}
	}
}
