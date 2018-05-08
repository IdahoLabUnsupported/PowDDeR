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
	public bool load;

	//Alpha1*s^2+Alpha2*s+Alpha3
	//-----------------------
	//Beta1*s^2+Beta2*s+Beta3
	public float palpha1,palpha2,palpha3;
	public float pbeta1,pbeta2,pbeta3;

	//Alpha1*s^2+Alpha2*s+Alpha3
	//-----------------------
	//Beta1*s^2+Beta2*s+Beta3
	public float qalpha1,qalpha2,qalpha3;
	public float qbeta1,qbeta2,qbeta3;
}
