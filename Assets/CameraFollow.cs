using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float smoothTime = 0.6f;

	private Vector3 vel = Vector3.zero;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 targetPos = new Vector3(target.position.x, target.position.y, -10);
		this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPos, ref vel, smoothTime);
	}
}
