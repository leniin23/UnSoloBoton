using UnityEngine;

namespace DefaultNamespace
{
    public interface IPickable
    {
        public void PickUp(Transform father);
        public void LetGo();
    }
}