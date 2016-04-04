using System;
using System.Collections.Generic;
using System.Threading;
using Engine.EventHandlers;

namespace Engine
{
    class Program
    {
        static void Main(string[] args)
        {
            var eventListener = new EventListener();

            var eventHandlers = new List<IEventHandler>
            {
                new BlogEventHandler(),
                new UserEventHandler()
            };
            eventHandlers.ForEach(eh => eventListener.SubscribeEventHandler(eh));

            eventListener.SetupConnection();

            // Store wait handle inside the try-scope as it gets modified outside the scope.
            // Not copying this will result in weird behavior in the application.
            var handle = new ManualResetEvent(false);

            // When the user presses Ctrl+C the application should close
            // To do this, the Ctrl+C handler is implemented here and sets a wait handle to enable the application to close.
            Console.CancelKeyPress += (s, e) =>
            {
                handle.Set();
            };

            handle.WaitOne();

            eventHandlers.ForEach(eh => eventListener.UnsubscribeEventHandler(eh));
        }
    }
}
