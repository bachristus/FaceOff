using System;
using UnityEngine;
using FaceOff.DataObjects;

namespace FaceOff.GUI
{
    public class NewPostView : View
    {              
        [SerializeField] private TMPro.TMP_InputField PostInput;
        [SerializeField] private PictureImage PostPicture;
        
        public event Action<Post> NewPostRequested;

        public void OnPickPostPictureClick()
        {
            PostPicture.Picture=new Picture(ImageLoader.PickImageFromFile());
        }

        public void OnPostButtonClick()
        {
            NewPostRequested?.Invoke(new Post { Text = PostInput.text, Picture = PostPicture.Picture });
        }

        public override void ClearContent()
        {
            PostInput.text = "";
            PostPicture.Picture = null;
        }
    }
}