using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public GameObject target;
	public GameObject cameraPosition;
	public float metersUp = 0;


	public float smoothFactor = 1f;

	// Update is called once per frame
	void LateUpdate () {
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position + transform.up * metersUp), Time.deltaTime * 5f);
		transform.position = Vector3.Lerp(transform.position, cameraPosition.transform.position, Time.deltaTime * smoothFactor);
	}
}
