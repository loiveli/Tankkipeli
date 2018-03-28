using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveControl : MonoBehaviour {

	// Use this for initialization
	public GameObject Tankki;
	
	// Update is called once per frame
	void Start(){
		Tankki = GameObject.FindWithTag("Player");
	}
	void FixedUpdate () {

			
			gameObject.GetComponent<SpriteRenderer>().enabled = Tankki.GetComponent<TankkiMove>().boxActive;
			if(!Tankki.GetComponent<TankkiMove>().boxActive){
				Destroy(gameObject);
			}
			
			
	}
}
