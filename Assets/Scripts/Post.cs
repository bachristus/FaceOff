using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FaceOff
{
    public class Post:Record
    {        
        public User User;
        public DateTime When;
        public string Text;
        public byte[] Picture;
    }
}
