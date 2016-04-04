namespace Engine.EventHandlers
{
    public interface IEventHandler
    {
        void Handle(string key, dynamic data);
    }
}