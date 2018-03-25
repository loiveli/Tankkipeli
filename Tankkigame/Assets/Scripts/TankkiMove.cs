using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankkiMove : MonoBehaviour {
    
    public GameObject Marker;
    public GameObject russia;
	public GameObject russiaChild;
    float valittuX;
    float valittuY;
    float nopeus;
    bool active;
    public Transform Alus;
    private Rigidbody2D rb2d;
    float angle;
    Vector3 pos;
    // Use this for initialization
    public Vector3 mousePos;
    public Vector3 mousePosLock;
    Vector3 dir;
    Vector2 kohdeRound;
    public bool moving;
    bool locked;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        nopeus = 0.1f;
        moving = false;
        locked = false;
        for(int i= -2; i <= 2; i++){
            
            for(int j= -2; j <= 2; j++){
            russiaChild = Instantiate(russia, new Vector3(transform.position.x + i, transform.position.y + j, 0), transform.rotation);
            russiaChild.transform.parent = transform;
        
        }
        
        }
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetMouseButtonDown(0)&&!moving )
        {
            if(Mathf.Abs(mousePos.x -transform.position.x ) <=2&&Mathf.Abs(mousePos.y -transform.position.y )<=2){
            mousePosLock = mousePos;
            locked = true;
            Debug.Log(mousePos);
            Destroy(GameObject.FindGameObjectWithTag("Marker"));
            Instantiate(Marker, mousePos, Quaternion.identity);
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Space)&&locked){
            Destroy(GameObject.FindGameObjectWithTag("Marker"));
            moving = true;
        }
    }
    void FixedUpdate()
    {
        if(!moving) transform.rotation = Quaternion.identity;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition)  ;
        mousePos = new Vector3(Mathf.Round(mousePos.x),Mathf.Round(mousePos.y), 0  );
        if(moving){
        
        transform.position = Vector3.MoveTowards(transform.position, mousePosLock, nopeus);
        dir = mousePosLock - transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
        if(transform.position == mousePosLock){
        
            moving = false;
        
        }
		
		
    }
}
