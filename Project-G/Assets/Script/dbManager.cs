using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;

namespace dbManager
{
    public enum dataType { None = 0, Dialog, Charator, Mob, Skill, Item }

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
        public DialogDatas LoadDialog()
        {
            DialogDatas readData = new DialogDatas(); 

            string tmpText;

            if(!OpenXml()) {
                //error
                _isError = true;
                return null;
            }

            while (xmlDocs.Read()) {
                if(xmlDocs.IsStartElement("Row")) {
                    //readData = new DialogDatas();
                    //readData.Tag = xmlDocs.GetAttribute("tag");
                    readData.Tag.Add(xmlDocs.GetAttribute("tag"));

                    //readData.Index = Convert.ToInt32(xmlDocs.GetAttribute("index"));
                    readData.Index.Add(Convert.ToInt32(xmlDocs.GetAttribute("index")));

                    xmlDocs.ReadToDescendant("Text");
                    tmpText = xmlDocs.ReadElementContentAsString();

                    readData.Text.Add(tmpText.Replace("[NEWLINE]", "\r\n"));
                    //readData.Text = readData.Text.Replace("[NEWLINE]", "\r\n");
                }
            }

            xmlDocs.Close();

            return readData;
        }

        public DialogDatas LoadDialog(string fileName) {
            //@overloading function
            this._xmlName = "/" + fileName;
            return LoadDialog();
        }

        public DialogDatas LoadDialogwithScene(string sceneName)
        {
            DialogDatas readData = new DialogDatas();
            string tmpText;

            if (!OpenXml()) {
                //error
                _isError = true;
                return null;
            }

            while (xmlDocs.Read()) {
                if(xmlDocs.IsStartElement("Row")) {

                    tmpText = xmlDocs.GetAttribute("tag");

                    if(tmpText == sceneName) {
                        readData.Tag.Add(tmpText);
                        readData.Index.Add(Convert.ToInt32(xmlDocs.GetAttribute("index")));

                        xmlDocs.ReadToDescendant("Text");
                        tmpText = xmlDocs.ReadElementContentAsString();

                        readData.Text.Add(tmpText.Replace("[NEWLINE]", "\r\n"));
                    }
                }
            }
            
            xmlDocs.Close();

            return readData;
        }

        public DialogDatas LoadDialogwithScene(string sceneName, string fileName) {
            this._xmlName = "/" + fileName;
            return LoadDialogwithScene(sceneName);
        }

    }

    public class DialogDatas
    {
        //dialog, charactor data, mob data, etc .. management class
        private List<string> _xmlText;
        private List<string> _xmlTag;
        private List<int> _xmlIndex;

        private dataType _dbtype;

        public DialogDatas()
        {
            this._xmlTag = new List<string> ();
            this._xmlIndex = new List<int> ();
            this._xmlText = new List<string> ();
            this._dbtype = dataType.Dialog;
        }

        public List<string> Text {
            get => _xmlText;
            set => _xmlText = Text;
        }

        public List<string> Tag {
            get => _xmlTag;
            set => _xmlTag = Tag;
        }

        public List<int> Index {
            get => _xmlIndex;
            set => _xmlIndex = Index;
        }

        public dataType DBtype {
            get => _dbtype;
        }
    }   
}
