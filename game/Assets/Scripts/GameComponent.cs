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
    bool canPause;

    void Start()
    {
        Time.timeScale = 0;
        optionsMenu.SetActive(false);
        camJugador.enabled = true;
        camMinijuego.enabled = false;
        Cursor.visible = true;//false
        Cursor.lockState = CursorLockMode.None;//Locked;
        v.LoadDialogue(new Dialog(new string[]
        {
            "Hola, chaval",
            "...",
            "NI",
            "�Oh no! ¡Se me ha vuelto a olvidar este a�o corregir los trabajos! Y las actas cierran esta noche. �Tengo que darme prisa!",
            "Vale... ¡Cómo se hac�a esto de corregir?",
            "Vale, si no recuerdo mal, las presentaciones de mis alumnos ir�n apareciendo en la pantalla de mi precioso ordenador.",
            "Y con mi súper-teclado custom de �ltima generaci�n puedo pasar entre las diapositivas",
            "No necesito m�s teclas, porque en cuanto corrija una presentaci�n, se pasar� directamente a la siguiente",
            "Ahora solo tengo que encontrar la gu�a...",
            "...",
            "Estas gu�as deber�an tener los criterios de correcci�n de la primera parte del curso.",
            "Lleva tanto tiempo en el caj�n que es dif�cil separar las hojas. De no ser porque tiene una esquina levantada no podr�a pasar las hojas, aunque eso significa que solo puedo pasar las hojas hacia delante.",
            "Seg�n avancen los trabajos, tendr� que atenerme a m�s reglas, pero ya ir� sacando esas gu�as cuando las necesite.",
            "Y para finalizar, el bot�n para corregir estaba�",
            "Ah s�, aqu� arriba. SI FALTA AUNQUE SEA UNO DE LOS REQUISITOS LA PR�CTICA ESTAR� SUPER-SUSPENSA,\n... y si est� perfecta, aprobada",
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
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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
    void UnPause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (canPause) {
            if (camJugador.enabled)
            {
                //Cursor.visible = false;
                //Cursor.lockState = CursorLockMode.Locked;
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
