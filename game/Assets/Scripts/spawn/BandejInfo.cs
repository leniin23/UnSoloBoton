using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandejaInfo : MonoBehaviour
{
    private bool hamburguesa;
    private bool bebida;
    private bool complemtentos;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setHamburguesa()
    {
        hamburguesa = true;
    }
    public void setBebida()
    {
        bebida = true;
    }
    public void setComplementos()
    {
        complemtentos = true;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("bandeja"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
