using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    #region new_version

    EventWaitHandle thd_exited = new EventWaitHandle(false, EventResetMode.ManualReset);

    private string _currScene;

    private void Awake()
    {
        GameObject.Find("Panel").transform.gameObject.SetActive(false);
    }

    public void ReqViewDialog(string sceneName)
    {
        Thread dialog_thd;

        _currScene = sceneName;

        dialog_thd = new Thread(getCurrSceneDialog);
        dialog_thd.Start((thd_exited));
    }

    void getCurrSceneDialog(object obj)
    {
        GameManager obj_gm = GameManager.instance;

        var events = (System.ValueTuple<EventWaitHandle>)obj;
        {
            obj_gm.dialog_db = obj_gm.xml_mgr.LoadDialogwithScene(_currScene, "Text.xml");
            //xmlParser.xmlName = "/Text.xml";
            //DialogList = xmlParser.LoadData(_sceneName);

            events.Item1.Set();
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
