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
        public XmlReader xmlDocs;
        private string xmlName;

        public PathManager(string path) => xmlPath = path;

        public string xmlPath
        {
            get => _path;
            set => _path = value;
        }

        public void OpenXml(string fileName)
        {
            this.xmlName = "/"+fileName;
            //this.xmlDocs = new XmlDocument();
            xmlDocs = XmlReader.Create(_path + xmlName);
        }

    }

    public class DataManager : PathManager
    {
        //dialog, charactor data, mob data, etc .. management class
        private string xmlText;
        private string xmlTag;
        private int xmlIndex;
        public enum dbType { Dialog, Charator, Mob }

        public DataManager(string filePath) : base(filePath)
        {
            this.xmlTag = string.Empty;
            this.xmlIndex = 0;
            this.xmlText = string.Empty;
        }

        public void readXml(dbType type)
        {
            switch(type) {
                case dbType.Dialog:
                    OpenXml("Text");
                break;
                default:
                    //another case is not execute. 
                    //dev not yet
                break;
            }

            while(xmlDocs.Read()) {
                if(xmlDocs.IsStartElement("Row")) {
                    xmlTag = xmlDocs.GetAttribute("tag");
                    xmlIndex = Int32.Parse(xmlDocs.GetAttribute("index"));

                    xmlDocs.Read();

                    xmlText = xmlDocs.ReadElementContentAsString("Text", "");
                }
            }

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
