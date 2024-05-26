using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Bandeja : MonoBehaviour, IPickable
    {
        private Rigidbody body;
<<<<<<< Updated upstream
        private Collider collider;
=======
        private Collider coll;
        private bool canBeTaken;
        private Transform selfTransform;
>>>>>>> Stashed changes
        public void PickUp(Transform father, IPickable other)
        {
            RayCast.instance.LetGo();
            RayCast.instance.PickUp(this);
<<<<<<< Updated upstream
            transform.SetParent(father);
            transform.localPosition = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.position += father.forward + father.right + father.up*0.35f;
=======
            selfTransform.SetParent(father);
            selfTransform.localPosition = Vector3.zero;
            selfTransform.rotation = Quaternion.Euler(0, 90, 0);
            selfTransform.position += father.forward*0.75f - father.right*0.7f + father.up*0.35f;
>>>>>>> Stashed changes
            //collider = transform.GetComponent<Collider>();
            //collider.isTrigger = true;
            body.isKinematic = true;
        }

        private void Start()
        {
<<<<<<< Updated upstream
=======
            selfTransform = transform;
            canBeTaken = true;
            tag = "Interactable";
>>>>>>> Stashed changes
            body = transform.GetComponent<Rigidbody>();
            coll = transform.GetComponent<Collider>();
            if (body == null)
            {
                body = gameObject.AddComponent<Rigidbody>();
            }

<<<<<<< Updated upstream
=======
            body.isKinematic = false;
            body.useGravity = true;
            coll.isTrigger = false;

>>>>>>> Stashed changes
        }

        public void LetGo()
        {
<<<<<<< Updated upstream
            transform.rotation = Quaternion.identity;
            transform.position = transform.parent.position + transform.parent.forward;
            transform.parent = null;
=======
            //Debug.Log("a");
            if(!this) return;
            //transform.rotation = Quaternion.identity;
            var parent = selfTransform.parent;
            selfTransform.position = transform.parent.position + parent.forward + parent.up*0.35f;
            selfTransform.parent = null;
>>>>>>> Stashed changes
            body.isKinematic = false;
            //collider.isTrigger = false;
        }
    }
}