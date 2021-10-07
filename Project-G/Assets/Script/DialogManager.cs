using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    #region new_version

    private GameObject _panel;  //child object

    //current scene's dialog index number.
    //this variable has counted up when calling the ReqViewDialog function.
    private int _currIndex = 0; 

    /* --- print event flag variables --- */
    private bool b_pntFinish;  //print event finish flag vars.
    private bool b_pntCut;     //print event cutting flag vars.

    public Button btn_Skip;

    private void Awake()
    {
        //_panel = GameObject.Find("Canvas_Textbox").transform.Find("Panel").gameObject;
        _panel = this.transform.Find("Panel").gameObject;
        b_pntFinish = false;

        DialogEnabler(false); 
    }

    private void Start()
    {
        btn_Skip.onClick.AddListener(SkipDialog);
    }

    private void DialogEnabler(bool Activity)
    {
        if(Activity) {
            _panel.SetActive(true);
        }else {
            _panel.SetActive(false);
        }
    }

    public void ReqViewDialog()
    {
        List<string> textList = new List<string>();
        Time.timeScale = 0; //stop the game.

        //flag variables init.
        b_pntCut = false;
        b_pntFinish = false;

        if(GameManager.instance.dialog_db.Index.Count > 0) {
            //if exist valid dialog data, view on dialog.
            _currIndex++;           //index count up.
            for (int i = 0; i < GameManager.instance.dialog_db.Index.Count; i++) {
                if(GameManager.instance.dialog_db.Index[i] == _currIndex) {
                    textList.Add(GameManager.instance.dialog_db.Text[i]);
                }
            }
            
            DialogEnabler(true);    //dialog panel enable.
            //Debug.Log(_panel.transform.Find("Text").gameObject.GetComponent<Text>().text);
            StartCoroutine(printDialog(textList));
            StartCoroutine(SkipEvtListener());
        }
    }

    private IEnumerator printDialog(List<string> texts)
    {
        GameObject textBox = _panel.transform.Find("Text").gameObject;
        string currShowString;

        if (!b_pntFinish) {
            foreach(string tmpString in texts) {
                currShowString = "";

                for(int i = 0; i < tmpString.Length; i++) {
                    if (b_pntCut) {
                        textBox.GetComponent<Text>().text = tmpString;
                        break;
                    }
                    currShowString = tmpString.Substring(0, i + 1);
                    textBox.GetComponent<Text>().text = currShowString;
                    yield return new WaitForSecondsRealtime(0.1f);
                }

                if(b_pntCut) {
                    yield return new WaitForSecondsRealtime(0.05f);
                }else {
                    yield return new WaitForSecondsRealtime(0.3f);
                }
            }

            b_pntCut = false;
            b_pntFinish = true;         //finish flag enable.

            yield return new WaitForSecondsRealtime(1.0f);
        }

        //finished print dialog.
        DialogEnabler(false);
        Time.timeScale = 1.0f;          //re-operation this game.
        StopCoroutine("printDialog");   //stop this coroutine.

    }

    private IEnumerator SkipEvtListener()
    {
        while(!b_pntFinish) {
            if (Input.GetKey(KeyCode.Space)) {
                SkipDialog();
            }
            yield return new WaitForEndOfFrame();
        }

        StopCoroutine("SkipEvtListener");
    }

    public void SkipDialog()
    {
        if(!b_pntCut) {
            b_pntCut = true;
        }
    }

    #endregion  //end_newVer.
    #region old_version
    // Start is called before the first frame update
    // public List<DialogDatas> DialogList = new List<DialogDatas> ();
    // XmlManager xmlParser;

    // EventWaitHandle thd_started = new EventWaitHandle(false, EventResetMode.ManualReset);
    // EventWaitHandle thd_exited = new EventWaitHandle(false, EventResetMode.ManualReset);

    // private string _sceneName;

    // public bool isThdStarted = true;
    // public bool isThdExited = true;

    // public Thread thd;

    // void Start()
    // {
    // xmlParser = new XmlManager(Application.dataPath + "/Xml");
    // _sceneName = SceneManager.GetActiveScene().name;

    // //Debug.Log($"{isThdStarted}, {isThdExited}");

    // thd = new Thread(getCurrSceneDialog);
    // thd.Start((thd_started, thd_exited));


    // isThdStarted = thd_started.WaitOne(0);
    // isThdExited = thd_exited.WaitOne(0);
    // }

    // Update is called once per frame

    // void getCurrSceneDialog(object obj)
    // {
    //     var events = (System.ValueTuple<EventWaitHandle, EventWaitHandle>) obj;
    //     {
    //         events.Item1.Set();

    //         xmlParser.xmlName = "/Text.xml";
    //         //DialogList = xmlParser.LoadData(_sceneName);

    //         events.Item2.Set();
    //     }
    // }

    #endregion
}
