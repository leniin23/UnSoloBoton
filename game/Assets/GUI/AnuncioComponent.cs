using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class AnuncioComponent : MonoBehaviour
{
    public TextMeshProUGUI texto;
    void Start()
    {
        texto=GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        texto.SetText("GANANCIAS: " + ClientComponent.ganancias + " EUROS");
    }

    public void Salir()
    {
        SceneManager.LoadScene("escenaAntonio", LoadSceneMode.Single);
    }
}
