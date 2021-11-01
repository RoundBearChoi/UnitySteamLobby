using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.GameElements
{
    public class TestPlayer : GameElement
    {
        [SerializeField] Vector3 _targetPos = Vector3.zero;
        [SerializeField] Vector3 _dir = Vector3.zero;
        [SerializeField] float _speed = 0f;

        public override void InitGameElement(IGameInitializer initializer)
        {
            _initializer = initializer;
            _speed = 0.01f;
        }

        public override void OnUpdate()
        {
            DetectedInputDevice input = _initializer.INPUT_DEVICES.GetInputDevice(0);

            Vector3 mousePick = _initializer.GAMECAMC_CONTROLLER.GAME_CAMERA.GetMouseRay(input.MOUSE);

            if (mousePick != Vector3.zero)
            {
                if (input.MOUSE.leftButton.wasPressedThisFrame)
                {
                    _targetPos = mousePick;
                    _speed = 0.02f;
                }
            }
        }

        public override void OnFixedUpdate()
        {
            if (_targetPos != Vector3.zero)
            {
                Vector3 dir = _targetPos - this.transform.position;
                dir.Normalize();

                if (Vector3.SqrMagnitude(_targetPos - this.transform.position) > 0.0001f)
                {
                    this.transform.position += (dir * _speed);
                }
            }

            _initializer.STEAM_CONTROL.Send_Player_Position(this.transform.position);
        }
    }
}