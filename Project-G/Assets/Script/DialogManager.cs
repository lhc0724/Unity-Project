using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using dbManager;

public class DialogManager : MonoBehaviour
{
    // Start is called before the first frame update
    List<DataManager> DialogList = new List<DataManager> ();
    XmlManager xmlParser;

    EventWaitHandle thd_started = new EventWaitHandle(false, EventResetMode.ManualReset);
    EventWaitHandle thd_exited = new EventWaitHandle(false, EventResetMode.ManualReset);

    private string _sceneName;

    public bool isThdStarted = true;
    public bool isThdExited = true;
    
    void Start()
    {
        xmlParser = new XmlManager(Application.dataPath + "/Xml");
        _sceneName = SceneManager.GetActiveScene().name;
        //new Thread (() => getCurrSceneDialog()).Start();

        Thread thd = new Thread(getCurrSceneDialog);
        thd.Start((thd_started, thd_exited));

        Debug.Log($"{isThdStarted}, {isThdExited}");

        isThdStarted = thd_started.WaitOne(0);
        isThdExited = thd_exited.WaitOne(0);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($" => {isThdStarted}, {isThdExited}");
    }

    void getCurrSceneDialog(object obj)
    {
        var events = (System.ValueTuple<EventWaitHandle, EventWaitHandle>) obj;
        {
            events.Item1.Set();

            xmlParser.xmlName = "/Text.xml";
            DialogList = xmlParser.LoadData(_sceneName);

            events.Item2.Set();
        }
    }
}
