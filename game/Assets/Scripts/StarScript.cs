using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarScript : MonoBehaviour
{
    private static Camera scoreCamera;
    public static StarScript instance;
    public static int profit;
    
    // Start is called before the first frame update
    private int life = 10;
    [SerializeField] private float halfStarSize;
    [SerializeField] private Transform starColors;
    private bool salir;
    public GameObject deadScreen;
    void Start()
    {
        profit = 0;
        
        scoreCamera = GameObject.Find("Telecamara").GetComponent<Camera>();
        scoreCamera.Render();
        halfStarSize = starColors.localScale.x*1.019f;
        if (instance == null) instance = this;
        else Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(salir&& Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("escenaAntonio", LoadSceneMode.Single);
        }
    }
    
    public void OnNext(int value)
    {
        // Debug.Log("Value: " + value);
        if (value > 2) value = 1;
        life -= value;
        var size = starColors.localScale;
        var fullStarOffset = life % 2 != 0 ? 0.005 : 0f;
        starColors.position -= starColors.right * (halfStarSize * value);
        scoreCamera.Render();
        /*if (life >= 0)
        {
            starColors.position += Vector3.back;
        }
        else
        {
            size.x = (fullSize / life) * 10;
            starColors.localScale = size;
            //starColors.bounds.size = size;
        }*/
    }

    public void Dead()
    {
        if (life <= 0)
        {
            salir = true;
            GameComponent.canPause = false;
            deadScreen.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            //mostrar mensaje en canvas
        }
    }
}
