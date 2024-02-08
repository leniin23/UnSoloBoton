using UnityEngine;

namespace DefaultNamespace
{
    public interface IPickable
    {
        public void PickUp(Transform father, IPickable itemInHand = null);
        public void LetGo();
    }
}