using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    
    [SerializeField] private Transform cam;   // Objeto que contiene rotacion posicion y escala
    public float distRayo = 3;  // Tope de distancia a la que puedes agarrar objetos
    public float distObj;  // Distancia a la que se va a situar el objeto de la c mara al agarrarlo
    public bool firstHit;  // Vale true mientras mantenemos un objeto vez que pulsamos sobre el objeto
    public Transform objAgarrado;
    public IPickable grabedBody;
    public GameObject rayCastedObj;
    [SerializeField] private GameObject manoInteract; 
    void Start()
    {
        //cam = this.transform;     // cam es una referencia a rotaci n posici n y escala de la camara 
        firstHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Si el objeto esta agarrado y se pulsa la E, se suelta el objeto y no se lanzan rayos
        if (firstHit && Input.GetKeyDown(KeyCode.Mouse0))
        {
            LetGo();
            return;
        }
        
        
        // El rayo se ve en la escena, mientras se ejecuta, pero no se ve en la partida
        Debug.DrawRay(cam.position, cam.forward*distRayo, Color.red); 
        // cam.fordward: direcci n eje azul
        // cam.position: posicion camara

        RaycastHit hit; // Contiene informaci n de la colisi n del rayo con un objeto
        
        // Si el rayo colisiona con un objeto...
        if (Physics.Raycast(cam.position + cam.forward*0.2f, cam.forward, out hit, distRayo)) {
            rayCastedObj = hit.transform.gameObject;  
            if (rayCastedObj.tag == "Hamburguesa")
            {
                manoInteract.SetActive(true);
            }
            else
            {
                manoInteract.SetActive(false);
            }

            // Si se pulsa la tecla e sobre el objeto...
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log("Rayo colisiona con " + hit.transform.name);
                grabedBody = hit.transform.GetComponent<IPickable>();
                if(grabedBody == null)  return;
                Debug.Log("Objeto encontrado");
                manoInteract.SetActive(false);
                if (!firstHit) {    // Si el objeto no estaba agarrado ---> Agarrar el objeto
                    grabedBody.PickUp(transform);
                    distObj = Vector3.Distance(hit.transform.position, cam.position);   // La primera vez nos quedamos con la distancia del momento de agarrarlo
                    firstHit = true;
                    objAgarrado = hit.transform;       // Me quedo la referencia al objeto agarrado
                }
            }
        }
        
    }


    private void LetGo()
    {
        firstHit = !firstHit;
        grabedBody.LetGo();
        objAgarrado = null;
    }
}
