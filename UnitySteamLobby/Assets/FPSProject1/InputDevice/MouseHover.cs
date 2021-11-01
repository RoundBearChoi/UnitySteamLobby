using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace RB.UI
{
    public static class MouseHover
    {
        public static bool IsHovering(RectTransform rectTransform, Mouse mouse)
        {
            Vector2 textSize = new Vector2();
            Vector3 textScreenPos = new Vector3();

            textScreenPos = rectTransform.position;
            textSize = rectTransform.rect.size;

            Vector2 mouseScreenPos = new Vector2();
            mouseScreenPos.x = mouse.position.x.ReadValue();
            mouseScreenPos.y = mouse.position.y.ReadValue();

            if (mouseScreenPos.x > textScreenPos.x - (textSize.x * 0.5f) &&
                mouseScreenPos.x < textScreenPos.x + (textSize.x * 0.5f) &&
                mouseScreenPos.y > textScreenPos.y - (textSize.y * 0.5f) &&
                mouseScreenPos.y < textScreenPos.y + (textSize.y * 0.5f))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}