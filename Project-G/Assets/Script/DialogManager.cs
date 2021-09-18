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
    private string _sceneName;
    
    void Start()
    {
        xmlParser = new XmlManager(Application.dataPath + "/Xml");
        _sceneName = SceneManager.GetActiveScene().name;
        new Thread (() => getCurrSceneDialog()).Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void getCurrSceneDialog()
    {
        xmlParser.xmlName = "/Text.xml";
        DialogList = xmlParser.LoadData(_sceneName);
    }
}
