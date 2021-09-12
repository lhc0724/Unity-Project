using System;
using System.Xml;
using System.IO;
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

    public class ReadXml
    {
        private XmlDocument xmlDoc;
        private string xmlName;

        public ReadXml(string xmlFileName)
        {
            this.xmlDoc = new XmlDocument();
            this.xmlName = "/" + xmlFileName;
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

        public string getTag()
        {
            return tag;
        }

        public int getIndex()
        {
            return textIndex;
        }

        public string getText()
        {
            return text;
        }
    }
}
