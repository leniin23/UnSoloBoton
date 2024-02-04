using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Bandeja : MonoBehaviour, IPickable
    {
        private Rigidbody body;
        private Collider collider;
        public void PickUp(Transform father)
        {
            transform.SetParent(father);
            transform.localPosition = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.position += father.forward + father.right + father.up*0.25f;
            //collider = transform.GetComponent<Collider>();
            //collider.isTrigger = true;
            body.isKinematic = true;
        }

        private void Start()
        {
            body = transform.GetComponent<Rigidbody>();
            collider = transform.GetComponent<Collider>();
            if (body == null)
            {
                body = gameObject.AddComponent<Rigidbody>();
            }

        }

        public void LetGo()
        {
            transform.rotation = Quaternion.identity;
            transform.position = transform.parent.position + transform.parent.forward;
            transform.parent = null;
            body.isKinematic = false;
            //collider.isTrigger = false;
        }
    }
}