using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateManifold : MonoBehaviour {

	public List<Vector3> points = new List<Vector3>();
	public List<Vector3> pointsLine = new List<Vector3>();
	public List<float> Pt = new List<float>();
	public List<float> Qt = new List<float>();
	public int timeStepsPerSecond;
	public int polarSteps;

	public List<Vector3> getLineAtTime(Asset asset, float currentTime, int polarSteps)
	{
		pointsLine.Clear ();

		for (int i = 0; i < polarSteps; i++) {
			pointsLine.Add (points [(int)currentTime * polarSteps * timeStepsPerSecond + i]);
		}

		return pointsLine;
	}

	public List<Vector3> buildOneManifold(Asset asset, int totalTimeSeconds, int timeStepsPerSecond, int polarSteps, float euLatency)
	{
		points.Clear ();
		Pt.Clear ();
		Qt.Clear ();
		this.polarSteps = polarSteps;
		this.timeStepsPerSecond = timeStepsPerSecond;

		// new latency with added eu
		float latency = asset.latency + euLatency;

		//grid size for mesh point generation
		float incrementT = 1.0f/(float)timeStepsPerSecond;
		float incrementP = 2*Mathf.PI/(float)polarSteps;

		int maxCountT = Mathf.FloorToInt (totalTimeSeconds / incrementT);
		int maxCountP = polarSteps;

		float currentTime = 0;
		float currentPolar = 0;

		float maxS = Mathf.Max (asset.maxP, asset.maxQ);


		// the calculated point
		Vector3 point = Vector3.zero;

		for (int i = 0; i < maxCountT; i++) {
			Pt.Add (0.0f);
			Qt.Add (0.0f);
			// if time hasn't reached latency time set to 0
			if (currentTime < latency) {
				Pt [i] = 0.0f;
				Qt [i] = 0.0f;
			} else {

				//handle p agility
				if (currentTime < (latency + asset.agilityP)) {
					Pt [i] = asset.maxP * (currentTime - latency) / asset.agilityP;
				} else {
					Pt [i] = asset.maxP;
				}

				// handle q agility
				if (currentTime < (latency + asset.agilityQ)) {
					Qt [i] = asset.maxQ * (currentTime - latency) / asset.agilityQ;			
				} else {
					Qt [i] = asset.maxQ;
				}
			}

			currentPolar = 0.0f;

			for (int j = 0; j < maxCountP; j++) {

				point.z = currentTime;

				if ((Pt [i] * Qt [i]) > 0.0f) {
					float x = maxS * Mathf.Cos (currentPolar);
					float y = maxS * Mathf.Sin (currentPolar);

					point.x = Mathf.Sign (x) * Mathf.Min (Mathf.Abs (x), Pt [i]); //limit to the maximum Pt
					point.y = Mathf.Sign (y) * Mathf.Min (Mathf.Abs (y), Qt [i]); //limit to the maximum Pt

					float pointAngle = Mathf.Atan2(point.y, point.x);

					if (Mathf.Abs (currentPolar - pointAngle) > 0.001f) {
						float testX = point.y / Mathf.Tan (currentPolar);
						float testY = point.x * Mathf.Tan (currentPolar);

						if (Mathf.Abs (testX) < Pt [i]) {
							point.x = testX;
						} else {
							point.x = Mathf.Sign (testX) * Pt [i];
						}

						if (Mathf.Abs (testY) < Qt [i]) {
							point.y = testY;
						} else {
							point.y = Mathf.Sign (testY) * Qt [i];
						}
					}
				} else {

					if (Mathf.Abs (Mathf.Cos (currentPolar)) == 1) {
						point.x = Mathf.Sign (Mathf.Cos (currentPolar)) * Pt [i];
					} else {
						point.x = 0.0f;
					}

					if (Mathf.Abs (Mathf.Sin (currentPolar)) == 1) {
						point.y = Mathf.Sign (Mathf.Sin (currentPolar)) * Qt [i];
					} else {
						point.y = 0.0f;
					}
				}

				currentPolar += incrementP;
				points.Add (point);
			}
			currentTime += incrementT;
		}

		if (asset.energy > 0) {

			// sum up each "column" of data
			// if that sum is greater than energy zeroize the rest of that column
			// since each row is appended end to end, need to figure out the start of that column, which is just the number of polarsteps

			//shift the "columns" after going through each row
			for (int i = 0; i < maxCountP; i++){

				float sumIt = 0.0f;
				bool maxedOut = false;

				//shift the row down and sum, once done reset and move onto next column
				for (int j = 0; j < maxCountT; j++) {

					sumIt += points [j*maxCountP + i].x;

					if (Mathf.Abs (sumIt * incrementT) > asset.energy || maxedOut) {
						Vector3 tempPoint = points [j*maxCountP + i];
						tempPoint.x = 0.0f;
						points [j*maxCountP + i] = tempPoint;
						maxedOut = true;
					}
				}
			}
		}

		return points;
	}
}