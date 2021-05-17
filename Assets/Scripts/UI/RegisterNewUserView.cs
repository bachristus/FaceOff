using System;
using UnityEngine;
using UnityEngine.UI;
using FaceOff.DataObjects;

namespace FaceOff.GUI
{
    public class RegisterNewUserView : View
    {        
        private string Name => NameInput.text;


        [SerializeField] private TMPro.TMP_InputField NameInput;

        [SerializeField] private PictureImage AvatarPicture;

        public event Action<User> CreateNewUserRequested;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnPickAvatarClick()
        {
            AvatarPicture.Picture=new Picture(ImageLoader.PickImageFromFile());
        }

        public void OnRegisterButtonClick()
        {
            CreateNewUserRequested?.Invoke(new User { Name = Name, Avatar = AvatarPicture.Picture });
        }

        public override void ClearContent()
        {
            NameInput.text = "";
            AvatarPicture.Picture = null;
        }
    }
}
