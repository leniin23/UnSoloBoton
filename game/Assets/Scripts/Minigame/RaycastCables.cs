using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RaycastCables : MonoBehaviour
{
    public static RaycastCables instance;
    private Camera minigameCamera;
    private Cable cable;
    public float ratio = 0.08f;

    private bool assigned;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }else
        {
            Destroy(this);
        }

        minigameCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    bool GetGameObjectAtPosition(out Transform obTransform)
    {
        var ray = minigameCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out var hit))
        {
            obTransform = hit.collider.transform;
            return true;
        }
        else
        {
            obTransform = null;
            return false;
        }
    }
    
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!GetGameObjectAtPosition(out var obj)) return;
            
            if (obj.transform.parent == null)
            {
                return;
            }

            cable = obj.transform.parent.GetComponent<Cable>();
            if (cable != null)
            {
                cable.Grab();
                assigned = true;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if(cable == null) return;
            cable.Release();
            assigned = false;
            cable = null;
        }
        else if(assigned)
        {
            //var movement = new Vector3(Input.GetAxisRaw("Mouse X")/Screen.currentResolution.width, Input.GetAxisRaw("Mouse Y")/Screen.currentResolution.width);
            var movement = minigameCamera.ScreenToWorldPoint(Input.mousePosition);
            movement.z = -1f;
            cable.Move(movement); 
        }

    }
}
