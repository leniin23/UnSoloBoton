using UnityEngine;

namespace Clients
{
    public class ClientStrollState: IClientState
    {
        private Transform clientTransform;
        private ClientComponent clientComponent;

        private int step;
        private readonly float speed = 5f;


        /// <summary>
        /// Follows a given route. Requires the ClientComponent, the Transform, and the route as a Vector3[]
        /// </summary>
        /// <param name="argv">Object array with the required items.</param>
        /// <param name="argc">Number of objects sent</param>
        public void OnEnter(object[] argv, int argc)
        {
            clientComponent = argv[0] as ClientComponent;
            clientTransform = argv[1] as Transform;

            step = 0;
        }

        public void OnUpdate(float delta)
        {
            var position = clientTransform.position;
            if (Vector3.Distance(position, clientComponent.route[step]) <= 0.001f)
            {
                step++;
                if (step == clientComponent.route.Length)
                {
                    clientComponent.ChangeState(new ClientStrollState());
                }
            }
            var newPosition = position + (clientComponent.route[step] - position).normalized * delta * speed;
            clientTransform.position = newPosition;
        }

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }
    }
}