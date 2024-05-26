using System;
using DefaultNamespace;
using UnityEngine;

namespace Minigame
{
    public class MinigameManager : MonoBehaviour
    {
        public static MinigameManager instance;
        public bool isActive;

        private void Start()
        {
            if (instance == null) instance = this;
            else
            { 
                Destroy(this);
            }
        }

        public void StartGame(Machine machine)
        {
            MachineManager.instance.ResetColors(machine);
            CableManager.instance.ResetCables();
            isActive = true;
        }
    }
}