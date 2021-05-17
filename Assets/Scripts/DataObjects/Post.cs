using System;

namespace FaceOff.DataObjects
{
    public class Post:Record
    {        
        public User User;
        public DateTime When;
        public string Text;
        public Picture Picture;
    }
}
