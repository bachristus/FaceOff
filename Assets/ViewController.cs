using UnityEngine;

namespace FaceOff.GUI
{
    public class ViewController : MonoBehaviour
    {
        public Model model;

        public SignInForm signInForm;
        public RegisterForm registerUserForm;
        public UserPostsForm postsForm;        

        private Form currentForm;

        // Start is called before the first frame update
        void Start()
        {            
            registerUserForm.Hide();
            postsForm.Hide();
            ShowForm(signInForm);

            signInForm.RegisterNewUserRequested += OnRegisterNewUserRequested;
            signInForm.SignInRequested += OnSignInRequested;

            registerUserForm.CreateNewUserRequested += OnCreateNewUserRequested;

            postsForm.SignOutRequested += OnSignOutRequested;
            postsForm.PostRequested += OnPostRequested;

            model.ErrorOccured += OnErrorOccured;            
        }

        private void OnPostRequested(Post post)
        {
            model.CreatePost(post);
        }

        private void OnSignOutRequested()
        {
            model.SignOut();
            ShowForm(signInForm);
        }

        private void OnSignInRequested(User user)
        {
            if (model.SignIn(user))
            {
                ShowForm(postsForm);
                var posts=model.GetPosts();
                postsForm.Show(model.CurrentUser, posts);
            }
        }

        private void OnRegisterNewUserRequested()
        {
            ShowForm(registerUserForm);
        }

        private void OnCreateNewUserRequested(User user)
        {
            if (model.CreateUser(user))
            {
                ShowForm(signInForm);
            }            
        }

        private void OnErrorOccured(string message)
        {
            currentForm.DisplayError(message);
        }

        private void ShowForm(Form form)
        {
            if (currentForm == form) return;
            currentForm?.Hide();
            currentForm = form;
            currentForm.Show();
        }
    }
}