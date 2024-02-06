using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class Enchufe : MonoBehaviour
{
    [SerializeField] private Color color;

    private static MachineManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = transform.parent.parent.GetComponent<MachineManager>();
        var renderer = GetComponent<MeshRenderer>();
        renderer.material.color = color;
    }

    private void OnTriggerEnter(Collider other)
    {
        ProcessCollision(other);
    }
    private void OnTriggerExit(Collider other)
    {
        ProcessCollision(other, true);
    }
    private void ProcessCollision(Collider other, bool exit = false)
    {
        Debug.Log("Triggered");
        var enchufe = other.transform.parent.GetComponent<Cable>();
        if (enchufe.GetColor().Compare(color))
        {
            if (enchufe.IsConnected)
            {
                if(exit) manager.TurnOffMainBulb(color);
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
