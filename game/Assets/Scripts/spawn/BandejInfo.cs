using System.Collections;
using System.Collections.Generic;
using spawn;
using UnityEngine;

public class BandejaInfo : MonoBehaviour
{
    private spawnController s;
    public int hamburguesa { get; private set; } = 0;
    public int bebida{ get; private set; }= 0;
    public int complemtentos{ get; private set; }= 0;
    
    // Start is called before the first frame update
    void Start()
    {
        s = GameObject.FindWithTag("spawner").GetComponent<spawnController>();
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bandeja"))
        {
            s.reset();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
