using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using dbManager;

public class TypeWriterEffect : MonoBehaviour
{
    /* --- temporary variables --- */
    public float t_Dly;         //time val, delay time
    public float t_SkippedDly;  //time val, skipped delay time

    /* --- typing effect variables --- */
    private string _currShowString;        //current effectly text
    private List<string> _textBuffer = new List<string>();

    /* --- typing check variables --- */
    public bool b_txtExit;
    public bool b_txtCut;

    public static bool isPrinting;

    DialogManager dialogData;
    

    //start with typing.
    void Start()
    {
        //variable init
        isPrinting  = false;
        b_txtExit   = false;
        b_txtCut    = false;

        dialogData = GameObject.Find("SysManager").GetComponent<DialogManager>();
    }

    //exit after print screen all texts.
    void Update()
    {
        if(!b_txtExit) {
            Time.timeScale = 0;
            if (dialogData.isThdExited) {
                dialogData.thd.Join();
            } else if (!dialogData.isThdExited && !isPrinting) {
                //Get_Typing(print_count, str_txts);
                print_Text(dialogData.DialogList, 1);
            }
        } else {
            //gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }

        if(Input.GetKeyDown("space")) {
            End_Typing();
        }
        
    }

    public void print_Text(List<DataManager> datas, int textIndex)
    {
        isPrinting = true;
        foreach (DataManager tmp in datas) {
            if(tmp.Index == textIndex) {
                _textBuffer.Add(tmp.Text);
            }
        }   

        //Debug.Log($"{textBuffer[0]}");
        StartCoroutine(typingAction(_textBuffer));
    }

    IEnumerator typingAction(List<string> texts)
    {
        if(b_txtExit) {
            StopCoroutine("typingAction");
            yield return null;
        } else {
            foreach(string buffer in texts) {
                _currShowString = "";

                for (int i = 0; i < buffer.Length; i++) {
                    if (b_txtCut) {
                        this.GetComponent<Text>().text = buffer;
                        break;
                    }
                    _currShowString = buffer.Substring(0, i + 1);
                    this.GetComponent<Text>().text = _currShowString;
                    //yield return new WaitForSeconds(t_Dly);
                    yield return new WaitForSecondsRealtime(t_Dly);
                }

                if(b_txtCut) {
                    yield return new WaitForSecondsRealtime(0.05f);
                }else {
                    yield return new WaitForSecondsRealtime(t_SkippedDly);
                }
            }
            b_txtCut    = false;
            isPrinting  = false;
            b_txtExit   = true;
        }

    }

    //end current text and show next text function.
    public void End_Typing()
    {
        if(!b_txtCut) {
            b_txtCut = true;
        }
    }

}
