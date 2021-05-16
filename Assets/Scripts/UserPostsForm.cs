using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FaceOff.GUI
{
    public class UserPostsForm : Form
    {        
        public string PostText => PostInput?.text;

        [SerializeField] private TMPro.TMP_Text Name;
        [SerializeField] private Image Avatar;

        [SerializeField] private TMPro.TMP_InputField PostInput;
        [SerializeField] private Image PostPicture;

        [SerializeField] private PostsListView PostsList;

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

        internal void Show(User user, IEnumerable<Post> posts)
        {
            Name.text = user.Name;
            if (user.Avatar == null) return;
            ImageLoader.PutBitmapIntoImage(user.Avatar, Avatar);

            PostsList.ShowPosts(posts);
        }

        public void OnSignOutClick()
        {
            SignOutRequested?.Invoke();
        }

        public void OnPickPostPictureClick()
        {
            ImageLoader.PickImageFromFile(PostPicture);
        }

        public void OnPostButtonClick()
        {
            PostRequested?.Invoke(new Post { Text = PostText, Picture = ImageLoader.GetTexturePNG(PostPicture)});
        }
    }
}