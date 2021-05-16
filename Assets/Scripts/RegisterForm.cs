using System;
using UnityEngine;
using UnityEngine.UI;

namespace FaceOff.GUI
{
    public class RegisterForm : Form
    {        
        private string Name => NameInput.text;


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
            ImageLoader.PickImageFromFile(AvatarImage);
        }

        public void OnRegisterButtonClick()
        {
            CreateNewUserRequested?.Invoke(new User { Name = Name, Avatar = ImageLoader.GetTexturePNG(AvatarImage) });;
        }
    }
}
