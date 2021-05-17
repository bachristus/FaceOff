using UnityEngine;

namespace FaceOff.DataObjects
{
    public class Picture
    {
        public byte[] Bytes;
        private byte[] vs;

        public Picture(byte[] bytes)
        {
            this.Bytes = bytes;
        }
    }
}
