using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class spawnController : MonoBehaviour
{
    public float speed;                 //velocidad de movimiento
    public float timeMoving;            //tiempo que se mueve en cinta
    public float delay;                 //cada cuanto se mueve por lo menos
    public GameObject objectToSpawn;    //va a ser prefab, objeto a spwanear
    
    private Vector3 initialPos;        
    private float timer;
    private float timer1;
    private  GameObject bandeja ;
    private bool isMoving;
    void Start()
    {
<<<<<<< Updated upstream
        initialPos = new Vector3(transform.position.x-1f,transform.position.y-0.4f ,transform.position.z);
        timer = 0;
        timer1 = 0;
=======
        public float speed; //velocidad de movimiento
        public float timeMoving; //tiempo que se mueve en cinta
        public float delay; //cada cuanto se mueve por lo menos
        public GameObject objectToSpawn; //va a ser prefab, objeto a spwanear

        private Vector3 initialPos;
        private float timer;
        private float timer1;
        private GameObject bandeja;
        private bool isMoving;

        void Start()
        {
            initialPos = new Vector3(transform.position.x - 1f, transform.position.y - 0.4f, transform.position.z);
            timer = 0;
            timer1 = 0;
        }


        void Update()
        {
            timer += Time.deltaTime;
            if (timer > delay)
            {
                bandeja = spawn();
                isMoving = true;
                timer = 0;
                //if (!Physics.Raycast(initialPos, Vector3.left, 3))
                //{
                //    bandeja = spawn();
                //    timer = 0;
                //    isMoving = true;
                //}

            }

         
            if (isMoving && bandeja)
            {
                timer1 += Time.deltaTime;
                bandeja.transform.Translate(Time.deltaTime * speed * Vector3.left);
                if (timer1 > timeMoving)
                {
                    isMoving = false;
                    timer1 = 0;
                }

            }
        }

        GameObject spawn()
        {
            SFXManager.instance.audioSource.PlayOneShot(Resources.Load<AudioClip>("SFX/conveyo1"));
            print("sacando obj");
            return Instantiate(objectToSpawn, initialPos, transform.rotation);
        }

        public float getSpeed()
        {
            return speed;
        }

        public float getTime()
        {
            return timeMoving;
        }

        public void reset()
        {
            isMoving = false;
            timer1 = 0;
        }

>>>>>>> Stashed changes
    }

   
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > delay )
        {
           
            if(!Physics.Raycast(initialPos,Vector3.left,3))
            {
                bandeja =spawn();
                timer = 0;
                isMoving = true;
            }
            
        }
        

        if (isMoving)
        {
            timer1 += Time.deltaTime;
            bandeja.transform.Translate(Time.deltaTime * speed * Vector3.left);
            if (timer1 > timeMoving)
            {
                isMoving = false;
                timer1 = 0;
            }

        }
    }

    GameObject spawn()
    {
        return Instantiate(objectToSpawn,initialPos,transform.rotation);
    }

    public float getSpeed()
    {
        return speed;
    }
    public float getTime()
    {
        return timeMoving;
    }
    
}
