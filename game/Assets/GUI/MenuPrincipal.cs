using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    GameObject optionsMenu;
    // Start is called before the first frame update
    void Start()
    {
        
        optionsMenu = transform.Find("Menu Opciones").gameObject;
        optionsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Salir()
    {
        Debug.Log("salir");
        Application.Quit();
    }

    public void Opciones()
    {
        optionsMenu.SetActive(true);
        optionsMenu.GetComponentInChildren<MenuOpciones>().MoveMenuUp();
    }

    public void Jugar()
    {
        SceneManager.LoadScene("Diego", LoadSceneMode.Single);
    }
}
