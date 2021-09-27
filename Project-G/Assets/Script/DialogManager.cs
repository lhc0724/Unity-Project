using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using dbManager;

public class DialogManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<DataManager> DialogList = new List<DataManager> ();
    XmlManager xmlParser;

    EventWaitHandle thd_started = new EventWaitHandle(false, EventResetMode.ManualReset);
    EventWaitHandle thd_exited = new EventWaitHandle(false, EventResetMode.ManualReset);

    private string _sceneName;

    public bool isThdStarted = true;
    public bool isThdExited = true;

    public Thread thd;
    
    void Start()
    {
        xmlParser = new XmlManager(Application.dataPath + "/Xml");
        _sceneName = SceneManager.GetActiveScene().name;

        //Debug.Log($"{isThdStarted}, {isThdExited}");

        thd = new Thread(getCurrSceneDialog);
        thd.Start((thd_started, thd_exited));


        isThdStarted = thd_started.WaitOne(0);
        isThdExited = thd_exited.WaitOne(0);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($" => {isThdStarted}, {isThdExited}");
    }

    public void loadSceneDB()
    {

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
