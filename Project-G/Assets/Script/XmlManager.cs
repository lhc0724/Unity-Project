using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace XmlManager
{
    public class PathManager
    {
        private string _path;

        public PathManager(string path) => xmlPath = path;

        public string xmlPath
        {
            get => _path;
            set => _path = value;
        }

    }

    public class DialogParser
    {
        private string tag;
        private int textIndex;
        private string text;

        public DialogParser()
        {
            this.tag = string.Empty;
            this.textIndex = 0;
            this.text = string.Empty;
        }

        public void setDialogData(string tag, string index, string text)
        {
            this.tag = tag;
            this.textIndex = Int32.Parse(index);
            this.text = text;
        }
    }
}
