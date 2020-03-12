
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
using System;
using System.Net;
using UnityEngine.SceneManagement;
using System.Text;
using System.IO;
using System.Xml;

using UnityEngine.Rendering;

public class IGCAPTReader : MonoBehaviour {
	
	// fields
	public String _name;
	public int _id;
	public string _type;
	public Boolean _enableDataaSending;
	public Boolean _enableDataPassThrough;
	public double _xCoord;
	public double _yCoord;
	public double _lat;
	public double _long;
	public List<int> _endpoints;
	public int _payload;
	public int _interval;
	public float _latency;
	public float _agilityP;
	public float _agilityQ;
	public float _maxP;
	public float _maxQ;
	public float _engergy;
	public float _b1coef;
	public float _b2coef;
	public float _b3coef;
	public float _s1coef;
	public float _s2coef;
	public float _s3coef;
	public string _ipaddress;

	public EuAdder euAdder;
	public AssetAdder assetAdder;

	public void setName(String name) {
		_name = name;
	}
	public String getName() {
		return _name;
	}

	public void setID(int id) {
		_id = id;
	}
	public int getID() {
		return _id;
	}

	public void setType(string type){
		_type = type;

	}

	public string getType () {
		return _type;
	}

	public void setSending(Boolean sending) {
		_enableDataaSending = sending;
	}

	public Boolean getSending(){
		return _enableDataaSending;
	}

	public void setPass(Boolean Pass) {
		_enableDataPassThrough = Pass;
	}

	public Boolean getPass(){
		return _enableDataPassThrough;
	}	



	public void setXCoordinate(double xcoord){
		_xCoord = xcoord;
	}

	public double getXCoordinate(){
		return _xCoord;
	}

	public void setYCoordinate(double ycoord){
		_yCoord = ycoord;
	}

	public double getYCoordinate(){
		return _yCoord;
	}

	public void setLat(double lat){
		_lat = lat;
	}

	public double getLat(){
		return _lat;
	}

	public void setLong(double lng){
		_long = lng;
	}

	public double getLong(){
		return _long;
	}

	public void setInterval(int interv) {
		_interval = interv;
	}

	public int getInterval(){
		return _interval;
	}
	public void setPayLoad(int load) {
		_payload= load;
	}

	public int getPlayLoad(){
		return _payload;
	}

	public void setLatency(float latency) {
		_latency = latency;
	}

	public float getLatency() {
		return _latency;
	}

	public void setAgilityP(float agil){
		_agilityP = agil;
	}
	public float getAgilityP(){
		return _agilityP;
	}

	public void setAgilityQ(float agil){
		_agilityQ = agil;
	}
	public float getAgilityQ(){
		return _agilityQ;
	}

	public void setMaxP(float max){
		_maxP = max;
	}
	public float getMaxP(){
		return _maxP;
	}

	public void setMaxQ(float max){
		_maxQ = max;
	}

	public float _getMaxQ(){
		return _maxQ;
	}

	public void setEngergy(float engergy){
		_engergy = engergy;
	}
	public float getEnergy(){
		return _engergy;
	}

	public void setB1coef (float coef) {
		_b1coef = coef;
	}

	public float getB1coef(){
		return _b1coef;
	}
	public void setB2coef (float coef) {
		_b2coef = coef;
	}

	public float getB2coef(){
		return _b2coef;
	}
	public void setB3coef (float coef) {
		_b3coef = coef;
	}

	public float getB3coef(){
		return _b3coef;
	}

	public void setS1coef (float coef) {
		_s1coef = coef;
	}

	public float getS1coef(){
		return _s1coef;
	}

	public void setS2coef (float coef) {
		_s2coef = coef;
	}

	public float getS2coef(){
		return _s2coef;
	}
	public void setS3coef (float coef) {
		_s3coef = coef;
	}

	public float getS3coef(){
		return _s3coef;
	}
	public void setIpAddress (string ipadd) {
		_ipaddress = ipadd;
	}

	public string getIpAddress(){
		return _ipaddress;
	}
	public void loadIGCAPNetwork(String fileName, List<IGCAPTReader> components){
//		IGCAPTReader tempRecord = new IGCAPTReader();
		XmlDocument xmlDoc = new XmlDocument ();
		_endpoints = new List<int>();
		if (File.Exists (fileName)) {
			xmlDoc.Load (fileName);
			XmlNodeList componentNodes = xmlDoc.SelectNodes ("/Gen/Nodes/node");

			// list of all the euobjects, needed to add assets to this
			Dictionary<int,EuSavable> euList = new Dictionary<int, EuSavable>();
			Dictionary<int,string> assetList = new Dictionary<int, string>();
			Dictionary<string, List<int>> assetConnections = new Dictionary<string, List<int>>();
			Dictionary<int, List<int>> euConnections = new Dictionary<int, List<int>>();

			foreach (XmlNode compNode in componentNodes) 
			{
//				tempRecord.setID (Int32.Parse (compNode.SelectSingleNode("id").InnerText));
//				tempRecord.setType(compNode.SelectSingleNode("type").InnerText);
//				String temp = compNode.SelectSingleNode("enableDataSending").InnerText;
//				if (temp.Equals("true")){
//					tempRecord.setSending (true);
//				}
//					else tempRecord.setSending (false);
//				temp = compNode.SelectSingleNode("enableDataPassThrough").InnerText;
//				if (temp.Equals("true")) {
//					tempRecord.setPass(true);
//				}
//				else tempRecord.setPass(false);
//				tempRecord.setPayLoad(Int32.Parse(compNode.SelectSingleNode("payload").InnerText));
//				tempRecord.setInterval (Int32.Parse(compNode.SelectSingleNode("interval").InnerText));
//				tempRecord.setXCoordinate(Double.Parse(compNode.SelectSingleNode("xCoord").InnerText));
//				tempRecord.setYCoordinate (Double.Parse(compNode.SelectSingleNode("yCoord").InnerText));
//				tempRecord.setLat (Double.Parse (compNode.SelectSingleNode("lat").InnerText));
//				tempRecord.setLong (Double.Parse (compNode.SelectSingleNode("long").InnerText));
//				tempRecord.setName(compNode.SelectSingleNode("name").InnerText);
//				tempRecord.setLatency (float.Parse (compNode.SelectSingleNode("latency").InnerText));
//				tempRecord.setAgilityP (float.Parse (compNode.SelectSingleNode("agilityp").InnerText));
//				tempRecord.setAgilityQ (float.Parse (compNode.SelectSingleNode("agilityq").InnerText));
//				tempRecord.setMaxP (float.Parse (compNode.SelectSingleNode("maxp").InnerText));
//				tempRecord.setMaxQ (float.Parse (compNode.SelectSingleNode("maxq").InnerText));
//				tempRecord.setEngergy (float.Parse (compNode.SelectSingleNode("energy").InnerText));
//				tempRecord.setB1coef (float.Parse (compNode.SelectSingleNode("bcoef1").InnerText));
//				tempRecord.setB2coef (float.Parse (compNode.SelectSingleNode("bcoef2").InnerText));
//				tempRecord.setB3coef (float.Parse (compNode.SelectSingleNode("bcoef3").InnerText));
//				tempRecord.setS1coef (float.Parse (compNode.SelectSingleNode("scoef1").InnerText));
//				tempRecord.setS2coef (float.Parse (compNode.SelectSingleNode("scoef2").InnerText));
//				tempRecord.setS3coef (float.Parse (compNode.SelectSingleNode("scoef3").InnerText));
//				tempRecord.setIpAddress(compNode.SelectSingleNode("ipaddress").InnerText);


				if(compNode.SelectSingleNode("type").InnerText == "0b78060e-978d-4132-b38d-8155f4741d11"){
					//add eu
					EuSavable newEu = new EuSavable();
					newEu.active = true;
					newEu.euname = compNode.SelectSingleNode("name").InnerText;
					newEu.latency = float.Parse (compNode.SelectSingleNode("latency").InnerText);
					newEu.assets = new List<string> ();
					newEu.qty = new List<int> ();
					euList.Add(Int32.Parse (compNode.SelectSingleNode("id").InnerText), newEu);
					//save the connection information
					List<int> endPtList = new List<int>();
					XmlNodeList ElementNodes = compNode.SelectSingleNode("endPoints").SelectNodes ("endPoint");
					foreach (XmlNode endpoint in ElementNodes) {
						endPtList.Add (int.Parse(endpoint.InnerText));
					}
					euConnections.Add(int.Parse (compNode.SelectSingleNode("id").InnerText), endPtList);
				}
				else
				{
					//add asset
					AssetSavable newAsset = new AssetSavable();

					newAsset.aname = compNode.SelectSingleNode("name").InnerText;
					newAsset.active = true;
					newAsset.agilityP = float.Parse(compNode.SelectSingleNode("agilityp").InnerText);
					newAsset.agilityQ = float.Parse(compNode.SelectSingleNode("agilityq").InnerText);
					newAsset.maxP = float.Parse(compNode.SelectSingleNode("maxp").InnerText);
					newAsset.maxQ = float.Parse(compNode.SelectSingleNode("maxq").InnerText);
					newAsset.latency = float.Parse(compNode.SelectSingleNode("latency").InnerText);
					newAsset.energy = float.Parse(compNode.SelectSingleNode("energy").InnerText);

					if (compNode.SelectSingleNode ("pacoef1") != null && compNode.SelectSingleNode ("qacoef1") != null) {
						newAsset.palpha1 = float.Parse (compNode.SelectSingleNode ("pacoef1").InnerText);
						newAsset.palpha2 = float.Parse (compNode.SelectSingleNode ("pacoef2").InnerText);
						newAsset.palpha3 = float.Parse (compNode.SelectSingleNode ("pacoef3").InnerText);

						newAsset.pbeta1 = float.Parse (compNode.SelectSingleNode ("pbcoef1").InnerText);
						newAsset.pbeta2 = float.Parse (compNode.SelectSingleNode ("pbcoef2").InnerText);
						newAsset.pbeta3 = float.Parse (compNode.SelectSingleNode ("pbcoef3").InnerText);

						newAsset.qalpha1 = float.Parse (compNode.SelectSingleNode ("qacoef1").InnerText);
						newAsset.qalpha2 = float.Parse (compNode.SelectSingleNode ("qacoef2").InnerText);
						newAsset.qalpha3 = float.Parse (compNode.SelectSingleNode ("qacoef3").InnerText);

						newAsset.qbeta1 = float.Parse (compNode.SelectSingleNode ("qbcoef1").InnerText);
						newAsset.qbeta2 = float.Parse (compNode.SelectSingleNode ("qbcoef2").InnerText);
						newAsset.qbeta3 = float.Parse (compNode.SelectSingleNode ("qbcoef3").InnerText);
					}

					//create and add the unity game object
					assetAdder.LoadAsset (newAsset);
					assetList.Add (Int32.Parse (compNode.SelectSingleNode("id").InnerText), newAsset.aname);
					List<int> endPtList = new List<int> ();
					XmlNodeList ElementNodes = compNode.SelectSingleNode("endPoints").SelectNodes ("endPoint");
					foreach (XmlNode endpoint in ElementNodes) {
						endPtList.Add(int.Parse(endpoint.InnerText));
					}
					assetConnections.Add(newAsset.aname, endPtList);
				}

				//components.Add (tempRecord);
			}

			// similar to "visited" flag in traditional graphs
			List<int> processedPts = new List<int> ();

			//process the connections on each economic unit
			foreach (KeyValuePair<int,List<int>> connections in euConnections) {

				processedPts.Clear ();

				foreach (int endPt in connections.Value) {
					List<int> toProcess = new List<int> ();

					toProcess.Add (endPt);

					for (int i = 0; i < toProcess.Count; i++) {
						int pt = toProcess [i];

						if (processedPts.Contains (pt)) {
							continue;
						}

						//if this point is an asset
						if (assetList.ContainsKey (pt)) {

							//add this asset to the eu's list of assets
							euList [connections.Key].assets.Add (assetList [pt]);
							euList [connections.Key].qty.Add (1);
							// add all the connection points on the asset for further processing, ignoring economic units and previously processed points
							foreach (int nextPt in assetConnections[assetList[pt]]) {
								if(assetList.ContainsKey(nextPt) && !processedPts.Contains(nextPt))
								{
									toProcess.Add (nextPt);
								}
							}
						}

						processedPts.Add (pt);
					}
			
				}
			}

			foreach (KeyValuePair<int,EuSavable> eu in euList) {
				euAdder.LoadEconomicUnit (eu.Value);
			}
		
		}
	}
	public IGCAPTReader ()
	{
		
	}
//		static void Main(string[] args)
//		{
//			//Console.WriteLine ("Calling loadIGCAP");
//			public List<IGCAPT> componnents = new List<IGCAPT>();
//			IGCAPT tempcomp = new IGCAPT();
//			tempcomp.loadIGCAPNetwork("c:\\users\\bev\\Libraries\\Documents\\endpoints");
//
//		}

}
	

