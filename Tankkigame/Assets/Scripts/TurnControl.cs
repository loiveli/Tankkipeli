using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnControl : MonoBehaviour {
	public List<GameObject> pelaajat = new List<GameObject>(); 
	// Use this for initialization
	void Start () {
			foreach (GameObject pelaaja in GameObject.FindGameObjectsWithTag("Player")){
				pelaajat.Insert(pelaaja.GetComponent<TankkiMove>().playerNum, pelaaja);
			}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		
	}
}
