using System;
using System.Collections.Generic;
using UnityEngine;
using FaceOff.DB;
using FaceOff.DataObjects;

namespace FaceOff
{
    public class Model:MonoBehaviour
    {
        public SQLiteDB db;
        
        public event Action<string> ErrorOccured;

        public User CurrentUser { get; private set; }
       
        public bool CreateUser(User user)
        {
            try
            {                
                db.CreateUser(user);
                return true;
            }
            catch (Exception e)
            {
                ErrorOccured?.Invoke("Unable to create user with name " + name);
            }
            return false;
        }

        public void CreatePost(Post post)
        {
            try
            {                
                post.User = CurrentUser;
                post.When = DateTime.Now;
                db.CreatePost(post);
            }
            catch (Exception e)
            {
                ErrorOccured?.Invoke("Unable to post");
            }
        }

        internal void SignOut()
        {
            CurrentUser = null;
        }

        public void CreateComment(Comment comment)
        {
            try
            {
                comment.User = CurrentUser;
                comment.When = DateTime.Now;
                db.CreateComment(comment);
            }
            catch (Exception e)
            {
                ErrorOccured?.Invoke("Unable to post comment");
            }
        }

        public bool SignIn(User user)
        {
            try
            {
                User dbUser=db.GetUser(user);
                if (dbUser == null)
                {
                    ErrorOccured?.Invoke($"User with name '{user.Name}' doesn't exist");
                    return false;
                }
                CurrentUser = dbUser;
                return true;
            }
            catch (Exception e)
            {
                ErrorOccured?.Invoke("Unable to get user with name " + user.Name);
            }

            return false;
        }

        private User GetUser(User user)
        {
            try
            {
                return db.GetUser(user);
            }
            catch (Exception e)
            {
                ErrorOccured?.Invoke("Unable to get user with name " + user.Name);
            }

            return null;
        }

        public List<Post> GetPosts(User user)
        {
            try
            {
                return db.GetPosts(user);
            }
            catch (Exception e)
            {
                ErrorOccured?.Invoke("Unable to get comments");
            }

            return null;
        }

        public List<Post> GetPosts()
        {
            try
            {
                var posts = db.GetPosts();
                posts.Sort((a, b) => { return a.When.CompareTo(b.When); });
                posts.Reverse();
                return posts;
            }
            catch (Exception e)
            {
                ErrorOccured?.Invoke("Unable to get posts");
            }

            return null;
        }

        public List<Comment> GetComments(Post post)
        {
            try
            {
                return db.GetComments(post);
            }
            catch (Exception e)
            {
                ErrorOccured?.Invoke("Unable to get comments");
            }

            return null;
        }
    }
}