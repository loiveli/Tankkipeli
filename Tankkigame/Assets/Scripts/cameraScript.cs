using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;
	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if(!player.GetComponent<TankkiMove>().moving){
			transform.position = Vector3.MoveTowards(transform.position, player.transform.position+ offset, 0.05f) ;
		}
		
		
	}
}
