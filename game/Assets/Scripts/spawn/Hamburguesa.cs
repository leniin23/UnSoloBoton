using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Hamburguesa : MonoBehaviour
{
    private bool bandejaMove;
    private float timer;
    private GameObject bandeja;
    private spawnController spawn;
   
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        spawn = GameObject.Find("Spawner").GetComponent<spawnController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (bandejaMove)
        {Debug.Log(spawn.getSpeed());
            timer += Time.deltaTime;
            if (timer < spawn.getTime())
            {
                bandeja.transform.Translate(spawn.getSpeed() * Time.deltaTime * Vector3.left);
                
            }
            else
            {
                timer = 0;
                bandejaMove = false;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("colisiontriger " + other.gameObject.name );
        if (other.gameObject.CompareTag("bandeja"))
        {
            
            Debug.Log("gameObject.GetComponent<BandejaInfo>() == null");
            bandeja = other.gameObject;
            gameObject.transform.parent = other.gameObject.transform;
            //transform.parent.SetParent(other.transform);
            other.gameObject.GetComponent<BandejaInfo>().setHamburguesa();
            bandejaMove = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("colision " + other.gameObject.name );
    }
}
