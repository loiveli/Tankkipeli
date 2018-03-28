using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {
	List<GameObject> palyers = new List<GameObject>();
	public GameObject ctrl;
	public GameObject player;
	private Vector3 offset;
	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//if(!player.GetComponent<TankkiMove>().moving){
			palyers = ctrl.GetComponent<TurnControl>().pelaajat;
			transform.position =Vector3.Lerp(transform.position,palyers[0].transform.position +offset, 0.05f)  ;
		//}
		
		//transform.position = player.transform.position+offset;
	}	
}
