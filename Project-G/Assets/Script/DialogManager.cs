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
    
    void Start()
    {
        xmlParser = new XmlManager(Application.dataPath + "/Xml");
        new Thread (() => getCurrSceneDialog(SceneManager.GetActiveScene().name)).Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void getCurrSceneDialog(string SceneName)
    {
        xmlParser.xmlName = "/Text.xml";
        DialogList = xmlParser.LoadData(SceneName);
    }
}
