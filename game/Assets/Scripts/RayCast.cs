using System.Collections;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    public static RayCast instance;
    [SerializeField] private Transform cam;   // Objeto que contiene rotacion posicion y escala
    public float distRayo = 3;  // Tope de distancia a la que puedes agarrar objetos
    //public Transform objAgarrado;
    private IPickable grabedBody;
    private IPickable lookAt;
    [SerializeField] private GameObject manoInteract; 
    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(this);
        // StartCoroutine(FakeUpdate());
    }

    // private IEnumerator FakeUpdate()
    // {
    //     // for (;;)
    //     // {
    //     //     Interact();
    //     //     yield return new WaitForSeconds(0.01F);
    //     // }
    // }
    
    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
=======
        Interact();
    }

    private void Interact()
    {
        if(Pausa)   return;
        var isLookingAtSomething = false;
>>>>>>> Stashed changes
        //Si el objeto esta agarrado y se pulsa la E, se suelta el objeto y no se lanzan rayos
        /*if (firstHit && Input.GetKeyDown(KeyCode.Mouse0))
        {
            LetGo();
            return;
        }*/
        
        
        // El rayo se ve en la escena, mientras se ejecuta, pero no se ve en la partida
        Debug.DrawRay(cam.position, cam.forward*distRayo, Color.red); 
        // cam.fordward: direcci n eje azul
        // cam.position: posicion camara

        RaycastHit hit; // Contiene informaci n de la colisi n del rayo con un objeto
        var isMousePressed = Input.GetKeyDown(KeyCode.Mouse0);
        
        // Si el rayo colisiona con un objeto...
        if (Physics.Raycast(cam.position + cam.forward*0.2f, cam.forward, out hit, distRayo))
        {
<<<<<<< Updated upstream
            //Actualizamos a que estamos mirando
            lookAt = hit.transform.GetComponent<IPickable>();

=======
            //Actualizamos a que estamos mirando (obtenemos el IPickable SOLO si la tag coincide)
            if(hit.transform.CompareTag("Interactable"))
            {
                // lookAt = hit.transform.GetComponent<IPickable>();
                isLookingAtSomething = true;
            }
            //Debug.Log("Name: " + lookAt?.GetTransform().name);
>>>>>>> Stashed changes
            // Si se pulsa la tecla e sobre el objeto...
            if (isMousePressed)
            {
                //Si no estamos mirando a nada interesante no tenemos que hacer nada
                if(!isLookingAtSomething)
                {
<<<<<<< Updated upstream
=======
                    // No se si es necesario
                    LetGo(); 
>>>>>>> Stashed changes
                    return;
                }

                lookAt = hit.transform.GetComponent<IPickable>();
                //Interactuamos con el objeto
                lookAt.PickUp(transform, grabedBody);
                //objAgarrado = hit.transform;       // Me quedo la referencia al objeto agarrado
            }
        }
        else
        {
            if(isMousePressed)  LetGo();
            lookAt = null;
        }
        
        manoInteract.SetActive(isLookingAtSomething);

<<<<<<< Updated upstream
=======
    }
    
    public void SetPausa(bool newPause)
    {
        Pausa = newPause;
        lookAt = null;
        manoInteract.SetActive(false);
    }
>>>>>>> Stashed changes
    public void PickUp(IPickable item)
    {
        grabedBody = item;
        lookAt = null;
    }

    public void LetGo()
    {
        grabedBody?.LetGo();
        grabedBody = null;
        //objAgarrado = null;
    }
}
