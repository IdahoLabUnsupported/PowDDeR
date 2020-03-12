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
public class AssetAdder : MonoBehaviour {

	public ModalController modalController;
	public GameObject devicePrefab;

	public void AddAsset()
	{
		GameObject newDevice = Instantiate (devicePrefab, transform);
		newDevice.GetComponent<Asset> ().active = true;
		//open modal window linking new prefab 
		modalController.ShowModal(newDevice, true);

	}

	public void LoadAsset(AssetSavable savedAsset)
	{
		GameObject newDevice = Instantiate (devicePrefab,transform);

		// sets the object name in unity's hierarchy
		newDevice.name = savedAsset.aname;

		Asset assetComponent = newDevice.GetComponent<Asset> ();

		// sets the asset parameters
		assetComponent.aname = savedAsset.aname;
		assetComponent.latency = savedAsset.latency;
		assetComponent.agilityP = savedAsset.agilityP;
		assetComponent.agilityQ = savedAsset.agilityQ;
		assetComponent.maxP = savedAsset.maxP;
		assetComponent.maxQ = savedAsset.maxQ;
		assetComponent.energy = savedAsset.energy;
		assetComponent.nameIndex = savedAsset.nameIndex;

		assetComponent.palpha1 = savedAsset.palpha1;
		assetComponent.palpha2 = savedAsset.palpha2;
		assetComponent.palpha3 = savedAsset.palpha3;

		assetComponent.pbeta1 = savedAsset.pbeta1;
		assetComponent.pbeta2 = savedAsset.pbeta2;
		assetComponent.pbeta3 = savedAsset.pbeta3;

		assetComponent.qalpha1 = savedAsset.qalpha1;
		assetComponent.qalpha2 = savedAsset.qalpha2;
		assetComponent.qalpha3 = savedAsset.qalpha3;

		assetComponent.qbeta1 = savedAsset.qbeta1;
		assetComponent.qbeta2 = savedAsset.qbeta2;
		assetComponent.qbeta3 = savedAsset.qbeta3;

		assetComponent.setActive(savedAsset.active);

		assetComponent.load = savedAsset.load;


		newDevice.GetComponentInChildren<Text> ().text = savedAsset.aname;

	}
}
