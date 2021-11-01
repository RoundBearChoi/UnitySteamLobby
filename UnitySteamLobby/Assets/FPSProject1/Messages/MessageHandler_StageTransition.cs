using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class MessageHandler_StageTransition : MessageHandler
    {
        private IGameInitializer _initializer = null;

        public static MessageHandler handler = null;
        public static List<IListener> listeners = null;

        public MessageHandler_StageTransition(IGameInitializer initializer)
        {
            _initializer = initializer;
            _handlerName = "message handler - stage transition";
            listeners = new List<IListener>();

            handler = this;
        }

        public override void HandleMessages()
        {
            _listMessages.Clear();

            foreach (Message m in _queue)
            {
                _listMessages.Add(m);
            }

            _queue.Clear();

            foreach (Message m in _listMessages)
            {
                if (m.MESSAGE_TYPE == MessageType.STAGE_TRANSITION)
                {
                    System.Type stageType = m.types[0];

                    MessageDebug.Log("received stage transition message: " + stageType.Name);

                    if (stageType.IsSubclassOf(typeof(BaseStage)))
                    {
                        _initializer.AddNextStage(stageType);
                    }
                }
            }

            foreach (Message m in _listMessages)
            {
                foreach (IListener listener in listeners)
                {
                    listener.OnNotify(m);
                }
            }

            _listMessages.Clear();
        }
    }
}