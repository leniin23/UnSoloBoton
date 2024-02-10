using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandejaInfo : MonoBehaviour
{
    public int hamburguesa { get; private set; } = 0;
    public int bebida{ get; private set; }= 0;
    public int complemtentos{ get; private set; }= 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setHamburguesa(int i = 0)
    {
        hamburguesa = i;
    }
    public void setBebida(int i = 0)
    {
        bebida = i;
    }
    public void setComplementos(int i = 0)
    {
        complemtentos = i;
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
