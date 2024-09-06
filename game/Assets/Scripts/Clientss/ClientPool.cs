using UnityEngine;

namespace DefaultNamespace.Clientss
{
    public class ClientPool: MonoBehaviour
    {
        [SerializeField]
        private Vector3[] spawnPoints;
        
        [SerializeField]
        private Vector3[] middlePoints;

        [SerializeField] private Vector3[] foodTruckSpots;
        private bool[] foodTruckSpotsBools;

        [SerializeField] private float minWaitTime;
        [SerializeField] private float maxWaitTime;
        [SerializeField] private int poolSize;

        private ClientComponent[] clientPool;

        private void Start()
        {
            foodTruckSpotsBools = new bool[foodTruckSpots.Length];
            clientPool = new ClientComponent[poolSize];
        }

        private void SpawnClient()
        {
            
        }
    }
}