using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace FaceOff {
    public static class ImageLoader 
    {       
        public static byte[] PickImageFromFile()
        {
            try
            {
                string[] filters = new[] { "Image files", "png,jpg,jpeg,bmp", "All files", "*" };
                string path = EditorUtility.OpenFilePanelWithFilters("Pick avatar picture", "", filters);

                if (path.Length != 0)
                {
                    return File.ReadAllBytes(path);                    
                }
                return null;

                //FileDialog.CreateOpenFileDialog().ShowDialog();
            }
            catch(Exception)
            {
                return null;
            }
        }       
    }
}