using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Clients
{
    public class ClientPool: MonoBehaviour
    {
        [SerializeField]
        private Vector3[] spawnPoints;
        private float[] timeSinceLastSpawn;
        
        [SerializeField]
        private Vector3[] middlePoints;
        
        
        [SerializeField]
        private Vector3[] backgroundSpawnPoints;
        
        [SerializeField]
        private Vector3[] backgroundMiddlePoints;

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

        private void Update()
        {
            UpdateLastSpawnTime(Time.deltaTime);
        }

        private void UpdateLastSpawnTime(float deltaTime)
        {
            for (var i = 0; i < timeSinceLastSpawn.Length; i++)
            {
                timeSinceLastSpawn[i] += deltaTime;
            }
        }
        
        private void SpawnClient()
        {
            
        }

        private Vector3[] GenerateRoute(int truckSpot)
        {
            return truckSpot >= 0 ? GetRouteToTruck(truckSpot) : null;
        }


        private Vector3[] GetRouteToTruck(int truckSpot)
        {
            var route = new Vector3[5];
            var selection = 0;
            do
            {
                selection = Random.Range(0, spawnPoints.Length);
            } while (timeSinceLastSpawn[selection] > 2f);

            route[0] = spawnPoints[selection];

            route[1] = middlePoints[Random.Range(0, middlePoints.Length)];
            route[2] = foodTruckSpots[truckSpot];

            route[3] = middlePoints[Random.Range(0, middlePoints.Length)];
            route[4] = spawnPoints[Random.Range(0, spawnPoints.Length)];

            return route;
        }

        private ClientComponent GetClient()
        {
            var aux = clientPool.Where(client => client.estado == 0).ToArray();
            return aux[0];
        }
    }
}