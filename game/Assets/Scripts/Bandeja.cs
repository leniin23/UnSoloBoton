using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Bandeja : MonoBehaviour, IPickable
    {
        private Rigidbody body;
        private Collider selfCollider;
        private bool canBeTaken;
        private Transform selfTransform;
        public void PickUp(Transform father, IPickable other)
        {
            if(!canBeTaken) return;
            canBeTaken = false;
            RayCast.instance.LetGo();
            RayCast.instance.PickUp(this);
            selfTransform.SetParent(father);
            selfTransform.localPosition = Vector3.zero;
            selfTransform.rotation = Quaternion.Euler(0, 90, 0);
            selfTransform.position += father.forward*0.75f - father.right*0.7f + father.up*0f;
            //collider = transform.GetComponent<Collider>();
            //collider.isTrigger = true;
            body.isKinematic = true;
        }

        private void Start()
        {
            canBeTaken = true;
            body = transform.GetComponent<Rigidbody>();
            selfCollider = transform.GetComponent<Collider>();
            if (body == null)
            {
                body = gameObject.AddComponent<Rigidbody>();
            }

            body.isKinematic = false;
            body.useGravity = true;
            selfCollider.isTrigger = false;

            selfTransform = transform;
        }

        public void LetGo()
        {
            //Debug.Log("a");
            if(!this) return;
            //transform.rotation = Quaternion.identity;
            var parent = selfTransform.parent;
            selfTransform.position = transform.parent.position + parent.forward + parent.up*0.35f;
            selfTransform.parent = null;
            body.isKinematic = false;
            canBeTaken = true;
            //collider.isTrigger = false;
        }
        public Transform GetTransform()
        {
            return this.transform;
        }
    }
}