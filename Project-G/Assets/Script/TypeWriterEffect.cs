using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using System.Xml;
using System.IO;

public class TypeWriterEffect : MonoBehaviour
{
    /* --- temporary variables --- */
    public float t_Dly;         //time val, delay time
    public float t_SkippedDly;  //time val, skipped delay time
    public int cnt = 0;             //universal count variable

    /* --- typing effect variables --- */
    public string[] str_txts;  //string type array variable, all texts stack
    public int print_count;    //text index number variable
    string str_currtxt;        //current effectly text

    /* --- typing check variables --- */
    public bool b_txtExit;
    public bool b_txtFull;
    public bool b_txtCut;
    
    [SerializeField]
    private TextAsset xmlTxt;
    private XmlDocument xmlDoc;
    private string _path;

    //start with typing.
    void Start()
    {
        _path = Application.dataPath + "/Xml";  //load XML directory path
        LoadXml();
        //Debug.Log(_path);

        Get_Typing(print_count, str_txts);
    }


    //exit after print screen all texts.
    void Update()
    {
        if (b_txtExit == true) {
            gameObject.SetActive(false);
        }
        
    }

    //end current text and show next text function.
    public void End_Typing()
    {
        if (b_txtFull == true) {
            //call next texts typing

            cnt++;
            b_txtFull = false;
            b_txtCut = false;
            StartCoroutine(ShowText(str_txts));

        } else {
            //skip typing
            b_txtCut = true;
        }
    }

    //call start text
    public void Get_Typing(int _index, string[] _fullText)
    {
        //init for reuse variables.
        b_txtExit = false;
        b_txtFull = false;
        b_txtCut = false;
        cnt = 0;

        //load text variables.
        print_count = _index;
        str_txts = new string[print_count];
        str_txts = _fullText;

        //start typing coroutine.
        StartCoroutine(ShowText(str_txts));
    }

    IEnumerator ShowText(string[] _fullText)
    {
        //exit all text typing.
        if (cnt >= print_count) {
            b_txtExit = true;
            StopCoroutine("showText");
        } else {
            //existing text clear.
            str_currtxt = "";
            //typing start.
            for (int i = 0; i < _fullText[cnt].Length; i++) {
                //exit in typing action.
                if (b_txtCut == true) {
                    break;
                }
                //one charactor print screen at time.
                str_currtxt = _fullText[cnt].Substring(0, i + 1);
                this.GetComponent<Text>().text = str_currtxt;
                yield return new WaitForSeconds(t_Dly);
            }
            //if exit then print screen all text.
            //Debug.Log("Typing 종료");
            this.GetComponent<Text>().text = _fullText[cnt];
            yield return new WaitForSeconds(t_SkippedDly);

            //exit after skip delay time;
            //Debug.Log("Enter 대기");
            b_txtFull = true;
        }
    }

    private void LoadXml()
    {
        xmlDoc = new XmlDocument();
        xmlDoc.Load(_path+"/Text.xml");

        string xmltxt;

        XmlNodeList nodes = xmlDoc.SelectNodes("TextGroup/Tutorial");
        foreach(XmlNode tmp in nodes) {
            Debug.Log("index : "+tmp.SelectSingleNode("Index").InnerText);

            xmltxt = tmp.SelectSingleNode("Text").InnerText;
            xmltxt = xmltxt.Replace("\\r", "\r");
            xmltxt = xmltxt.Replace("\\n", "\n");
            Debug.Log(xmltxt);
        }
        //Debug.Log(xmlDoc);
    }
}
