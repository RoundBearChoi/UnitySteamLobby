using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RB.UI
{
    [System.Serializable]
    public class PlayerSlot
    {
        [SerializeField] GameObject _slotObj = null;
        [SerializeField] Image _backgroundImage = null;
        [SerializeField] RawImage _rawImage = null;
        [SerializeField] Text _playerName = null;
        [SerializeField] int _slotNumber = 0;

        public PlayerSlot(GameObject slotObj, int slotNumber)
        {
            _slotObj = slotObj;
            _slotNumber = slotNumber;

            _backgroundImage = slotObj.transform.Find("backgroundImage").GetComponent<Image>();
            _rawImage = slotObj.transform.Find("rawImage").GetComponent<RawImage>();
            _playerName = slotObj.GetComponentInChildren<Text>();

            Reset();
        }

        public void Reset()
        {
            _rawImage.enabled = false;
            _playerName.text = "SLOT " + _slotNumber;

            _backgroundImage.color = new Color(_backgroundImage.color.r, _backgroundImage.color.g, _backgroundImage.color.b, 0.2f);
            _playerName.color = new Color(_playerName.color.r, _playerName.color.g, _playerName.color.b, 0.2f);
        }

        public async void Update(Network.HandShakenPlayer player)
        {
            _playerName.text = player.mSteamName;
            _rawImage.enabled = true;

            _backgroundImage.color = new Color(_backgroundImage.color.r, _backgroundImage.color.g, _backgroundImage.color.b, 1f);
            _playerName.color = new Color(_playerName.color.r, _playerName.color.g, _playerName.color.b, 1f);

            System.Threading.Tasks.Task<Steamworks.Data.Image?> avatarImage = GetAvatar(player.mSteamID);
            await System.Threading.Tasks.Task.WhenAll(avatarImage);

            if (avatarImage.Result.HasValue)
            {
                Steamworks.Data.Image img = avatarImage.Result.Value;
                Texture2D tex = new Texture2D((int)img.Width, (int)img.Height);

                for (int x = 0; x < img.Width; x++)
                {
                    for (int y = 0; y < img.Height; y++)
                    {
                        Steamworks.Data.Color p = img.GetPixel(x, y);
                        tex.SetPixel(x, (int)img.Height - y, new Color(p.r / 255f, p.g / 255f, p.b / 255f, p.a / 255f));
                    }
                }

                tex.Apply();
                _rawImage.texture = tex;
            }
        }

        private async System.Threading.Tasks.Task<Steamworks.Data.Image?> GetAvatar(Steamworks.SteamId steamID)
        {
            try
            {
                return await Steamworks.SteamFriends.GetLargeAvatarAsync(steamID);
            }
            catch (System.Exception e)
            {
                SteamDebug.Log("system error loading player avatar image: " + e);
                return null;
            }
        }
    }
}