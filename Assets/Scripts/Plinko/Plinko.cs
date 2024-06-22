using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plinko : MonoBehaviour
{
    [SerializeField] private GameObject Rwall, Lwall;
    [SerializeField] private GameObject Brick;
    [SerializeField] private GameObject X;
    public GameObject firstL;

    public float widhtBrick;
    public float coordLx, coordRx, coordTy, coordLy; //Rx - (widtx + x) Rl - (widt*2 * (rows + 1) - width) < bounds - Rx <= 1
    public int rows = 2;
    void Start()
    {
        coordRx = Mathf.Abs(Rwall.transform.localPosition.x) - Rwall.transform.localScale.x;
        coordLx = Mathf.Abs(Lwall.transform.localPosition.x) - Lwall.transform.localScale.x;
        coordTy = Rwall.transform.localPosition.y + Rwall.transform.localScale.y;
        coordLy = Rwall.transform.localPosition.y;//0.875
        for(int i=0; i < rows; i++){
            firstL = Instantiate(Brick,new Vector2(4f + Brick.transform.localScale.x + (Brick.transform.localScale.x *i), 4.2f - ((Brick.transform.localScale.y * 1.75f) * i)), Quaternion.identity);
            for(int j=0; j <= i; j++){
                Instantiate(Brick,new Vector2(firstL.transform.localPosition.x - (((j+1) * Brick.transform.localScale.x * 2)), firstL.transform.localPosition.y), Quaternion.identity);
            }
        }

        for(int i= 0; i < rows; i++){
            Instantiate(X,new Vector2(firstL.transform.localPosition.x - Brick.transform.localScale.x - (Brick.transform.localScale.x*(i+i)), firstL.transform.localPosition.y - (Brick.transform.localScale.y * 1.75f)), Quaternion.identity);
        }


    
    }


    public void Bounds() {

    }


   // Update is called once per frame
    void Update()
    {
        
    }
}
