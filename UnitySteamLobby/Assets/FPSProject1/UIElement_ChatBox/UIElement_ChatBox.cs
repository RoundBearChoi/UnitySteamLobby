using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RB.UI
{
    public class UIElement_ChatBox : UIElementObj
    {
        [Space(10)]
        [SerializeField] List<Color> _listIDTextColors = new List<Color>();
        [Space(10)]
        [SerializeField] Dictionary<int, ulong> _dicColors;
        [SerializeField] Color _notificationColor = new Color();
        [SerializeField] List<int> _recordedChatBoxLength = new List<int>();

        [Space(10)]
        [Header("Debug")]
        [SerializeField] Text _text = null;

        public override void Init(IGameInitializer initializer)
        {
            _initializer = initializer;
            _listener = new ChatBox_Listener(initializer, this);
            _dicColors = new Dictionary<int, ulong>();

            Transform[] arr = this.gameObject.GetComponentsInChildren<Transform>();

            foreach(Transform t in arr)
            {
                if (t.name.Equals("ChatText"))
                {
                    _text = t.GetComponent<Text>();
                }
            }

            _text.text = string.Empty;

            if (_initializer.SERVER.SERVER_STARTED)
            {
                AddNotification("Server started.. Invite your friends on Steam!");
            }
        }

        public void AddChatText(ulong steamID, string name, string text)
        {
            string str = string.Empty;
            string c = GetClientTextColor(steamID);

            str += (System.Environment.NewLine);
            str += ("<color=#" + c + ">" + name + ": </color>");
            str += (text);

            _text.text += str;

            _recordedChatBoxLength.Add(str.Length);

            if (_recordedChatBoxLength.Count > 25)
            {
                _text.text = _text.text.Substring(_recordedChatBoxLength[0], _text.text.Length - _recordedChatBoxLength[0]);
                _recordedChatBoxLength.RemoveAt(0);
            }
        }

        public void AddNotification(string text)
        {
            string str = string.Empty;
            string c = ColorUtility.ToHtmlStringRGBA(_notificationColor);

            str += (System.Environment.NewLine);
            str += ("<i><color=#" + c + ">" + text + "</color></i>");

            _text.text += str;

            _recordedChatBoxLength.Add(str.Length);
        }

        public void RemoveColorIndex(ulong steamID)
        {
            foreach(KeyValuePair<int, ulong> data in _dicColors)
            {
                if (data.Value == steamID)
                {
                    _dicColors.Remove(data.Key);
                    break;
                }
            }
        }

        string GetClientTextColor(ulong steamID)
        {
            string c = ColorUtility.ToHtmlStringRGBA(Color.white);

            Network.HandShakenPlayer[] arr;

            if (_initializer.SERVER.SERVER_STARTED)
            {
                arr = _initializer.SERVER.GetAllHandShakenPlayers();
            }
            else
            {
                arr = _initializer.CLIENT.GetAllHandShakenPlayers();
            }

            int colorIndex = 0;

            if (!_dicColors.ContainsValue(steamID))
            {
                colorIndex = GetUnusedIndex();

                if (colorIndex >= 0)
                {
                    _dicColors.Add(colorIndex, steamID);
                }
            }
            else
            {
                colorIndex = GetColorIndex(steamID);
            }

            if (colorIndex >= 0)
            {
                if (colorIndex < arr.Length)
                {
                    c = ColorUtility.ToHtmlStringRGBA(_listIDTextColors[colorIndex]);
                }
            }

            return c;
        }

        int GetUnusedIndex()
        {
            for (int i = 0; i < 10; i++)
            {
                if (!_dicColors.ContainsKey(i))
                {
                    return i;
                }
            }

            return -1;
        }

        int GetColorIndex(ulong steamID)
        {
            foreach(KeyValuePair<int, ulong> data in _dicColors)
            {
                if (data.Value == steamID)
                {
                    return data.Key;
                }
            }

            return -1;
        }
    }
}