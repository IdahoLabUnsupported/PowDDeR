using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//generate screen shot

public class GenerateMesh : MonoBehaviour {

	public GameObject euContainer;
	public int totalTimeSeconds = 1700, timeStepsPerSecond = 10, polarSteps = 100;
	public Camera lineCam;
	public GameObject menu;
	public SetGenerationSettings settings;
	public Slider slider;

	static int MAX_VERTEX = 65000;
	static int MAX_MESHES = 50;

	List<Vector3>[] points = new List<Vector3>[MAX_MESHES];
	List<Vector3>[] negativePoints = new List<Vector3>[MAX_MESHES];
	Vector3 mins = Vector3.zero;
	Vector3 maxs = Vector3.zero;

	int numPoints;
	int totalMeshes = 0;

	int fieldCount = 0;

	public GameObject displayCube;
	List<GameObject> displayCubeList = new List<GameObject>();
	public Transform displayCubeHolder;


	public GameObject negativeDisplayCube;
	List<GameObject> negativeDisplayCubeList = new List<GameObject>();
	public Transform negativeDisplayCubeHolder;

	private Mesh[] meshBack = new Mesh[MAX_MESHES*2];

	VectorLine line;
	VectorLine line2d;
	GameObject lineObject;
	GameObject display;
	List<Vector3> linePoints;

	// Use this for initialization
	void Start () {

		for (int i = 0; i < MAX_MESHES; i++) {
			points [i] = new List<Vector3> ();
			points [i].Capacity = MAX_VERTEX;
			negativePoints [i] = new List<Vector3> ();
			negativePoints [i].Capacity = MAX_VERTEX;
			meshBack[i] = new Mesh();
		}

		totalTimeSeconds = 1700;
		timeStepsPerSecond = 10;
		polarSteps = 100;

		// handle line
		VectorLine.SetCamera3D (lineCam);
		linePoints = new List<Vector3>();
		line = new VectorLine ("line", linePoints, 7.0f, LineType.Continuous,Joins.Fill);
		line.drawTransform = displayCube.transform;
		line2d = new VectorLine ("line2d", linePoints, 2.0f, LineType.Continuous, Joins.Fill);
		GameObject.Find ("line").layer = 9;
		GameObject.Find ("line2d").layer = 8;
	}


	public void setColor(Color inColor)
	{
		settings.currentSettings.color = inColor;

		//set the object color
		foreach(GameObject display in displayCubeList)
		{
			display.GetComponent<Renderer>().material.SetColor ("_Color", settings.currentSettings.color);
		}
	}

	public void setBgColor(Color inColor)
	{
		settings.currentSettings.bgColor = inColor;

		//set the background color
		Camera.main.backgroundColor = settings.currentSettings.bgColor;
		lineCam.backgroundColor = settings.currentSettings.bgColor;
	}

	public void setLineColor(Color inColor)
	{
		settings.currentSettings.lineColor = inColor;

		//redraw the line with the new color, have to redraw due to limitation of vectrocity.
		drawLine (slider.value);
	}

	public void onSliderChange()
	{
		Slider slider = EventSystem.current.currentSelectedGameObject.GetComponent<Slider>();

		if (slider != null) {
			drawLine (slider.value);
			slider.GetComponentInChildren<Text> ().text = slider.value.ToString ();
		}
	}

	public void onGenerateManifold()
	{
		for (int i = 0; i < MAX_MESHES; i++) {
			points [i].Clear();
			negativePoints [i].Clear ();
		}

		//set the background color
		Camera.main.backgroundColor = settings.currentSettings.bgColor;
		lineCam.backgroundColor = settings.currentSettings.bgColor;

		mins = Vector3.zero;
		maxs = Vector3.zero;

		totalTimeSeconds = settings.currentSettings.totalTimeSeconds;
		polarSteps = settings.currentSettings.polarSteps;
		timeStepsPerSecond = settings.currentSettings.timeStepsPerSecond;

		int count = 0;
		int numMesh = 0;
		int numNegativeMesh = 0;
		bool runOnce = false;

		int index;

		slider.maxValue = totalTimeSeconds;
		slider.minValue = 0;
		slider.value = (totalTimeSeconds / 2.0f);
		slider.GetComponentInChildren<Text> ().text = (totalTimeSeconds / 2.0f).ToString ();

		bool first = true;

		// get all the economic units
		foreach (Transform child in euContainer.transform) {

			// if this economic unit isn't toggled on then skip it
			if (!child.GetComponentInChildren<Toggle> ().isOn) {
				continue;
			}

			float euLatency = child.GetComponent<EconomicUnit> ().latency;

			Transform listingContainer = child.Find ("Scroll View/Viewport/Content");

			// get the listing of assets
			foreach (Transform listing in listingContainer) {

				Asset asset = listing.GetComponent<Asset> ();

				// if this asset isn't active continue on
				if (!asset.active) {
					continue;
				}

				int qty = int.Parse(listing.GetComponentInChildren<InputField> ().text);

				Debug.Log ("Processing Asset " + asset.aname + "qty: " + qty);

				// for each quantity of this asset
				for (int c = 0; c < qty; c++) {	

					count = 0;
					numMesh = 0;
					index = 0;
					fieldCount = polarSteps;

					List<Vector3> vertices = this.GetComponent<CalculateManifold> ().buildOneManifold(asset,totalTimeSeconds,timeStepsPerSecond, polarSteps, euLatency);

					for (int i = 0; i < vertices.Count; i++) {

						Vector3 point = vertices [i];
						Vector3 negativePoint = vertices [i];
						numMesh = count / MAX_VERTEX;

						// if this is the first asset just add it appropriately to the points because no addition needs to be calculated
						if (first && !asset.load) {
							points [numMesh].Add (point);
							negativePoints [numMesh].Add (Vector3.zero);

						} else if (first && asset.load) {
							negativePoints [numMesh].Add (point);
							points [numMesh].Add (Vector3.zero);

						}else{
							float newX = 0.0f;
							float newY = 0.0f;

							float negativeX = 0.0f;
							float negativeY = 0.0f;

							if (index >= MAX_VERTEX) {
								index = 0;
							}
							// if this isn't a load shrink the negative manifold and grow the positive manifold
							if (!asset.load) {


								//if we are still at negative side of manifold and the addition of this point doesn't change the sign of this 
								//point, shrink the negative manifold and set the positive manifold zero
								if (negativePoints [numMesh] [index].x != 0.0f && Mathf.Sign (negativePoints [numMesh] [index].x - point.x) == Mathf.Sign (negativePoints [numMesh] [index].x)) {
									negativeX = negativePoints [numMesh] [index].x - point.x;
									newX = 0.0f;
									//else if there is a sign change
								} else if (negativePoints [numMesh] [index].x != 0.0f && Mathf.Sign (negativePoints [numMesh] [index].x - point.x) != Mathf.Sign (negativePoints [numMesh] [index].x)) {
									//add the difference to the manifold and zeroize this negative manifold point
									newX = points [numMesh] [index].x + negativePoints [numMesh] [index].x - point.x;
									negativeX = 0.0f;

									//else theres no negative component to worry about, just add everything together
								} else {
									negativeX = 0.0f;
									newX = points [numMesh] [index].x + point.x;
								}


								if (negativePoints [numMesh] [index].y != 0.0f && Mathf.Sign (negativePoints [numMesh] [index].y - point.y) == Mathf.Sign (negativePoints [numMesh] [index].y)) {
									negativeY = negativePoints [numMesh] [index].y - point.y;
									newY = 0.0f;
								//else if there is a sign change
								} else if (negativePoints [numMesh] [index].y != 0.0f && Mathf.Sign (negativePoints [numMesh] [index].y - point.y) != Mathf.Sign (negativePoints [numMesh] [index].y)) {
									//add the difference to the manifold and zeroize this negative manifold point
									negativeY = 0.0f;
									newY = points [numMesh] [index].y + negativePoints [numMesh] [index].y - point.y;
								//else theres no negative component to worry about, just add everything together
								} else {
									negativeY = 0.0f;
									newY = points [numMesh] [index].y + point.y;
								}


								// this is a load that should shrink positive manifold and grow negative manifold
							} else {

								//if we are still at positive side of manifold and the subtraction of this point doesn't change the sign of this 
								//point, shrink the positive manifold and set the negative manifold zero
								if (points [numMesh] [index].x != 0.0f && Mathf.Sign (points [numMesh] [index].x - point.x) == Mathf.Sign (points [numMesh] [index].x)) {
									negativeX = 0.0f;
									newX = points [numMesh] [index].x - point.x;
									//else if there is a sign change
								} else if (points [numMesh] [index].x != 0.0f && Mathf.Sign (points [numMesh] [index].x - point.x) != Mathf.Sign (points [numMesh] [index].x)) {
									//add the difference to the negative manifold and zeroize this positive manifold point
									negativeX = negativePoints [numMesh] [index].x + points [numMesh] [index].x - point.x;
									newX = 0.0f;

									//else theres no positive component to worry about, just add everything together
								} else {
									newX = 0.0f;
									negativeX = negativePoints [numMesh] [index].x + point.x;
								}


								if (points [numMesh] [index].y != 0.0f && Mathf.Sign (points [numMesh] [index].y - point.y) == Mathf.Sign (points [numMesh] [index].y)) {
									negativeY = 0.0f;
									newY = points [numMesh] [index].y - point.y;
									//else if there is a sign change
								} else if (points [numMesh] [index].y != 0.0f && Mathf.Sign (points [numMesh] [index].y - point.y) != Mathf.Sign (points [numMesh] [index].y)) {
									//add the difference to the negative manifold and zeroize this positive manifold point
									negativeY = negativePoints [numMesh] [index].y + points [numMesh] [index].y - point.y;
									newY = 0.0f;

									//else theres no positive component to worry about, just add everything together
								} else {
									newY = 0.0f;
									negativeY = negativePoints [numMesh] [index].y + point.y;
								}


							}
								
							// don't add z's because that is the time step
							float newZ = point.z;

							point = new Vector3 (newX, newY, newZ);
							negativePoint = new Vector3 (negativeX, negativeY, newZ);
							if (points [numMesh].Count > 0) {
								((List<Vector3>)(points [numMesh])) [index] = point;
							}

							if (negativePoints [numMesh].Count > 0) {
								((List<Vector3>)(negativePoints [numMesh])) [index] = negativePoint;
							}

							index++;
						}

						count++;

						if (!runOnce) {
							mins = new Vector3 (point.x, point.y, point.z);
							maxs = new Vector3 (point.x, point.y, point.z);
							runOnce = true;
						}

						//figure out the mins
						if (point.x < mins.x) {
							mins = new Vector3 (point.x, mins.y, mins.z);
						}
						if (point.y < mins.y) {
							mins = new Vector3 (mins.x, point.y, mins.z);
						}
						if (point.z < mins.z) {
							mins = new Vector3 (mins.x, mins.y, point.z);
						}
						if (negativePoint.x < mins.x) {
							mins = new Vector3 (negativePoint.x, mins.y, mins.z);
						}
						if (negativePoint.y < mins.y) {
							mins = new Vector3 (mins.x, negativePoint.y, mins.z);
						}
						if (negativePoint.z < mins.z) {
							mins = new Vector3 (mins.x, mins.y, negativePoint.z);
						}


						//figure out the maxs
						if (point.x > maxs.x) {
							maxs = new Vector3 (point.x, maxs.y, maxs.z);
						}
						if (point.y > maxs.y) {
							maxs = new Vector3 (maxs.x, point.y, maxs.z);
						}
						if (point.z > maxs.z) {
							maxs = new Vector3 (maxs.x, maxs.y, point.z);
						}
						if (negativePoint.x > maxs.x) {
							maxs = new Vector3 (negativePoint.x, maxs.y, maxs.z);
						}
						if (negativePoint.y > maxs.y) {
							maxs = new Vector3 (maxs.x, negativePoint.y, maxs.z);
						}
						if (negativePoint.z > maxs.z) {
							maxs = new Vector3 (maxs.x, maxs.y, negativePoint.z);
						}
					}

					// finished processing first asset
					first = false;
				}
			}
		}

		// turn off the menu
		menu.SetActive (false);

		totalMeshes = numMesh;

		Debug.Log ("Mins " + mins);
		Debug.Log ("Maxs " + maxs);
		Debug.Log ("Count " + count);
		Debug.Log ("Number of meshes needed " + totalMeshes);

		createMesh ();
		createNegativeMesh ();
		drawLine ();
		drawAxisLabels ();
	}

	void createMesh()
	{
		//clear out old meshes
		foreach (GameObject item in displayCubeList) {
			Destroy (item);
		}

		displayCube.GetComponent<Renderer> ().enabled = true;

		int currentMesh = 0;

		// generate the faces
		while (currentMesh < totalMeshes) {
			int numPoints = points [currentMesh].Count;

			int[] indecies = new int[numPoints];
			int[] triangles = new int[Mathf.CeilToInt((numPoints/polarSteps)*(polarSteps*2)*3)];

			int bandCount = 1;

			// create new colors array where the colors will be created.
			Color[] colors = new Color[numPoints];

			for (int j = 0; j < numPoints; j++) {

				// replace current point with it's normalized value
				points [currentMesh] [j] = normalizePoints (points [currentMesh] [j]);

				indecies [j] = j;
				colors [j] = Color.red;

				//if this is not the last row of points and not at the end of a band
				if((j+fieldCount + 1)<numPoints && (j != (bandCount*(fieldCount) - 1)))
				{

					//front side
					triangles [j*12/2] = j;
					triangles [j*12/2 + 1] = j + 1;
					triangles [j*12/2 + 2] = j + fieldCount;

					triangles [j*12/2 + 3] = j + 1;
					triangles [j*12/2 + 4] = j + 1 + fieldCount;
					triangles [j*12/2 + 5] = j + fieldCount;
				}

				// if this is at the end of a band, close the loop and increment to the next band
				if (j == (bandCount*(fieldCount) - 1)) {

					// if this isn't the very last band
					if ((j + fieldCount) < numPoints) {
						//front side
						triangles [j * 12 / 2] = j;
						triangles [j * 12 / 2 + 1] = j - (fieldCount - 1);
						triangles [j * 12 / 2 + 2] = j + 1;

						triangles [j * 12 / 2 + 3] = j;
						triangles [j * 12 / 2 + 4] = j + 1;
						triangles [j * 12 / 2 + 5] = j + fieldCount - 1;
					}

					bandCount++;
				}

			}


			meshBack[currentMesh] = new Mesh();

			meshBack[currentMesh].SetVertices (points[currentMesh]);
		
			meshBack[currentMesh].colors = colors;

			// generate mesh
			meshBack[currentMesh].triangles = triangles;
			meshBack[currentMesh].RecalculateNormals();
			//NormalSolver.RecalculateNormals(meshBack[i],60.0f);

			//generate points
			//meshBack[currentMesh].SetIndices (indecies, MeshTopology.Points, 0);

			GameObject display = Instantiate (displayCube,displayCubeHolder);
			display.GetComponent<MeshFilter> ().mesh.Clear ();
			display.GetComponent<MeshFilter> ().mesh = meshBack [currentMesh];

			display.GetComponent<Renderer>().material.SetColor ("_Color", settings.currentSettings.color);

			displayCubeList.Add (display);

			currentMesh++;
		}

		displayCube.GetComponent<Renderer>().enabled = false;
	}
	void createNegativeMesh()
	{
		//clear out old meshes
		foreach (GameObject item in negativeDisplayCubeList) {
			Destroy (item);
		}

		negativeDisplayCube.GetComponent<Renderer> ().enabled = true;

		int currentMesh = 0;

		// generate the faces
		while (currentMesh <= totalMeshes) {
			int numPoints = 0;

			numPoints = negativePoints [currentMesh].Count;

			int[] indecies = new int[numPoints];
			int[] triangles = new int[Mathf.CeilToInt((numPoints/polarSteps)*(polarSteps*2)*3)];

			int bandCount = 1;

			// create new colors array where the colors will be created.
			Color[] colors = new Color[numPoints];

			for (int j = 0; j < numPoints; j++) {

				// replace current point with it's normalized value
				negativePoints [currentMesh] [j] = normalizePoints (negativePoints [currentMesh] [j]);

				indecies [j] = j;
				colors [j] = Color.red;

				//if this is not the last row of points and not at the end of a band
				if((j+fieldCount + 1)<numPoints && (j != (bandCount*(fieldCount) - 1)))
				{

					//front side
					triangles [j*12/2] = j;
					triangles [j*12/2 + 1] = j + 1;
					triangles [j*12/2 + 2] = j + fieldCount;

					triangles [j*12/2 + 3] = j + 1;
					triangles [j*12/2 + 4] = j + 1 + fieldCount;
					triangles [j*12/2 + 5] = j + fieldCount;
				}

				// if this is at the end of a band, close the loop and increment to the next band
				if (j == (bandCount*(fieldCount) - 1)) {

					// if this isn't the very last band
					if ((j + fieldCount) < numPoints) {
						//front side
						triangles [j * 12 / 2] = j;
						triangles [j * 12 / 2 + 1] = j - (fieldCount - 1);
						triangles [j * 12 / 2 + 2] = j + 1;

						triangles [j * 12 / 2 + 3] = j;
						triangles [j * 12 / 2 + 4] = j + 1;
						triangles [j * 12 / 2 + 5] = j + fieldCount - 1;
					}

					bandCount++;
				}

			}


			meshBack[currentMesh] = new Mesh();

			meshBack[currentMesh].SetVertices (negativePoints [currentMesh]);



			meshBack[currentMesh].colors = colors;

			// generate mesh
			meshBack[currentMesh].triangles = triangles;
			meshBack[currentMesh].RecalculateNormals();
			//NormalSolver.RecalculateNormals(meshBack[i],60.0f);

			//generate points
			//meshBack[currentMesh].SetIndices (indecies, MeshTopology.Points, 0);

			GameObject display = Instantiate (negativeDisplayCube,negativeDisplayCubeHolder);
			display.GetComponent<MeshFilter> ().mesh.Clear ();
			display.GetComponent<MeshFilter> ().mesh = meshBack [currentMesh];

			display.GetComponent<Renderer>().material.SetColor ("_Color", Color.red);


			negativeDisplayCubeList.Add (display);

			currentMesh++;
		}

		negativeDisplayCube.GetComponent<Renderer>().enabled = false;
	}
	// draw the line
	public void drawLine(float? stime = null)
	{

		if (points[0].Count < 1) {
			return;
		}

		linePoints.Clear ();
		stime = stime ?? totalTimeSeconds / 2.0f;

		// save the start indices to close the line loop
		int startCurrentIndex = -1;
		int startMeshIndex = 0;

		for (int i = 0; i < polarSteps; i++) {

			int currentIndex = (int)Mathf.Clamp(stime.Value ,0.0f, totalTimeSeconds-1) * (polarSteps) * timeStepsPerSecond + i;
			int meshIndex = currentIndex / MAX_VERTEX;

			//adjust current index to account for which mesh is being indexed
			currentIndex = currentIndex - (meshIndex * MAX_VERTEX);

			if (currentIndex < points [meshIndex].Count) {

				if (startCurrentIndex == -1) {
					startCurrentIndex = currentIndex;
					startMeshIndex = meshIndex;
				}

				linePoints.Add (points [meshIndex] [currentIndex]);
			}
		}

		linePoints.Add (points [startMeshIndex] [startCurrentIndex]);

		line.color = new Color(settings.currentSettings.lineColor.r, settings.currentSettings.lineColor.g, settings.currentSettings.lineColor.b, 1);
		line2d.color = new Color(settings.currentSettings.lineColor.r, settings.currentSettings.lineColor.g, settings.currentSettings.lineColor.b, 1);
		//line.drawTransform = displayCube.transform;
	}


	//
	public void updateObjectColor()
	{
		foreach(GameObject display in displayCubeList)
		{
			display.GetComponent<Renderer>().material.SetColor ("_Color", settings.currentSettings.color);
		}
	}

	public Vector3 normalizePoints(Vector3 point)
	{
		float normalX, normalY, normalZ;


		if ((maxs.x - mins.x) == 0.0f) {
			normalX = 0.0f;
		} else {
			normalX = Mathf.Clamp(((point.x - mins.x) / (maxs.x - mins.x)) *(0.5f-(-0.5f))-0.5f, -0.5f, 0.5f);
		}

		if ((maxs.y - mins.y) == 0.0f) {
			normalY = 0.0f;
		} else {
			normalY = Mathf.Clamp(((point.y - mins.y) / (maxs.y - mins.y)) *(0.5f-(-0.5f))-0.5f, -0.5f, 0.5f);
		}

		if ((maxs.z - mins.z) == 0.0f) {
			normalZ = 0.0f;
		} else {
			normalZ = Mathf.Clamp(((point.z - mins.z) / (maxs.z - mins.z)) *(0.5f-(-0.5f))-0.5f, -0.5f, 0.5f);
		}

		Vector3 outV = new Vector3 (normalX, normalY, normalZ);
		return outV;

	}

	// draw the labels for axis
	void drawAxisLabels()
	{
		GameObject.Find("/Mesh Generation/LineCameraHolder/Camera/AxisHolderLine/Xaxis/xMinTic").GetComponentInChildren<TextMesh>().text = mins.x.ToString();
		GameObject.Find("/Mesh Generation/LineCameraHolder/Camera/AxisHolderLine/Yaxis/yMinTic").GetComponentInChildren<TextMesh>().text = mins.y.ToString();

		GameObject.Find("/Mesh Generation/LineCameraHolder/Camera/AxisHolderLine/Xaxis/xMaxTic").GetComponentInChildren<TextMesh>().text = maxs.x.ToString();
		GameObject.Find("/Mesh Generation/LineCameraHolder/Camera/AxisHolderLine/Yaxis/yMaxTic").GetComponentInChildren<TextMesh>().text = maxs.y.ToString();

		GameObject.Find("/Mesh Generation/displayCubeHolder/AxisHolderLine3d/Xaxis/xMinTic").GetComponentInChildren<TextMesh>().text = mins.x.ToString();
		GameObject.Find("/Mesh Generation/displayCubeHolder/AxisHolderLine3d/Yaxis/yMinTic").GetComponentInChildren<TextMesh>().text = mins.y.ToString();
		GameObject.Find("/Mesh Generation/displayCubeHolder/AxisHolderLine3d/Zaxis/zMinTic").GetComponentInChildren<TextMesh>().text = mins.z.ToString();

		GameObject.Find("/Mesh Generation/displayCubeHolder/AxisHolderLine3d/Xaxis/xMaxTic").GetComponentInChildren<TextMesh>().text = maxs.x.ToString();
		GameObject.Find("/Mesh Generation/displayCubeHolder/AxisHolderLine3d/Yaxis/yMaxTic").GetComponentInChildren<TextMesh>().text = maxs.y.ToString();
		GameObject.Find("/Mesh Generation/displayCubeHolder/AxisHolderLine3d/Zaxis/zMaxTic").GetComponentInChildren<TextMesh>().text = maxs.z.ToString();
	}

	public void showMenu()
	{
		//clear out old meshes
		foreach (GameObject item in displayCubeList) {
			Destroy (item);
		}

		displayCubeList.Clear ();
		menu.SetActive (true);
	}

	// Update is called once per frame
	void LateUpdate () {
		if (line != null && line.points3.Count > 0) {
			//line.drawTransform = display.transform;
			line.Draw3D ();

			// this is weird i know
			line2d.Draw3D ();
		}
	}
}
