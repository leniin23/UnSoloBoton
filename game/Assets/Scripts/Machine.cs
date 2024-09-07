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
        private Vector3 playerCameraPosition;

        //esto se podría hacer más optimo lo sé xd
        private static GameObject hTERNERA;
        private static GameObject hPOLLO;
        private static GameObject hVEGANO;
        private static GameObject cPATATAS;
        private static GameObject cDELUXE;
        private static GameObject cNUGGETS;
        private static GameObject bCOLACOCA;
        private static GameObject bNAFTA;
        private static GameObject bSPRINT;

        private void Start()
        {
            if (_playerCamera == null) _playerCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            if (_gameCamera == null) _gameCamera = GameObject.Find("Main Camera 2").GetComponent<Camera>();

            hTERNERA   = GameObject.Find("hTERNERA");
            hPOLLO     = GameObject.Find("hPOLLO");
            hVEGANO    = GameObject.Find("hVEGANO");
            cPATATAS   = GameObject.Find("cPATATAS");
            cDELUXE    = GameObject.Find("cDELUXE");
            cNUGGETS   = GameObject.Find("cNUGGETS");
            bCOLACOCA  = GameObject.Find("bCOLACOCA");
            bNAFTA     = GameObject.Find("bNAFTA");
            bSPRINT = GameObject.Find("bSPRINT");
        }

        public void PickUp(Transform father, IPickable other)
        {
            //Lock fathers movement
            player = father.parent.GetComponent<PlayerController>();
            if(player == null) return;
            player.SetPause(true);
            // RayCast.instance.LetGo();
            //Time.timeScale = 1f;
            //Open minigame interface
            MinigameManager.instance.StartGame(this);
            //Change camera
            //Camera.SetupCurrent(_gameCamera);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;

            // _playerCamera.enabled = !_playerCamera.enabled;
            // _gameCamera.enabled = !_gameCamera.enabled;
            
            var transform1 = _playerCamera.transform;
            var transform2 = _gameCamera.transform;
            playerCameraPosition = transform1.position;
            transform1.position = transform2.position;
            _playerCamera.orthographic = true;
            _playerCamera.orthographicSize = 5f;
            _playerCamera.farClipPlane = 20;
            _playerCamera.backgroundColor = new Color(1,1,1);
            transform1.rotation = transform2.rotation;
            SpriteType();
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
            // _playerCamera.enabled = !_playerCamera.enabled;
            // _gameCamera.enabled = !_gameCamera.enabled;
            _playerCamera.orthographic = false;
            _playerCamera.farClipPlane = 1000;
            _playerCamera.transform.position = playerCameraPosition;
            player.SetPause(false);
        }

        public Transform GetTransform()
        {
            return transform;
        }

        void SpriteType()
        {
            hTERNERA  .SetActive(type == 0);
            hPOLLO    .SetActive(type == 0);
            hVEGANO   .SetActive(type == 0);
            cPATATAS  .SetActive(type == 1);
            cDELUXE   .SetActive(type == 1);
            cNUGGETS  .SetActive(type == 1);
            bCOLACOCA .SetActive(type == 2);
            bNAFTA    .SetActive(type == 2);
            bSPRINT   .SetActive(type == 2);
        }
    }
}