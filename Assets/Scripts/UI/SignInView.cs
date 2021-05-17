using FaceOff.DataObjects;
using System;
using UnityEngine;

namespace FaceOff.GUI
{
    public class SignInView : View
    {        
        [SerializeField] private TMPro.TMP_InputField UserName;

        public event Action<User> SignInRequested;
        public event Action RegisterNewUserRequested;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnSignIn()
        {
            SignInRequested?.Invoke(new User { Name = UserName.text });
        }

        public void OnRegisterNewUser()
        {
            RegisterNewUserRequested?.Invoke();
        }

        public override void ClearContent()
        {
            UserName.text = "";
            //UserName."Enter your name";
        }
    }
}