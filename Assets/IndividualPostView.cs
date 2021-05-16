using UnityEngine;
using UnityEngine.UI;
using System;

namespace FaceOff.GUI
{
    public class IndividualPostView : MonoBehaviour
    {
        public Image PostImage;
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


        private void Awake()
        {
            PostImage = GetComponentInChildren<Image>();
            PostText = GetComponentInChildren<TMPro.TMP_Text>();            
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnShowDetails()
        {
            PostDetailsRequested?.Invoke(CurrentPost);
        }

        private void Refresh()
        {
            if (CurrentPost == null)
            {
                PostImage.sprite = null;
                PostText.text = "";
            }
            else
            {
                ImageLoader.PutBitmapIntoImage(CurrentPost.Picture, PostImage);                
                PostText.text = String.Format($"[{CurrentPost.When.ToString("G")}] {CurrentPost.User.Name} : {CurrentPost.Text}");
            }
        }
    }
}