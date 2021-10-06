using System.Collections;
using System.Collections.Generic;
using System.Threading;

using UnityEngine;
using UnityEngine.SceneManagement;

using dbManager;

public class GameManager : MonoBehaviour
{   
    public XmlManager xml_mgr;      //Variables that manage xml to be referenced in the game.
    public DialogDatas dialog_db;   //dialog data set, in the currently required dialog ui.

    #region Singleton

    public static GameManager instance = null;

    //singleton instance init
    private void Awake()
    {   
        if(instance == null) {  
            //system has not walking singleton instance.
            instance = this;
            DontDestroyOnLoad(gameObject);  //if on load secene, don't destroy myself.

            xml_mgr = new XmlManager(Application.dataPath + "/Xml");
            dialog_db = new DialogDatas();

            /*** debug code ***/
            // dialog_db = xml_mgr.LoadDialogwithScene("Tutorial_Scene_1", "Text.xml");

            // for(int i = 0; i < dialog_db.Index.Count; i++ ) {
            //     Debug.Log(dialog_db.Index[i].ToString());
            //     Debug.Log(dialog_db.Tag[i]);
            //     Debug.Log(dialog_db.Text[i]);
            // }

            /*** end debug ***/

        }else {
            if(instance != this) {
                //system just have singleton instance and not use this instance.
                Destroy(this.gameObject);
            }
        }
    }

    private void Start()
    {
        //this code is for use scene loading test.
        //this function will be button's on_click event on the 'Start_Scene' later.
        if (SceneManager.GetActiveScene().name == "Start_Scene") {
            //SceneManager.LoadScene("Tutorial_Scene_1", LoadSceneMode.Single);
            StartCoroutine(AsyncSceneLoading("Tutorial_Scene_1"));
        }
    }

    private IEnumerator AsyncSceneLoading(string nextSceneName)
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Single);

        //threading read xml data.
        Thread xmlThd = new Thread(() =>
          dialog_db = xml_mgr.LoadDialogwithScene(nextSceneName, "Text.xml"));
        xmlThd.Start();

        asyncOp.allowSceneActivation = false;

        while(!asyncOp.isDone) {
            yield return new WaitForSeconds(0.1f);
            if(asyncOp.progress >= 0.9f) {
                //synchoronous thread.
                xmlThd.Join();
                asyncOp.allowSceneActivation = true;
                break;
            }
        }

        StopCoroutine("AsyncSceneLoading");
        yield return null;
    }

    #endregion

}
