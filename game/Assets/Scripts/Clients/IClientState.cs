namespace Clients
{
    public interface IClientState
    {
        void OnEnter(object[] argv, int argc);
        
        void OnUpdate(float delta);

        void OnExit();
    }
}