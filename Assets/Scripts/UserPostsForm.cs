using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FaceOff
{
    public class UserPostsForm : Form
    {
        //private byte[] PictureBytes;
        public string PostText => PostInput.text;

        [SerializeField] private ImageLoader imageLoader;
        [SerializeField] private TMPro.TMP_Text Name;
        [SerializeField] private Image Avatar;

        [SerializeField] private TMPro.TMP_InputField PostInput;
        [SerializeField] private Image PostPicture;

        public event Action SignOutRequested;
        public event Action<Post> PostRequested;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        internal void ShowUser(User user)
        {
            Name.text = user.Name;
            if (user.Avatar == null) return;
            imageLoader.PutBitmapIntoImage(user.Avatar, Avatar);
        }

        public void OnSignOutClick()
        {
            SignOutRequested?.Invoke();
        }

        public void OnPickPostPictureClick()
        {
            /*PictureBytes = */imageLoader.PickImageFromFile(PostPicture);
        }

        public void OnPostButtonClick()
        {
            PostRequested?.Invoke(new Post { Text = PostText, Picture = imageLoader.GetTexturePNG(PostPicture)});
        }
    }
}