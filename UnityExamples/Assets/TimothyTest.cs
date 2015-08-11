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

		Vector3 nearest = Vector3.zero;
		Vector3 p1 = m_camera.transform.position;
		Vector3 p2 = m_camera.transform.position + m_camera.transform.forward;

		Vector3 t1 = m_camera.transform.position + m_camera.transform.forward * 5 + m_camera.transform.up/5;
		Vector3 t2 = m_camera.transform.position + m_camera.transform.forward * 5 + m_camera.transform.right/5;
		Vector3 t3 = m_camera.transform.position + m_camera.transform.forward * 5 + m_camera.transform.up/5 + m_camera.transform.right/5;

		Vector3 normal = Vector3.Cross (t1 - t3, t2 - t3);
		m_bounceSurface.transform.rotation = Quaternion.FromToRotation (Vector3.up, -normal);

		m_bounceSurface.transform.position = (t1 + t2 + t3) / 3;

//		return;

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
