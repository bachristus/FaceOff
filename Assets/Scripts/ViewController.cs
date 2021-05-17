using FaceOff.DataObjects;
using System;
using UnityEngine;

namespace FaceOff.GUI
{
    public class ViewController : MonoBehaviour
    {
        public Model model;

        public SignInView signInView;
        public CurrentUserView currentUserView;
        public RegisterNewUserView registerUserView;
        public PostsView postsView;
        public StatusView statusView;
        
        // Start is called before the first frame update
        void Start()
        {            
            registerUserView.Hide();
            postsView.Hide();
            currentUserView.Hide();

            statusView.Show();
            signInView.Show();

            signInView.RegisterNewUserRequested += OnRegisterNewUserRequested;
            signInView.SignInRequested += OnSignInRequested;

            registerUserView.CreateNewUserRequested += OnCreateNewUserRequested;

            currentUserView.SignOutRequested += OnSignOutRequested;
            postsView.NewPostRequested += OnNewPostRequested;

            model.ErrorOccured += OnErrorOccured;            
        }

        private void OnDestroy()
        {
            model.ErrorOccured -= OnErrorOccured;
            postsView.NewPostRequested -= OnNewPostRequested;
            currentUserView.SignOutRequested -= OnSignOutRequested;

            registerUserView.CreateNewUserRequested -= OnCreateNewUserRequested;

            signInView.SignInRequested -= OnSignInRequested;
            signInView.RegisterNewUserRequested -= OnRegisterNewUserRequested;
        }

        private void OnNewPostRequested(Post post)
        {
            model.CreatePost(post);
            var posts = model.GetPosts();
            this.postsView.ShowPosts(posts);
        }

        private void OnSignOutRequested()
        {
            model.SignOut();
            currentUserView.Hide();
            postsView.Hide();
            signInView.Show();
        }

        private void OnSignInRequested(User user)
        {
            if (model.SignIn(user))
            {
                signInView.Hide();
                currentUserView.Show();
                currentUserView.Show(model.CurrentUser);

                postsView.Show();
                var posts=model.GetPosts();
                postsView.ShowPosts(posts);
            }
        }

        private void OnRegisterNewUserRequested()
        {            
            registerUserView.Show();
        }

        private void OnCreateNewUserRequested(User user)
        {
            if (model.CreateUser(user))
            {
                registerUserView.Hide();
                signInView.Show();
            }            
        }

        private void OnErrorOccured(string message)
        {        
            statusView.DisplayError(message);
        }        
    }
}