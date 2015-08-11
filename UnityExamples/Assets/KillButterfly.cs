using UnityEngine;
using System.Collections;

public class KillButterfly : MonoBehaviour {
	public Camera m_camera;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		this.gameObject.transform.position = m_camera.transform.position - m_camera.transform.right * 1;
	}
}
