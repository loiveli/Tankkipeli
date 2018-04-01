using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankkiMove : MonoBehaviour
{
    public int playerNum;
    public GameObject ammus;
    public GameObject Tykki;
    public GameObject Marker;
    public GameObject russia;
    public GameObject russiaChild;
    List<GameObject> ammukset = new List<GameObject>(); 
    public GameObject controller;
    float valittuX;
    float valittuY;
    float nopeus;
    bool active;
    public Transform Alus;
    List<Vector3> shootPositions= new List<Vector3>();
    List<Vector3> targetPositions= new List<Vector3>();
    private Rigidbody2D rb2d;
    float angle;
    Vector3 lastPos;
    Vector3 pos;
    // Use this for initialization
    public Vector3 mousePos;
    public Vector3 movePos;
    Vector3 dir;
    List<Vector3> backTrackPos = new List<Vector3>();
    Vector2 kohdeRound;
  
    public bool boxActive;
    
    public int actionPoints;
    
    
    enum tila { moving, shooting, moveNow, shootNow, backTrackNow };
    tila TankkiState;
    int range;
    GameObject[] tuhottavat;
    void Start()
    {
        //rb2d = GetComponent<Rigidbody2D>();
        range = 4;
        nopeus = 0.1f;
        
        boxActive = true;
        actionPoints = 3;
        
        ShowMove(actionPoints, transform);
        TankkiState = tila.moving;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && TankkiState == tila.moving)
        {

            TankkiLiikeInit(mousePos);

        }
        if (Input.GetMouseButtonDown(0) && TankkiState == tila.shooting)
        {

            shoot(transform.position ,mousePos, actionPoints);
            
            
            endMyTurn();
        }
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            endMyTurn();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            backTrack();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if(TankkiState == tila.moving){
                boxActive = false;
                TankkiState = tila.shooting;
                
            }else if(TankkiState == tila.shooting){
                boxActive = true;
                DestroyWithTag("Marker");
                ShowMove(actionPoints, transform);
                TankkiState = tila.moving;
            }
        }
    }
    void Merkki(Vector3 merkkiPos)
    {
        if (checkMouse(mousePos, TankkiState))
        {
            DestroyWithTag("Marker");
            Instantiate(Marker, mousePos, Quaternion.identity);
        }

    }
    IEnumerator TankkiLiike(Vector3 target, float speed)
    {
        while(transform.position !=target){
        transform.position = Vector3.MoveTowards(transform.position, target, speed);
        dir = movePos - transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        yield return new WaitForSeconds(1f);
        }

    }
    bool checkAmmo(Vector3 tankPos){
        foreach (GameObject ammus in ammukset){
            if(ammus&&ammus.transform.position == tankPos){
                return true;
            }
        }
        return false;
    }
    IEnumerator startTurn(){
        yield return new WaitForSeconds(2);
        boxActive = true;
        DestroyWithTag("Marker");       
        actionPoints += 2;
        ShowMove(actionPoints, transform);
        TankkiState = tila.moving;
        backTrackPos.RemoveRange(0, backTrackPos.Count);
        
    }
    void endMyTurn(){
       controller.GetComponent<TurnControl>().pelaajat.RemoveAt(playerNum);
       StartCoroutine(startTurn());
    }
    void backTrack()
    {
        if (backTrackPos.Count > 0)
        {
            movePos = backTrackPos[backTrackPos.Count - 1];
            TankkiState = tila.moveNow;
            backTrackPos.RemoveAt(backTrackPos.Count - 1);
            actionPoints += (int)Mathf.Round(Vector3.Distance(movePos, transform.position));
        }

    }
    void shoot(Vector3 selfPos,Vector3 trgt, int AP)
    {
        if(AP >0){
            ammukset.Add(Instantiate(ammus, transform.position, Quaternion.identity)); 
            ammukset[ammukset.Count-1].GetComponent<AmmoMove>().shootTarget = trgt;
            actionPoints--;
        }
        
    }
    void TykkiMeme(Vector3 targetTykki)
    {
        if (checkMouse(targetTykki, TankkiState))
        {
            Merkki(targetTykki);
            dir = targetTykki - Tykki.gameObject.transform.position;
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Tykki.gameObject.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        }

    }
    void ShowMove(int AP, Transform tankPosition)
    {
        for (int i = -AP; i <= AP; i++)
        {


            russiaChild = Instantiate(russia, new Vector3(tankPosition.position.x + i, tankPosition.position.y, 0), transform.rotation);

            russiaChild = Instantiate(russia, new Vector3(tankPosition.position.x, tankPosition.position.y + i, 0), tankPosition.rotation);




        }
    }
    bool checkMouse(Vector3 MP, tila state)
    {
        if (state == tila.moving)
        {
            if (Mathf.Abs(MP.x - transform.position.x) < 1 && Mathf.Abs(MP.y - transform.position.y) < actionPoints + 1)
            {
                return true;
            }
            else if (Mathf.Abs(MP.x - transform.position.x) < actionPoints + 1 && Mathf.Abs(MP.y - transform.position.y) < 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (state == tila.shooting)
        {
            if (Mathf.Abs(MP.x - transform.position.x) < range + 1 && Mathf.Abs(MP.y - transform.position.y) < range + 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

    }

    void TankkiLiikeInit(Vector3 MP1)
    {
        if (checkMouse(MP1, TankkiState))
        {
            backTrackPos.Add(transform.position);

            movePos = MP1;
            TankkiState = tila.moveNow;
            actionPoints -= (int)Mathf.Round(Vector3.Distance(movePos, transform.position));

        }

    }
    void DestroyWithTag(string tagDestroy)
    {
        tuhottavat = GameObject.FindGameObjectsWithTag(tagDestroy);

        for (var i = 0; i < tuhottavat.Length; i++)
        {
            Destroy(tuhottavat[i]);
        }
    }

    void FixedUpdate()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y), 0);
        if (TankkiState == tila.shooting)
        {
            TykkiMeme(mousePos);
        }
        else
        {

            Tykki.gameObject.transform.rotation = transform.rotation;


            if (TankkiState == tila.moveNow)
            {

                TankkiLiike(movePos, nopeus);
                boxActive = false;
            }
            else if (TankkiState == tila.moving)
            {
                boxActive = true;
            }
            if (transform.position == movePos && TankkiState == tila.moveNow)
            {
                TankkiState = tila.moving;
                ShowMove(actionPoints, transform);
            }



        }
    }
}
