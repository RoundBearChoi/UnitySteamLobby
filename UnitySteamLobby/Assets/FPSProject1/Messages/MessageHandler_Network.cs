using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class MessageHandler_Network : MessageHandler
    {
        private IGameInitializer _initializer = null;

        public static MessageHandler handler = null;
        public static List<IListener> listeners = null;

        public MessageHandler_Network(IGameInitializer initializer)
        {
            _initializer = initializer;
            _handlerName = "message handler - network";
            listeners = new List<IListener>();

            handler = this;
        }

        public override void HandleMessages()
        {
            _listMessages.Clear();

            foreach(Message m in _queue)
            {
                _listMessages.Add(m);
            }

            _queue.Clear();

            foreach (Message m in _listMessages)
            {
                for (int i = 0; i < listeners.Count; i++)
                {
                    if (listeners[i].REMOVE)
                    {
                        listeners.RemoveAt(i);
                    }
                    else
                    {
                        listeners[i].OnNotify(m);
                    }
                }

                //foreach(IListener listener in listeners)
                //{
                //    listener.OnNotify(m);
                //}
            }

            _listMessages.Clear();
        }
    }
}