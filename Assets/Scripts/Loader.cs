
/*
© 2020 Battelle Energy Alliance, LLC
ALL RIGHTS RESERVED

Prepared by Battelle Energy Alliance, LLC
Under Contract No.DE-AC07-05ID14517
With the U. S.Department of Energy

NOTICE:  This computer software was prepared by Battelle Energy
Alliance, LLC, hereinafter the Contractor, under Contract
No.AC07-05ID14517 with the United States (U.S.) Department of
Energy (DOE).  The Government is granted for itself and others acting on
its behalf a nonexclusive, paid-up, irrevocable worldwide license in this
data to reproduce, prepare derivative works, and perform publicly and
display publicly, by or on behalf of the Government.There is provision for
the possible extension of the term of this license.Subsequent to that
period or any extension granted, the Government is granted for itself and
others acting on its behalf a nonexclusive, paid-up, irrevocable worldwide
license in this data to reproduce, prepare derivative works, distribute
copies to the public, perform publicly and display publicly, and to permit
others to do so.The specific term of the license can be identified by
inquiry made to Contractor or DOE.NEITHER THE UNITED STATES NOR THE UNITED
STATES DEPARTMENT OF ENERGY, NOR CONTRACTOR MAKES ANY WARRANTY, EXPRESS OR
IMPLIED, OR ASSUMES ANY LIABILITY OR RESPONSIBILITY FOR THE USE, ACCURACY,
COMPLETENESS, OR USEFULNESS OR ANY INFORMATION, APPARATUS, PRODUCT, OR
PROCESS DISCLOSED, OR REPRESENTS THAT ITS USE WOULD NOT INFRINGE PRIVATELY
OWNED RIGHTS.

Authors:
Tim McJunkin
Craig Rieger
Thomas Szewczyk
James Money
Randall Reese
*/



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
