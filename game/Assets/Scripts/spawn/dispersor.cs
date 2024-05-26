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
        var position1 = transform.position;
        position = new Vector3(position1.x, position1.y -0.5f, position1.z);
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

<<<<<<< Updated upstream
    public void setMoveTrue()
=======
    public void SetMoveTrue(int ingredient)
>>>>>>> Stashed changes
    {
        move = true;
        objeto = Instantiate(objetoACrear, position , transform.rotation);
    }
    
}
