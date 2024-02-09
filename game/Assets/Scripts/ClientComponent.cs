using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = System.Random;

public class ClientComponent : MonoBehaviour
{
    public static Transform target;
    [SerializeField] Sprite[] variacionesClientes;
    private SpriteRenderer client;
    public static Camera targetCamera;
    Vector3 vec = new Vector3(0f, -90f, 0f);
    public int varId;
    private Random _random = new Random();
    private GameObject pedido;
    private GameObject rectanguloTimer;

    private float tiempoLimite;
    public float maxTime;

    private int hamburguesa, complemento, bebida; 
    // h.Ternera 0, h.Pollo 1, h.Vegana 2
    // c.Patatas 0, c.Deluxe 1, c.Nuggets 2
    // b.Colacoca 0, b.Nafta 1, c.Sprint 2

    // Start is called before the first frame update
    void Start()
    {
        client = transform.Find("1_c").gameObject.GetComponent<SpriteRenderer>();
        targetCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        pedido = transform.GetChild(0).gameObject;
        rectanguloTimer=transform.GetChild(1).GetChild(0).gameObject;
        varId = _random.Next(6);
        //Debug.LogWarning(varId);
        CambiarSprite(varId);
        NuevoPedido();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
            NuevoPedido();
        }
        transform.LookAt(transform.position + targetCamera.transform.rotation * Vector3.left);
        transform.Rotate(vec, Space.World);

        tiempoLimite -= Time.deltaTime;
        rectanguloTimer.transform.position = new Vector3(
            -(tiempoLimite/maxTime),
            rectanguloTimer.transform.position.y,
            rectanguloTimer.transform.position.z);
    }

    private void Enfadarse()
    {
        CambiarSprite(varId + 6);
    }

    private void CambiarSprite(int i)
    {
        client.sprite = variacionesClientes[i];
    }

    private void NuevoPedido()
    {
        foreach(Transform child in pedido.transform)
        {
            child.gameObject.SetActive(false);
        }
        hamburguesa = _random.Next(0,3);
        complemento = _random.Next(3,6);
        bebida = _random.Next(6,9);
        pedido.transform.GetChild(hamburguesa).gameObject.SetActive(true);
        pedido.transform.GetChild(complemento).gameObject.SetActive(true);
        pedido.transform.GetChild(bebida).gameObject.SetActive(true);

        Debug.LogWarning(hamburguesa+" - "+complemento+" - "+bebida);
        tiempoLimite = maxTime;
    }

    private void Irse() { }
    private void Llegar() { }
}

