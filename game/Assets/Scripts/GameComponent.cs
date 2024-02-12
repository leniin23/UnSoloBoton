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
            "Como es tu primer día de trabajo te voy a explicar cómo van las cosas por aquí.",
            "Tu trabajo es preparar los menús que te pidan los clientes. Hamburguesa, complemento y bebida. ¿Sencillito, no?",
            "Aunque debería avisarte, debido a algún problemita técnico, solo tenemos un único botón para las tres máquinas.",
            "¿Ves esos paneles delante de la cinta? Solo tienes que acercarte a ellos y conectar el botón. Una vez lo hagas, la comida caerá sola.",
            "¡Parece magia, pero solo son productos recalentados!",
            "Cada vez que vayas a conectarlo tendrás que fijarte en los colores del elemento del menú que quieras y enchufar los cables correspondientes.",
            "Y si te preoucupa que puedas electrocutarte, ¡No te preocupes! El último empleado lo sobrevivió...",
            "Con lo que sí que deberías tener cuidado es con la cinta, ¡si la comida no cae en la bandeja, la destruirá inmediatamente!",
            " Y si dos bandejas se chocan en la cinta, les ocurrirá lo mismo",
            "... Ah, sí, también debería decirte que no metas ahí la mano. Es un jaleo de limpiar...",
            "...",
            "Bueno, siguiendo la formación; Lo importante del trabajo es ofrecer un buen servicio y satisfacer a la clientela...",
            "... para que vuelvan y los ingresos suban.",
            "Estaré vigilando las puntuaciones online del restaurante, y más te vale que tú también lo hagas... Porque como las estrellas lleguen a cero...",
            "...ESTÁS ACABADO!",
            "Te daré un consejo, a la gente no les gusta que les des un pedido equivocado. Pero lo que les gusta incluso menos, es que les hagas esperar.",
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
