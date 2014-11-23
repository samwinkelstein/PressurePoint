using UnityEngine;
using System.Collections;

public class fluidInit : MonoBehaviour {
	public Transform Fluid;
	private Transform f;
	// Use this for initialization
	// Update is called once per frame
	void Awake(){
		for(float i = - 8f ; i < 8f; i+=.5f) {
			for(float j = -8f; j < 8f; j+=.5f) {
				//transform.Rotate(Vector3.forward * i);
				f = (Transform)Instantiate(Fluid, new Vector3(i, j,0), Quaternion.identity);
				
			}
		}
		}
	void Update () {

		Destroy (this);
		
		}

}
