using System;
using UnityEngine;

namespace DefaultNamespace.spawn
{
    public class boton: MonoBehaviour
    {
        private GameObject dispersor;

        private void Start()
        {
            dispersor = transform.parent.gameObject;
        }

        void OnMouseDown()
        {
            Debug.Log("click");
            dispersor.GetComponent<dispersor>().setMoveTrue(0);
        }
    }
}