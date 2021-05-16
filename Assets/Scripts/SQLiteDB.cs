using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using System.IO;

namespace FaceOff
{
    public class SQLiteDB : MonoBehaviour, IDatabase
    {
        public event Action<Post> PostUpdated;
        public event Action<Comment> CommentedUpdated;

        [SerializeField] private string DbFileName = @"FaceOff.db";
        SqliteConnection DbConnection;
        
        // Start is called before the first frame update
        void Start()
        {

            string connectionString = "URI=file:" + Path.Combine(Application.dataPath, DbFileName);
            using (DbConnection = new SqliteConnection(connectionString))
            {                
                DbConnection.Open();

                //DropPersonTable();
                //DropPostTable();
                //DropCommentTable();

                CreatePersonTable();
                CreatePostTable();
                CreateCommentTable();

                DbConnection.Close();
            }
        }

        private void CreatePostTable()
        {
            string query = "CREATE TABLE IF NOT EXISTS[Post] (ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, PersonID INTEGER NOT NULL, Text VARCHAR(511), Picture BLOB, Timestamp INTEGER NOT NULL, FOREIGN KEY (PersonID) REFERENCES Person(ID)); ";
            DoExecuteScalarQuery(query);            
        }

        private void CreateCommentTable()
        {
            string query = "CREATE TABLE IF NOT EXISTS[Comment] (ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, PostID INTEGER NOT NULL, PersonID INTEGER NOT NULL, Text VARCHAR(255) NOT NULL, Timestamp INTEGER NOT NULL, FOREIGN KEY (PostID) REFERENCES Post(ID), FOREIGN KEY (PersonID) REFERENCES Person(ID));";
            DoExecuteScalarQuery(query);
        }

        private void CreatePersonTable()
        {
            string query = "CREATE TABLE IF NOT EXISTS[Person] (ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, Name VARCHAR(64) NOT NULL, Avatar Blob);";
            DoExecuteScalarQuery(query);
        }

        private void DropPersonTable()
        {
            string query = "DROP TABLE IF EXISTS[Person];";
            DoExecuteScalarQuery(query);
        }

        private void DropPostTable()
        {
            string query = "DROP TABLE IF EXISTS[Post];";
            DoExecuteScalarQuery(query);
        }

        private void DropCommentTable()
        {
            string query = "DROP TABLE IF EXISTS[Comment];";
            DoExecuteScalarQuery(query);
        }

        public void CreateUser(User user)
        {
            string query = "INSERT INTO Person (Name, Avatar) VALUES (@name, @avatar);";
            var dbCommand = DbConnection.CreateCommand();
            dbCommand.CommandText = query;
            dbCommand.Parameters.Add("@name",DbType.String).Value= user.Name;
            dbCommand.Parameters.Add("@avatar", DbType.Binary).Value=user.Avatar;
            ExecuteScalarDbCommand(dbCommand);
        }


        public User GetUser(User user)
        {
            using (DbConnection)
            {
                DbConnection.Open();
                
                var dbCommand = DbConnection.CreateCommand();
                
                if (user.ID >= 0)
                {
                    dbCommand.CommandText = "SELECT * FROM Person WHERE ID=@id;";
                    dbCommand.Parameters.Add("@id", DbType.Int32).Value = user.ID;
                }
                else if(string.IsNullOrWhiteSpace(user.Name))
                {
                    throw new Exception("Either User ID or Name should be provided");
                }
                else
                {
                    dbCommand.CommandText = "SELECT * FROM Person WHERE Name=@name;";
                    dbCommand.Parameters.Add("@name", DbType.String).Value = user.Name;                    
                }

                var reader = dbCommand.ExecuteReader();
                User resultUser = null ;
                if (reader.HasRows)
                {
                    reader.Read();
                    var field = reader["Avatar"];
                    resultUser = new User
                    {
                        ID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Avatar = (byte[])reader["Avatar"]
                    };                    
                }
                DbConnection.Close();
                return resultUser;
            }            
        }

        public void CreatePost(Post post)
        {
            string query = "INSERT INTO Post (PersonID, Text, Picture, Timestamp) VALUES (@personid, @text, @picture, strftime('%s','now'));";
            var dbCommand = DbConnection.CreateCommand();
            dbCommand.CommandText = query;
            dbCommand.Parameters.Add("@personid", DbType.Int32).Value = post.User.ID;
            dbCommand.Parameters.Add("@text", DbType.String).Value = post.Text;
            dbCommand.Parameters.Add("@picture", DbType.Binary).Value = post.Picture;
           
            ExecuteScalarDbCommand(dbCommand);
        }

        public List<Post> GetPosts()
        {
            return GetPosts(null);
        }

        public List<Post> GetPosts(User user)
        {
            using (DbConnection)
            {
                DbConnection.Open();

                var dbCommand = DbConnection.CreateCommand();

                if (user!=null&&user.ID >= 0)
                {
                    dbCommand.CommandText = "SELECT Post.ID, PersonID, Person.Name, Text, Picture, datetime(Timestamp,'unixepoch') FROM Post JOIN Person ON Post.PersonID=Person.ID WHERE PersonID=@id;";
                    dbCommand.Parameters.Add("@id", DbType.Int32).Value = user.ID;
                }                
                else
                {
                    dbCommand.CommandText = "SELECT Post.ID, PersonID, Person.Name, Text, Picture, datetime(Timestamp,'unixepoch') FROM Post JOIN Person ON Post.PersonID=Person.ID ;";                   
                }

                var reader = dbCommand.ExecuteReader();
                List<Post> list = new List<Post>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var result = new Post
                        {
                            ID = reader.GetInt32(0),
                            User = new User { ID = reader.GetInt32(1), Name=reader.GetString(2) },
                            Text = reader.GetString(3),
                            Picture = (byte[])reader["Picture"],
                            When = reader.GetDateTime(5)
                        };
                        list.Add(result);
                    }
                }
                DbConnection.Close();
                return list;
            }
        }

        private void DoExecuteScalarQuery(string sqlQuery)
        {
            var dbCommand = DbConnection.CreateCommand();
            dbCommand.CommandText = sqlQuery;
            dbCommand.ExecuteScalar();
        }

        private void ExecuteScalarDbCommand(SqliteCommand dbCommand)
        {
            using (DbConnection)
            {
                DbConnection.Open();               
                dbCommand.ExecuteScalar();                
                DbConnection.Close();
            }
        }

        private void ExecuteReaderDbCommand(SqliteCommand dbCommand)
        {
            using (DbConnection)
            {
                DbConnection.Open();
                var reader=dbCommand.ExecuteReader();
                DbConnection.Close();
            }
        }

        private void ExecuteNonQueryDbCommand(SqliteCommand dbCommand)
        {
            using (DbConnection)
            {
                DbConnection.Open();
                dbCommand.ExecuteNonQuery();
                DbConnection.Close();
            }
        }

        public List<Comment> GetComments(Post post)
        {
            throw new NotImplementedException();
        }    

        public void CreateComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public void UpdatePost(Post post)
        {
            throw new NotImplementedException();
        }

        public void UpdateComment(Comment comment)
        {
            throw new NotImplementedException();
        }
    }
}