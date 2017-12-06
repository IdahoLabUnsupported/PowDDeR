using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Asset : MonoBehaviour {

	public int nameIndex;
	public string aname;
	public float latency;  
	public float agilityP;  
	public float agilityQ;
	public float maxP;
	public float maxQ;
	public float energy;
	public bool active;

	public class AssetChangeEvent : UnityEvent<string, GameObject, string> {} //empty class; just needs to exist

	public AssetChangeEvent onChanged = new AssetChangeEvent();

	public void saveAsset(Asset asset)
	{
		string oldName = GetComponentInChildren<Text> ().text;
		aname = asset.aname;
		latency = asset.latency;
		agilityP = asset.agilityP;
		agilityQ = asset.agilityQ;
		maxP = asset.maxP;
		maxQ = asset.maxQ;
		energy = asset.energy;
		active = asset.active;
		nameIndex = asset.nameIndex;

		GetComponentInChildren<Text> ().text = aname;

		onChanged.Invoke (aname, this.gameObject, oldName);
	}

	public void saveAssetLoader(Asset asset)
	{
		string oldName = GetComponentInChildren<Text> ().text;
		aname = asset.aname;
		latency = asset.latency;
		agilityP = asset.agilityP;
		agilityQ = asset.agilityQ;
		maxP = asset.maxP;
		maxQ = asset.maxQ;
		energy = asset.energy;
		active = asset.active;
		nameIndex = asset.nameIndex;

		GetComponentInChildren<Text> ().text = aname;
	}

	public void deleteAsset()
	{
		onChanged.Invoke(aname, null, null);

		Destroy (this.gameObject);
	}
}
