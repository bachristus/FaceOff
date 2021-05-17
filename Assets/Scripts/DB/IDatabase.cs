using System;
using System.Collections.Generic;
using FaceOff.DataObjects;

namespace FaceOff.DB
{
    public interface IDatabase 
    {

        void CreateUser(User user);

        User GetUser(User user);

        List<Comment> GetComments(Post post);

        List<Post> GetPosts(User user);
        List<Post> GetPosts();
                
        void CreatePost(Post post);
        void CreateComment(Comment comment);

        void UpdatePost(Post post);
        void UpdateComment(Comment comment);
    }
}