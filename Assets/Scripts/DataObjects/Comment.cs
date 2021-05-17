using System;

namespace FaceOff.DataObjects
{
    public class Comment : Record
    {
        public User User;
        public DateTime When;
    }
}
