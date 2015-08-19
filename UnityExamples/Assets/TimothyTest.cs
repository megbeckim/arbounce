﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimothyTest : MonoBehaviour {

	public class DistanceAndPoint {
		public Vector3 point;
		public double distance;

		public DistanceAndPoint(double aDistance, Vector3 aPoint) {
			this.point = aPoint;
			this.distance = aDistance;
		}
	}

	public TangoPointCloud m_tangoPointCloud;
	public Camera m_camera;
	public GameObject m_bounceSurfacePrefab;

	// Use this for initialization
	void Start () {
	
	}


	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			updateBounceSurface ();
		}
		
		for (var i = 0; i < Input.touchCount; ++i) {
			if (Input.GetTouch(i).phase == TouchPhase.Began) {
				updateBounceSurface ();
			}
		}
	}

	private void updateBounceSurface() {

		Vector3 p1 = m_camera.transform.position;
		Vector3 p2 = m_camera.transform.position + m_camera.transform.forward;

		List<DistanceAndPoint> distancesAndPoints = new List<DistanceAndPoint>();

		for (int index = 0; index < m_tangoPointCloud.m_pointsCount; index++) {
			Vector3 p0 = m_tangoPointCloud.m_points[index];
			double distance = Vector3.Cross(p0 - p1, p0 - p2).magnitude;
			distancesAndPoints.Add(new DistanceAndPoint (distance, p0) );
		}

		distancesAndPoints.Sort (delegate(DistanceAndPoint o1, DistanceAndPoint o2) { 
			if (o2.distance > o1.distance)
				return -1;
			else if (o2.distance < o1.distance)
				return 1;
			else 
				return 0;
		});

		if (distancesAndPoints.Count == 0) {
			return;
		}

		Vector3 c1 = distancesAndPoints[0].point;
		Vector3 c2 = distancesAndPoints[1].point;
		Vector3 c3 = distancesAndPoints[2].point;

		Vector3 cNormal = Vector3.Cross (c1 - c3, c2 - c3);

		GameObject m_bounceSurface = (GameObject)Instantiate (m_bounceSurfacePrefab);
		m_bounceSurface.SetActive (true);

		m_bounceSurface.transform.rotation = Quaternion.FromToRotation (Vector3.up, -cNormal);
		
		m_bounceSurface.transform.position = (c1 + c2 + c3) / 3;
	}
}
