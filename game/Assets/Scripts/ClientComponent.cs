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
    public float timePercent;
    private bool angryBool;

    private float tiempoCooldown;
    [SerializeField] private float maxCooldown;

    public int speed; 
    public int estado = 0;

<<<<<<< Updated upstream
    private int hamburguesa, complemento, bebida; 
=======
    private int hamburguesa, complemento, bebida;

    private Transform selfTransform;
>>>>>>> Stashed changes
    // h.Ternera 0, h.Pollo 1, h.Vegana 2
    // c.Patatas 0, c.Deluxe 1, c.Nuggets 2
    // b.Colacoca 0, b.Nafta 1, c.Sprint 2

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< Updated upstream
=======
        selfTransform = transform;
        
        ganancias = 0;
>>>>>>> Stashed changes
        client = transform.Find("1_c").gameObject.GetComponent<SpriteRenderer>();
        targetCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        pedido = transform.GetChild(0).gameObject;
        rectanguloTimer=transform.GetChild(1).GetChild(0).gameObject;
        varId = _random.Next(6);
        //Debug.LogWarning(varId);
        CambiarSprite(varId);
        NuevoPedido();
        StartCoroutine(Movement());
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
        if(Input.GetKeyDown(KeyCode.P)){
            NuevoPedido();
        }
=======
        // if (Input.GetKeyDown(KeyCode.P))
        // {
        //     NuevoPedido();
        // }
>>>>>>> Stashed changes
        transform.LookAt(transform.position + targetCamera.transform.rotation * Vector3.left);
        transform.Rotate(vec, Space.World);

        
        timePercent = tiempoLimite / maxTime;
<<<<<<< Updated upstream
        rectanguloTimer.transform.localScale = new Vector3(
            timePercent*2,
            rectanguloTimer.transform.localScale.y,
            rectanguloTimer.transform.localScale.z);


        //moverse esto se tendria que hacer con estados pero que pereza
        if (estado != 0)
        {
            if (estado == 1) //bajar
            {
                transform.position = new Vector3(
                transform.position.x,
                transform.position.y - speed * Time.deltaTime,
                transform.position.z);
                if (transform.position.y <= -1f){ Debug.Log("joder macho"); tiempoCooldown = maxCooldown; estado = 3;}
            }
            else if (estado == 2) //subir
            {
                transform.position = new Vector3(
                transform.position.x,
                transform.position.y + speed * Time.deltaTime,
                transform.position.z);
                if (transform.position.y >= 3f) { estado = 0; }
            }
            else if(estado == 3) //cooldown
            {
                tiempoCooldown -= Time.deltaTime;
                if (tiempoCooldown<=0)
                {
                    varId = _random.Next(6);
                    CambiarSprite(varId);
                    NuevoPedido();
                    estado = 2;
                }
            }
        }
        else
        {
            tiempoLimite -= Time.deltaTime;
            if (tiempoLimite <= 0)
            {
                Irse(false);
                rectanguloTimer.gameObject.SetActive(false);
            }
            if (!angryBool && tiempoLimite < (maxTime * 0.45))
            {
                Enfadarse();
            }
        }
=======
        var localScale = rectanguloTimer.transform.localScale;
        localScale = new Vector3(
            timePercent * 2,
            localScale.y,
            localScale.z);
        rectanguloTimer.transform.localScale = localScale;
>>>>>>> Stashed changes
    }

    private IEnumerator Movement()
    {
        for (;;)
        {
            switch (estado)
            {
                case 0:
                    tiempoLimite -= Time.deltaTime;
                    if (tiempoLimite <= 0)
                    {
                        Irse(false, 2);
                        rectanguloTimer.gameObject.SetActive(false);
                    }

                    if (!angryBool && tiempoLimite < (maxTime * 0.45))
                    {
                        Enfadarse();
                    }

                    break;
                //bajar
                case 1:
                {
                    var localPosition = selfTransform.localPosition;
                    localPosition = new Vector3(
                        transform.localPosition.x,
                        localPosition.y - speed * Time.deltaTime,
                        localPosition.z);
                    selfTransform.localPosition = localPosition;
                    if (transform.localPosition.y <= -4f)
                    {
                        tiempoCooldown = maxCooldown;
                        estado = 3;
                    }

                    break;
                }
                //subir
                case 2:
                {
                    var localPosition = selfTransform.localPosition;
                    localPosition = new Vector3(
                        localPosition.x,
                        localPosition.y + speed * Time.deltaTime,
                        localPosition.z);
                    selfTransform.localPosition = localPosition;
                    if (transform.localPosition.y >= 0)
                    {
                        estado = 0;
                    }

                    break;
                }
                //cooldown
                case 3:
                {
                    if (comida) Destroy(comida);
                    tiempoCooldown -= Time.deltaTime;
                    if (tiempoCooldown <= 0)
                    {
                        varId = _random.Next(6);
                        CambiarSprite(varId);
                        NuevoPedido();
                        estado = 2;
                    }

                    break;
                }
            }

            yield return new WaitForSeconds(.1f);
        }
    }
    
    
    private void Enfadarse()
    {
        CambiarSprite(varId + 6);
        angryBool = true;
    }

    private void CambiarSprite(int i)
    {
        client.sprite = variacionesClientes[i];
    }

    private void NuevoPedido()
    {
        pedido.gameObject.SetActive(true);
        rectanguloTimer.gameObject.SetActive(true);
        var localScale = rectanguloTimer.transform.localScale;
        localScale = new Vector3(
            2f,
            localScale.y,
            localScale.z);
        rectanguloTimer.transform.localScale = localScale;

        foreach (Transform child in pedido.transform)
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

    private void Irse(bool a) {
        pedido.gameObject.SetActive(false);
        rectanguloTimer.gameObject.SetActive(false);
        if (a)
        {
            //llamar a observers y quitar estrellas
        }
        else
        {
            //comprobar ingredientes
        }
        estado = 1;
    }

}

