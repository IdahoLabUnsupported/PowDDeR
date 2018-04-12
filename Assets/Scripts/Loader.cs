using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


public class Loader : MonoBehaviour {
	string path = "Assets/Resources/";
	public EuAdder euAdder;
	public AssetAdder assetAdder;
	public SetGenerationSettings settings;
	public IGCAPTReader xmlReader;

	void Awake()
	{
		Load ();
	}

	public void Load()
	{
		DirectoryInfo directory;

		if (!Directory.Exists (path)) {
			directory = Directory.CreateDirectory (path);
		} else {
			directory = new DirectoryInfo (path);
		}

		FileInfo[] info = directory.GetFiles ();

		//sort by file name
		Array.Sort(info, delegate(FileInfo f1, FileInfo f2) {
			return f1.Name.CompareTo(f2.Name);
		});

		//search for XML file first, and if found skip the other files
		bool foundXml = false;
		for (int i = 0; i < info.Length; i++) {
			if(info [i].Name.Contains ("fileconfig")&& !info [i].Extension.Contains ("meta"))
			{
				List<IGCAPTReader> components = new List<IGCAPTReader> ();
				xmlReader.loadIGCAPNetwork (path+info[i].Name, components);
				foundXml = true;
				break;
			}
		}

		for (int i = 0; i < info.Length && !foundXml; i++) {
			string json = File.ReadAllText (info[i].FullName);

			if(info [i].Name.Contains ("device")&& !info [i].Extension.Contains ("meta"))
			{
				assetAdder.LoadAsset (JsonUtility.FromJson<AssetSavable> (json));

			} else if(info [i].Name.Contains ("eu") && !info [i].Extension.Contains ("meta")){

				euAdder.LoadEconomicUnit(JsonUtility.FromJson<EuSavable> (json));

			} else if(info [i].Name.Contains ("settings") && !info [i].Extension.Contains ("meta")){

				settings.setSettings(JsonUtility.FromJson<GenerationSettings> (json));
			}
		}
	}
}
