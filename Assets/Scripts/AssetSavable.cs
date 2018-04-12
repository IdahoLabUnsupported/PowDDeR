using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AssetSavable{

	public int nameIndex;
	public string aname;
	public float latency;  
	public float agilityP;  
	public float agilityQ;
	public float maxP;
	public float maxQ;
	public float energy;
	public bool active;


	//Beta3*s^2+Beta2*s+Beta1
	//-----------------------
	//Alpha3*s^2+Alpha2*s+Alpha1
	public float alpha1,alpha2,alpha3;
	public float beta1,beta2,beta3;
}
