using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RB
{
    [System.Serializable]
    public class MessageHandler
    {
        [SerializeField] protected string _handlerName = "empty message handler";
        protected List<Message> _listMessages = new List<Message>();
        protected List<Message> _queue = new List<Message>();

        public virtual void HandleMessages() { ClassDebug.Log("undefined"); }

        public virtual void AddMessage(Message message)
        {
            _queue.Add(message);
        }
    }
}