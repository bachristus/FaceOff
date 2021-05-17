using System;
using UnityEngine;
using UnityEngine.UI;
using FaceOff.DataObjects;

namespace FaceOff.GUI
{
    public class CurrentUserView : View
    {
        [SerializeField] private TMPro.TMP_Text Name;
        [SerializeField] private PictureImage Avatar;

        public event Action SignOutRequested;

        internal void Show(User user)
        {
            Name.text = user.Name;
            Avatar.Picture=user.Avatar;                     
        }

        public void OnSignOutClick()
        {
            SignOutRequested?.Invoke();
        }

        public override void ClearContent()
        {
            Name.text = "";
            Avatar.Picture = null ;
        }
    }
}