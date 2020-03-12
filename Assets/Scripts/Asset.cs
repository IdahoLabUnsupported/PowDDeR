

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
	public bool active = true;
	public bool load = false;


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

		load = asset.load;

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

		load = asset.load;

		nameIndex = asset.nameIndex;

		palpha1 = asset.palpha1;
		palpha2 = asset.palpha2;
		palpha3 = asset.palpha3;
		pbeta1 = asset.pbeta1;
		pbeta2 = asset.pbeta2;
		pbeta3 = asset.pbeta3;

		qalpha1 = asset.qalpha1;
		qalpha2 = asset.qalpha2;
		qalpha3 = asset.qalpha3;
		qbeta1 = asset.qbeta1;
		qbeta2 = asset.qbeta2;
		qbeta3 = asset.qbeta3;
	
		GetComponentInChildren<Text> ().text = aname;
	}

	public void deleteAsset()
	{
		onChanged.Invoke(aname, null, null);

		Destroy (this.gameObject);
	}

	public void toggleActiveAsset()
	{
		// interesting thing, this gets called even when .isOn is set through scripting, as below
		// the call back but just check if isOn changes and then calls this method
		if (active != GetComponentInChildren<Toggle> ().isOn) {
			active = !active;
			onChanged.Invoke (aname, this.gameObject, aname);
		}
	}

	public void setActive(bool isActive)
	{
		active = isActive;
		GetComponentInChildren<Toggle> ().isOn = isActive;
	}
}
