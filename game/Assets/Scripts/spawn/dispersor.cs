using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dispersor : MonoBehaviour
{
    public GameObject objetoACrear;
    private GameObject objeto;
    public float timeMoving;
    private bool move;
    private float timer;
    private Vector3 position;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        position = new Vector3(transform.position.x, transform.position.y -0.5f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

        if (move && timer<timeMoving && objeto)
        {
            timer += Time.deltaTime;
            objeto.transform.Translate(speed * Time.deltaTime * Vector3.down);
        }
        else
        {
            timer = 0;
            move = false;
        }
    }

    public void setMoveTrue()
    {
        move = true;
        objeto = Instantiate(objetoACrear, position , transform.rotation);
    }
    
}
