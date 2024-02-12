using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class GameComponent : MonoBehaviour
{
    [SerializeField] private ventanaDialogo v;

    [SerializeField] private Camera camJugador;
    [SerializeField] private Camera camMinijuego;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject dialogBox;
    public static bool canPause;

    void Start()
    {
        Time.timeScale = 0;
        optionsMenu.SetActive(false);
        camJugador.enabled = true;
        camMinijuego.enabled = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        v.LoadDialogue(new Dialog(new string[]
        {
            "Buenas, chaval.",
            "Como es tu primer d�a de trabajo te voy a explicar c�mo van las cosas por aqu�.",
            "Tu trabajo es preparar los men�s que te pidan los clientes. Hamburguesa, complemento y bebida. �Sencillito, no?",
            "Aunque deber�a avisarte, debido a alg�n problemita t�cnico, solo tenemos un �nico bot�n para las tres m�quinas.",
            "�Ves esos paneles delante de la cinta? Solo tienes que acercarte a ellos y conectar el bot�n. Una vez lo hagas, la comida caer� sola.",
            "�Parece magia, pero solo son productos recalentados!",
            "Cada vez que vayas a conectarlo tendr�s que fijarte en los colores del elemento del men� que quieras y enchufar los cables correspondientes.",
            "Y si te preoucupa que puedas electrocutarte, �No te preocupes! El �ltimo empleado lo sobrevivi�...",
            "Con lo que s� que deber�as tener cuidado es con la cinta, �si la comida no cae en la bandeja, la destruir� inmediatamente!",
            " Y si dos bandejas se chocan en la cinta, les ocurrir� lo mismo",
            "... Ah, s�, tambi�n deber�a decirte que no metas ah� la mano. Es un jaleo de limpiar...",
            "...",
            "Bueno, siguiendo la formaci�n; Lo importante del trabajo es ofrecer un buen servicio y satisfacer a la clientela...",
            "... para que vuelvan y los ingresos suban.",
            "Estar� vigilando las puntuaciones online del restaurante, y m�s te vale que t� tambi�n lo hagas... Porque como las estrellas lleguen a cero...",
            "...EST�S ACABADO!",
            "Te dar� un consejo, a la gente no les gusta que les des un pedido equivocado. Pero lo que les gusta incluso menos, es que les hagas esperar.",
            ""
        }, new bool[] {false}, new Image[]
        {
            null
        }, new Action[]
        {
            null, null, null, null, null, null,null, null,null, null, null, null, null, null, null, null, null, StartGameAfterIntro
        })); ;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            v.NextTip();
        }

        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale > 0.9f)
            {
                Pause();
            }
            else
            {
                UnPause();
            }
        }
    }

    void Pause(){
        if (canPause) {
            if (camJugador.enabled)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
            optionsMenu.SetActive(true);
            optionsMenu.GetComponentInChildren<MenuOpciones>().MoveMenuUp();
        }
    }
    void UnPause(){
        if (canPause) {
            if (camJugador.enabled)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            optionsMenu.SetActive(false);
            optionsMenu.GetComponentInChildren<MenuOpciones>().MoveMenuDown();
        }
    }

    void StartGameAfterIntro()
    {
        canPause = true;
        dialogBox.SetActive(false);
        UnPause();
    }

}
