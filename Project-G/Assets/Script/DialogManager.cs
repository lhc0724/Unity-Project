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
        new Thread (() => getCurrSceneDialog()).Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void getCurrSceneDialog()
    {
        xmlParser = new XmlManager(Application.dataPath + "/Xml");
        xmlParser.xmlName = "/Text.xml";
        
        DialogList = xmlParser.LoadData(SceneManager.GetActiveScene().name);
    }
}
