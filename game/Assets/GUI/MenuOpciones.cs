using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuOpciones : MonoBehaviour
{
    // Start is called before the first frame update
    static GameObject exitButton;
    static Slider volumeSlider;
    static Toggle fullscreenToggle;
    static bool fullscreen;
    static float volumen = 1;
    //const float upPosition = 0;
    //float downPosition = Screen.height*-0.5f;
    void Start()
    {
        exitButton=transform.Find("Salir").gameObject;
        volumeSlider = transform.Find("Slider").gameObject.GetComponent<Slider>();
        fullscreenToggle = transform.Find("Toggle").gameObject.GetComponent<Toggle>();

        AudioListener.volume = volumen;
        volumeSlider.value = volumen;

        fullscreen = Screen.fullScreen;
        fullscreenToggle.isOn = fullscreen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveMenuUp()
    {
        Time.timeScale = 0;
        if (SceneManager.GetActiveScene().name!="escenaAntonio")
        {
            exitButton.SetActive(true);
        }

    }

    public void MoveMenuDown()
    {
        if (SceneManager.GetActiveScene().name != "escenaAntonio")
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        Time.timeScale = 1;
        exitButton.SetActive(false);
        transform.parent.gameObject.SetActive(false);
    }

    public void ChangeVolumeValue()
    {
        volumen = volumeSlider.value;
        AudioListener.volume = volumen;
        Debug.Log(volumen);
    }

    public void ToggleFullscreen()
    {
        fullscreen = !fullscreen;   
        Screen.fullScreen = fullscreen;
    }

    public void Salir()
    {
        SceneManager.LoadScene("escenaAntonio", LoadSceneMode.Single);
    }
}
