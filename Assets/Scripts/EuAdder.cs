using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EuAdder : MonoBehaviour {

	public EuModalController euModalController;
	public GameObject euPrefab;

	public void AddEconomicUnit()
	{
		GameObject newEu = Instantiate (euPrefab, this.gameObject.transform);

		if (euModalController != null) {
			//open modal window linking new prefab 
			euModalController.ShowModal (newEu, true);
		}
	}

	public void LoadEconomicUnit(EuSavable savedEu)
	{
		GameObject newEu = Instantiate (euPrefab, this.gameObject.transform);

		EconomicUnit euComponent = newEu.GetComponent<EconomicUnit> ();

		euComponent.euname = savedEu.euname;
		euComponent.latency = savedEu.latency;
		euComponent.description = savedEu.description;
		euComponent.GetComponentInChildren<Text> ().text = savedEu.euname;

		euComponent.setActive (savedEu.active);

		for (int i = 0; i < savedEu.assets.Count; i++) {
			GameObject asset = GameObject.Find (savedEu.assets [i]);
			euComponent.addAsset (ref asset, savedEu.qty[i]);
		}
	}
}
