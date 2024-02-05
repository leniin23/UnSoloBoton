using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class spawnController : MonoBehaviour
{
    public float speed;                 //velocidad de movimiento
    public float timeMoving;            //tiempo que se mueve en cinta
    public float delay;                 //cada cuanto se mueve
    public GameObject objectToSpawn;    //va a ser prefab, objeto a spwanear
    
    private Vector3 initialPos;        
    private float timer;
    private float timer1;
    private List<GameObject> panes =new();
    private bool isMoving;
    void Start()
    {
        initialPos = new Vector3(transform.position.x +2.72f,transform.position.y + 0.668f,transform.position.z);
        timer = 0;
        timer1 = 0;
    }

   
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > delay)
        {
            GameObject pan =spawn();
            panes.Add(pan);
            timer = 0;
            isMoving = true;
        }
        

        if (isMoving)
        {
            timer1 += Time.deltaTime;
            foreach (var pan in panes)
            {
                pan.transform.Translate(Time.deltaTime * speed * Vector3.left);
            }

            if (timer1 > timeMoving)
            {
                isMoving = false;
                timer1 = 0;
            }

        }
    }

    GameObject spawn()
    {
        return Instantiate(objectToSpawn,initialPos,transform.rotation);
    }
    
    
}
