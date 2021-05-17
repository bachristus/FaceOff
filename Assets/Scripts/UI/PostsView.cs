using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FaceOff.DataObjects;

namespace FaceOff.GUI
{
    public class PostsView : View
    {     
        [SerializeField] private NewPostView newPostView;
        [SerializeField] private PostsListView postsList;   
        
        public event Action<Post> NewPostRequested;

        private void Start()
        {
            newPostView.NewPostRequested += OnNewPostRequested;
        }

        private void OnDestroy()
        {
            newPostView.NewPostRequested -= OnNewPostRequested;
        }

        private void OnNewPostRequested(Post post)
        {
            NewPostRequested?.Invoke(post);
        }

        internal void ShowPosts(IEnumerable<Post> posts)
        {
            postsList.ShowPosts(posts);
        }

        public override void ClearContent()
        {
            postsList.ClearContent();
            newPostView.ClearContent();
        }
    }
}