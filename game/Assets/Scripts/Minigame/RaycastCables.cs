using System.Collections;
using System.Collections.Generic;
using Minigame;
using Unity.VisualScripting;
using UnityEngine;

public class RaycastCables : MonoBehaviour
{
    public static RaycastCables instance;
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
    }

    private static GameObject GetGameObjectAtPosition()
    {
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit) ?
            hit.collider.gameObject : null;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(!MinigameManager.instance.isActive)   return;
        
        if (Input.GetMouseButtonDown(0))
        {
            var obj = GetGameObjectAtPosition();
            if(obj == null) return;
            if(obj.transform.parent == null)    return;
            cable = obj.transform.parent.GetComponent<Cable>();
            if(cable != null)
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
        } else
        {
            if(assigned)
            {
                //var movement = new Vector3(Input.GetAxisRaw("Mouse X")/Screen.currentResolution.width, Input.GetAxisRaw("Mouse Y")/Screen.currentResolution.width);
                var movement = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
                movement.z = -1f;
                cable.Move(movement);
            }
                //cable.Move(new Vector3(Input.mousePosition.x, Input.mousePosition.y,0));
        }
    }
}
