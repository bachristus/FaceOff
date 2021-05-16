using System;
using UnityEngine;
using UnityEngine.UI;

namespace FaceOff {
    public class RegisterForm : Form
    {
        //private byte[] AvatarBytes;
        private string Name => NameInput.text;

        [SerializeField] private ImageLoader imageLoader;

        [SerializeField] private TMPro.TMP_InputField NameInput;

        [SerializeField] private Image AvatarImage;

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
            /*AvatarBytes=*/imageLoader.PickImageFromFile(AvatarImage);
        }

        public void OnRegisterButtonClick()
        {
            CreateNewUserRequested?.Invoke(new User { Name = Name, Avatar = imageLoader.GetTexturePNG(AvatarImage) });;
        }
    }
}
