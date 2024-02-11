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
            "Pufff, ya son las 23:30 qué ganas de echarme unas partiditas al solitario.",
            "...",
            "NI",
            "¡Oh no! ¡Se me ha vuelto a olvidar este año corregir los trabajos! Y las actas cierran esta noche. ¡Tengo que darme prisa!",
            "Vale... ¿Cómo se hacía esto de corregir?",
            "Vale, si no recuerdo mal, las presentaciones de mis alumnos irán apareciendo en la pantalla de mi precioso ordenador.",
            "Y con mi súper-teclado custom de última generación puedo pasar entre las diapositivas",
            "No necesito más teclas, porque en cuanto corrija una presentación, se pasará directamente a la siguiente",
            "Ahora solo tengo que encontrar la guía...",
            "...",
            "Estas guías deberían tener los criterios de corrección de la primera parte del curso.",
            "Lleva tanto tiempo en el cajón que es difícil separar las hojas. De no ser porque tiene una esquina levantada no podría pasar las hojas, aunque eso significa que solo puedo pasar las hojas hacia delante.",
            "Según avancen los trabajos, tendré que atenerme a más reglas, pero ya iré sacando esas guías cuando las necesite.",
            "Y para finalizar, el botón para corregir estaba…",
            "Ah sí, aquí arriba. SI FALTA AUNQUE SEA UNO DE LOS REQUISITOS LA PRÁCTICA ESTARÁ SUPER-SUSPENSA,\n... y si está perfecta, aprobada",
            ""
        }, new bool[] {false}, new Image[]
        {
            null
        }, new Action[]
        {
            null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, StartGameAfterIntro
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
