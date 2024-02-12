using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using spawn;
using UnityEngine;

public class BandejaInfo : MonoBehaviour
{
    private spawnController s;

    private static ParticleSystem smoke;
    private bool pathStep;
    public GameObject final;
    const int speed = 1;
    public int hamburguesa { get; private set; } = 0;
    public int bebida{ get; private set; }= 0;
    public int complemtentos{ get; private set; }= 0;
    
    // Start is called before the first frame update
    void Start()
    {
        if (smoke == null) smoke = FindObjectOfType<ParticleSystem>();
        s = GameObject.FindWithTag("spawner").GetComponent<spawnController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pathStep)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed * Time.deltaTime);
        }
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
            var pos = (transform.position + other.transform.position) * 0.5f;
            var smokeCopy = Instantiate(smoke, pos, Quaternion.identity);
            smokeCopy.Play();
            SFXManager.instance.audioSource.PlayOneShot(Resources.Load<AudioClip>("SFX/fireball"));
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("bandejaPath"))
        {
            //Debug.LogWarning("fofofoofofof");
            if (!pathStep)
            {
                transform.Rotate(0.0f, 270, 0.0f, Space.World);
                transform.position = final.transform.position;
                pathStep = true;
            }
            else { pathStep = false;
                transform.gameObject.AddComponent(typeof(Bandeja));
            }
        }
    }
}
