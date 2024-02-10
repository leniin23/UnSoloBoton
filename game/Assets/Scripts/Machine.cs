using System;
using Minigame;
using UnityEngine;

namespace DefaultNamespace
{
    public class Machine : MonoBehaviour, IPickable
    {
        private static Camera _gameCamera;
        private static Camera _playerCamera;
        [SerializeField] private int type;
        [SerializeField] private dispersor spawner;
        private PlayerController player;

        private void Start()
        {
            if (_playerCamera == null) _playerCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            if (_gameCamera == null) _gameCamera = GameObject.Find("Main Camera 2").GetComponent<Camera>();
        }

        public void PickUp(Transform father, IPickable other)
        {
            //Lock fathers movement
            player = father.GetComponent<PlayerController>();
            if(player == null) return;
            player.SetPause(true);
            //Open minigame interface
            MinigameManager.instance.StartGame(this);
            //Change camera
            //Camera.SetupCurrent(_gameCamera);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            _playerCamera.enabled = !_playerCamera.enabled;
            _gameCamera.enabled = !_gameCamera.enabled;
        }

        public void ServeIngredient(int ingredient)
        {
            //Hacer que spawnee el ingrediente correcto por favor lenin dime como se hace por favor 
            //(El spawner se llama "spawner")
            spawner.setMoveTrue(ingredient);
            //Liberar el movimiento
            LetGo();
        }
        public void LetGo()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _playerCamera.enabled = !_playerCamera.enabled;
            _gameCamera.enabled = !_gameCamera.enabled;
            player.SetPause(false);
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}