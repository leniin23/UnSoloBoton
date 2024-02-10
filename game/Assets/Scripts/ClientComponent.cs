using System.Collections;
using System.Collections.Generic;
using System;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class ClientComponent : MonoBehaviour, IPickable
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
        rectanguloTimer = transform.GetChild(1).GetChild(0).gameObject;
        varId = _random.Next(6);
        //Debug.LogWarning(varId);
        CambiarSprite(varId);
        NuevoPedido();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            NuevoPedido();
        }
        transform.LookAt(transform.position + targetCamera.transform.rotation * Vector3.left);
        transform.Rotate(vec, Space.World);


        timePercent = tiempoLimite / maxTime;
        rectanguloTimer.transform.localScale = new Vector3(
            timePercent * 2,
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
                if (transform.position.y <= -1f) { Debug.Log("joder macho"); tiempoCooldown = maxCooldown; estado = 3; }
            }
            else if (estado == 2) //subir
            {
                transform.position = new Vector3(
                transform.position.x,
                transform.position.y + speed * Time.deltaTime,
                transform.position.z);
                if (transform.position.y >= 3f) { estado = 0; }
            }
            else if (estado == 3) //cooldown
            {
                tiempoCooldown -= Time.deltaTime;
                if (tiempoCooldown <= 0)
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
        rectanguloTimer.transform.localScale = new Vector3(
            2f,
            rectanguloTimer.transform.localScale.y,
            rectanguloTimer.transform.localScale.z);

        foreach (Transform child in pedido.transform)
        {
            child.gameObject.SetActive(false);
        }
        hamburguesa = _random.Next(0, 3);
        complemento = _random.Next(3, 6);
        bebida = _random.Next(6, 9);
        pedido.transform.GetChild(hamburguesa).gameObject.SetActive(true);
        pedido.transform.GetChild(complemento).gameObject.SetActive(true);
        pedido.transform.GetChild(bebida).gameObject.SetActive(true);

        Debug.LogWarning(hamburguesa + " - " + complemento + " - " + bebida);
        tiempoLimite = maxTime;
    }

    private void Irse(bool a)
    {
        pedido.gameObject.SetActive(false);
        rectanguloTimer.gameObject.SetActive(false);
        if (a)
        {
            //Oh baby all bien all correcto
        }
        else
        {
            //llamar a observers y quitar estrellas
        }
        estado = 1;
    }

    public void PickUp(Transform father, IPickable itemInHand)
    {
        Debug.Log("hOLIS 1");
        if(itemInHand == null)  return;
        var info = itemInHand.GetTransform().GetComponent<BandejaInfo>();
        if(info == null) return;
        Debug.Log("hOLIS 2");
        var isSameInfo = info.hamburguesa == hamburguesa && info.complemtentos == complemento && info.bebida == bebida;
        
        RayCast.instance.LetGo();
        itemInHand.GetTransform().SetParent(transform);
        itemInHand.PickUp(transform);
        /*var body = itemInHand.GetTransform().GetComponent<Rigidbody>();
        body.isKinematic = true;
        itemInHand.GetTransform().position = (transform1.position + transform1.forward*0.5f - transform1.up*0.8f + transform1.right*0.05f);*/
        var transform1 = transform;
        itemInHand.GetTransform().position = (transform1.position + transform1.forward*0.5f - transform1.up*0.8f + transform1.right*0.05f);
        Irse(isSameInfo);
    }

    public void LetGo()
    {
        throw new NotImplementedException();
    }
    public Transform GetTransform()
    {
        return transform;
    }
}

