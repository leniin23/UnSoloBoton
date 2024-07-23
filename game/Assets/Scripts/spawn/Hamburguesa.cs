using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using spawn;
using UnityEngine;

public class Hamburguesa : MonoBehaviour, IFood
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
        {
            timer += Time.deltaTime;
            if (timer < 2)
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
        
        if (other.gameObject.CompareTag("bandeja"))
        {
            bandeja = other.gameObject;
            gameObject.transform.parent = other.gameObject.transform;
            other.gameObject.GetComponent<BandejaInfo>().setHamburguesa(Number);
            bandejaMove = true;
        }
    }

    public int Number { get; set; }
}
