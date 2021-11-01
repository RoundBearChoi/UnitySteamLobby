using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RB
{
    public class GameCamera : MonoBehaviour
    {
        IGameInitializer _initializer = null;
        [SerializeField] Camera _cam = null;

        public virtual void InitCam(IGameInitializer initializer)
        {
            _initializer = initializer;
            _cam = this.gameObject.GetComponentInChildren<Camera>();
        }

        public virtual Vector3 GetMouseRay(Pointer mousePointer)
        {
            Ray ray = _cam.ScreenPointToRay(mousePointer.position.ReadValue());

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                return hit.point;
            }

            return Vector3.zero;
        }
    }
}