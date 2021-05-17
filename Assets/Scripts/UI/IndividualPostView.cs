using UnityEngine;
using System;
using FaceOff.DataObjects;

namespace FaceOff.GUI
{
    public class IndividualPostView : MonoBehaviour
    {
        public PictureImage PostImage;
        public TMPro.TMP_Text PostText;        

        public event Action<Post> PostDetailsRequested;

        public Post Post
        {
            get
            {
                return CurrentPost;
            }
            set
            {
                CurrentPost = value;
                Refresh();
            }
        }      
        private Post CurrentPost;


        //private void Awake()
        //{
        //    PostImage = GetComponentInChildren<Image>();
        //    PostText = GetComponentInChildren<TMPro.TMP_Text>();            
        //}

        //// Start is called before the first frame update
        //void Start()
        //{

        //}

        //// Update is called once per frame
        //void Update()
        //{

        //}

        public void OnShowDetails()
        {
            PostDetailsRequested?.Invoke(CurrentPost);
        }

        private void Refresh()
        {
            if (CurrentPost == null)
            {
                PostImage.Picture = null;
                PostText.text = "";
            }
            else
            {
                PostImage.Picture = CurrentPost.Picture;
                PostText.text = String.Format($"[{CurrentPost.When.ToString("G")}] {CurrentPost.User.Name} : {CurrentPost.Text}");
            }
        }
    }
}