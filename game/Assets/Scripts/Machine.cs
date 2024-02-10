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
            player = father.GetComponent<PlayerController>();
            if(player == null) return;
            player.SetPause(true);
            //Time.timeScale = 1f;
            //Open minigame interface
            MinigameManager.instance.StartGame(this);
            //Change camera
            //Camera.SetupCurrent(_gameCamera);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            _playerCamera.enabled = !_playerCamera.enabled;
            _gameCamera.enabled = !_gameCamera.enabled;
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
            _playerCamera.enabled = !_playerCamera.enabled;
            _gameCamera.enabled = !_gameCamera.enabled;
            player.SetPause(false);
        }

        public Transform GetTransform()
        {
            return transform;
        }

        void SpriteType()
        {
            if (type==0)
            {
                hTERNERA  .SetActive(true);
                hPOLLO    .SetActive(true);
                hVEGANO   .SetActive(true);
                cPATATAS  .SetActive(false);
                cDELUXE   .SetActive(false);
                cNUGGETS  .SetActive(false);
                bCOLACOCA .SetActive(false);
                bNAFTA    .SetActive(false);
                bSPRINT   .SetActive(false);
            }else if (type == 1)
            {
                hTERNERA.SetActive(false);
                hPOLLO.SetActive(false);
                hVEGANO.SetActive(false);
                cPATATAS.SetActive(true);
                cDELUXE.SetActive(true);
                cNUGGETS.SetActive(true);
                bCOLACOCA.SetActive(false);
                bNAFTA.SetActive(false);
                bSPRINT.SetActive(false);
            } else if(type == 2)
            {
                hTERNERA.SetActive(false);
                hPOLLO.SetActive(false);
                hVEGANO.SetActive(false);
                cPATATAS.SetActive(false);
                cDELUXE.SetActive(false);
                cNUGGETS.SetActive(false);
                bCOLACOCA.SetActive(true);
                bNAFTA.SetActive(true);
                bSPRINT.SetActive(true);
            }
        }
    }
}