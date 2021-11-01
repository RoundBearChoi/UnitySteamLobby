using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public abstract class GameResources<KeyType> : MonoBehaviour
    {
        [SerializeField]
        protected Dictionary<KeyType, Object> dicResources = new Dictionary<KeyType, Object>();

        public abstract void InitLoader();

        public virtual void LoadObj<ObjType>(KeyType keyType, string objName)
        {
            if (!dicResources.ContainsKey(keyType))
            {
                Object createdObj = Resources.Load(objName, typeof(ObjType));
                dicResources.Add(keyType, createdObj);
            }
        }

        public virtual Object GetLoadedObj(KeyType keyType)
        {
            if (dicResources.ContainsKey(keyType))
            {
                return dicResources[keyType];
            }
            else
            {
                return null;
            }
        }
    }
}