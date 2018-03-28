using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoMove : MonoBehaviour {

	public Vector3 shootTarget;
	// Use this for initialization
	Vector3 dir;
	Vector3 startPos;
	float angle;
	Vector3 currentPos;
	void Start () {
		dir = shootTarget - transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
		startPos = transform.position;
		currentPos = transform.position;
	}
	
	// Update is called once per frame
	/*public void UpdateAmmo () {
		currentPos = Vector3.MoveTowards(transform.position, shootTarget, 1);
	}*/
	void FixedUpdate(){
		transform.position = Vector3.MoveTowards(transform.position, shootTarget, 0.1f);
		
		 
		if(transform.position == shootTarget){
			Destroy(gameObject);
		}
	}
}
