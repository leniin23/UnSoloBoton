using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.Android;

public class Enchufe : MonoBehaviour
{
    [SerializeField] private Color color;

    private static MachineManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = transform.parent.parent.GetComponent<MachineManager>();
        GetComponent<MeshRenderer>().material.color = color;
        GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", color);
    }

    private void OnTriggerEnter(Collider other)
    {
        ProcessCollision(other);
    }
    private void OnTriggerExit(Collider other)
    {
        //ProcessCollision(other, true);
        var cable = other.transform.GetComponent<Rigidbody>();
        if(cable == null) return;
        if (!cable.isKinematic)
        {
            //Debug.Log("EXIT");
            manager.TurnOffMainBulb(color);
        }
    }
    private void ProcessCollision(Collider other, bool exit = false)
    {
        //Debug.Log("Triggered");
        var enchufe = other.transform.parent.GetComponent<Cable>();
        if (enchufe.GetColor().Compare(color))
        {
            if (enchufe.IsConnected)
            {
                return;
            }
            if(manager.LightMainBulb(color))
                enchufe.Connect(transform.position);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
