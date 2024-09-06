using System;
using DefaultNamespace;
using UnityEngine;

namespace Minigame
{
    public class MinigameManager : MonoBehaviour
    {
        public static MinigameManager instance;
        private Transform raycast;
        private Transform machineManager;
        bool isInMinigame { get; set;}
        private Machine machine;

        private void Start()
        {
            if (instance == null) instance = this;
            else
            { 
                Destroy(this);
            }
            RestartGame();

        }

        public void StartGame(Machine inMachine)
        {
            machine = inMachine;
            MachineManager.instance.ResetColors();
            CableManager.instance.ResetCables();
            FindObjectOfType<RaycastCables>().enabled = true;
        }

        private void RestartGame()
        {
            GameObject.FindObjectOfType<RaycastCables>().enabled = false;
            GameObject.FindObjectOfType<RaycastCables>().enabled = true;
            isInMinigame = false;
        }
        
        public void EndGame(int ingredient)
        {
            RestartGame();
            machine.ServeIngredient(ingredient);
        }
    }
}