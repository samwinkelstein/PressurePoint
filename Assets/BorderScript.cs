using UnityEngine;
using System.Collections;

public class BorderScript : MonoBehaviour {
	private GameObject Player;
	private bool follow = false;
	// Use this for initialization
	void Start () {
		Player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (follow) {
						transform.position = Player.transform.position;
				}
		follow = true;

	}
}
