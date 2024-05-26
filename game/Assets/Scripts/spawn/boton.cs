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
<<<<<<< Updated upstream
            dispersor.GetComponent<dispersor>().setMoveTrue();
=======
            dispersor.GetComponent<dispersor>().SetMoveTrue(0);
>>>>>>> Stashed changes
        }
    }
}