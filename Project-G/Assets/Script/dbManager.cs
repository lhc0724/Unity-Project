using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;

//using UnityEngine;

namespace dbManager
{

    public class XmlManager
    {
        private string _path;
        public XmlReader xmlDocs;
        private string _xmlName;

        private bool _isError;

        public XmlManager(string filePath) {
            xmlPath = filePath;
            xmlName = string.Empty;
            _isError = false;
        }

        private string xmlPath {
            get => _path;
            set => _path = value;
        }

        public string xmlName {
            get => _xmlName;
            set => _xmlName = value;
        }

        public bool checkError {
            get => _isError;
        }

        public bool OpenXml() {
            if (this._xmlName != string.Empty) {
                xmlDocs = XmlReader.Create(_path + _xmlName);
                return true;
            }
            return false;
        }
        public void OpenXml(string fileName) {
            this._xmlName = "/"+fileName;
            //this.xmlDocs = new XmlDocument();
            xmlDocs = XmlReader.Create(_path + _xmlName);
        }

        public List<DataManager> LoadData(string sceneName) {

            List<DataManager> dataList = new List<DataManager>();
            DataManager readData; 

            if(!OpenXml()) {
                //error
                _isError = true;
                return null;
            }

            while (xmlDocs.Read()) {
                if(xmlDocs.IsStartElement("Row")) {
                    readData = new DataManager();
                    readData.Tag = xmlDocs.GetAttribute("tag");
                    readData.Index = Convert.ToInt32(xmlDocs.GetAttribute("index"));

                    xmlDocs.ReadToDescendant("Text");
                    readData.Text = xmlDocs.ReadElementContentAsString();

                    readData.Text = readData.Text.Replace("[NEWLINE]", "\r\n");

                    if (readData.Tag != sceneName) {
                        break;
                    } else {
                        dataList.Add(readData);
                    }
                }
            }

            xmlDocs.Close();

            return dataList;
        }

        public List<DataManager> LoadData(string sceneName, string fileName) {
            //@overloading function
            this._xmlName = "/" + fileName;
            return LoadData(sceneName);
        }

    }

    public class DataManager
    {
        //dialog, charactor data, mob data, etc .. management class
        private string _xmlText;
        private string _xmlTag;
        private int _xmlIndex;
        public enum dbType { Dialog, Charator, Mob }

        public DataManager()
        {
            this._xmlTag = string.Empty;
            this._xmlIndex = 0;
            this._xmlText = string.Empty;
        }

        public string Text {
            get => _xmlText;
            set => _xmlText = value;
        }

        public string Tag {
            get => _xmlTag;
            set => _xmlTag = value;
        }

        public int Index {
            get => _xmlIndex;
            set => _xmlIndex = value;
        }
    }   
}
