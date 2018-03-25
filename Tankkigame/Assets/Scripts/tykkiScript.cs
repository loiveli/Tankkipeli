using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tykkiScript : MonoBehaviour {
	public Vector3 mousePos;
	public Vector3 suunta;
	public Vector3 suuntaNorm;
	public bool shootMode;

	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(shootMode){
			mousePos= Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,transform.position.z- Camera.main.transform.position.z ));
		suunta= transform.position-mousePos;
		suuntaNorm = suunta.normalized;
		float angle = Mathf.Atan2(suunta.y, suunta.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle+90));
		}
		
	}
}
