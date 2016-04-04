using System.Collections.Generic;
using System.Text;
using Engine.EventHandlers;
using Newtonsoft.Json;

namespace Engine
{
    /// <summary>
    ///     Listener class for registering and listening
    ///     for events.s
    /// </summary>
    public class EventListener
    {
        private readonly List<IEventHandler> _eventHandlers;

        public EventListener()
        {
            _eventHandlers = new List<IEventHandler>();
        }

        /// <summary>
        ///     Get the subscribed eventhandlers
        /// </summary>
        public IEnumerable<IEventHandler> EventHandlers
        {
            get
            {
                lock (_eventHandlers)
                {
                    return _eventHandlers;
                }
            }
        }

        /// <summary>
        ///     Subscribe an eventhandler to start receiving messages
        /// </summary>
        /// <param name="eventHandler"></param>
        public void SubscribeEventHandler(IEventHandler eventHandler)
        {
            lock (_eventHandlers)
            {
                _eventHandlers.Add(eventHandler);
            }
        }

        /// <summary>
        ///     Unsubscribe an eventhandler
        /// </summary>
        /// <param name="eventHandler"></param>
        public void UnsubscribeEventHandler(IEventHandler eventHandler)
        {
            lock (_eventHandlers)
            {
                if (!_eventHandlers.Contains(eventHandler))
                {
                    return;
                }
                _eventHandlers.Remove(eventHandler);
            }
        }

        /// <summary>
        ///     This function would normally connect to an eventbus and listen for messages
        /// </summary>
        public void SetupConnection()
        {
        }

        public void HandleMessage(byte[] data)
        {
            var message = JsonConvert.DeserializeObject<dynamic>(Encoding.UTF8.GetString(data));

            var key = (string) message.key;
            var content = message.content;

            lock (_eventHandlers)
            {
                foreach (var eventHandler in _eventHandlers)
                {
                    eventHandler.Handle(key, content);
                }
            }
        }
    }
}