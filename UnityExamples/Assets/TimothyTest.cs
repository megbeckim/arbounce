using UnityEngine;
using System.Collections;

public class TimothyTest : MonoBehaviour {
	public TangoPointCloud m_tangoPointCloud;
	public Camera m_camera;
	public GameObject m_bounceSurface;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("pos " + m_camera.transform.position);
		Debug.Log ("fwd " + m_camera.transform.forward);

		Vector3 nearest = Vector3.zero;
		Vector3 p1 = m_camera.transform.position;
		Vector3 p2 = m_camera.transform.position + m_camera.transform.forward;
		double minDistance = double.MaxValue;
		int nearestIndex = 0;
		for (int index = 0; index < m_tangoPointCloud.m_pointsCount; index++) {
			Vector3 p0 = m_tangoPointCloud.m_points[index];
			double distance = Vector3.Cross(p0 - p1, p0 - p2).magnitude;
			if (distance < minDistance) {
				minDistance = distance;
				nearest = p0;
				nearestIndex = index;
			}
		}

		Debug.Log ("middle (" + nearestIndex + ") " + nearest);
		m_bounceSurface.transform.position = nearest;
	}
}
