using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map_Triggers : MonoBehaviour
{
    public GameObject Player;
    //public GameObject Canvas;

    Vector3 start_pos;
    Vector3 end_pos;
    Quaternion start_rot;
    static bool stage_end = false;

    void Awake()
    {
        Time.timeScale = 0f;   
    }

    // Update is called once per frame
    void Start()
    {
        //starting position initialize
        start_pos = GameObject.FindGameObjectWithTag("Start").transform.position;
        start_rot = GameObject.FindGameObjectWithTag("Start").transform.rotation;

        //end position initialize
        end_pos = GameObject.FindGameObjectWithTag("Stage_End").transform.position;

        StartGame();
    }

    void StartGame() {
        Time.timeScale = 1.0f;

        GameObject std_cam = GameObject.FindGameObjectWithTag("MainCamera");
        std_cam.SetActive(false);

        start_pos = new Vector3(start_pos.x, start_pos.y, start_pos.z);
        
        //create player character
        Instantiate(Player, start_pos, start_rot);

        std_cam.SetActive(true);
    }

    public static void StageEnd() 
    {
        Time.timeScale = 0;
        stage_end = true;
    }

    public void init_obj_position(string tag_name)
    {
        //only input tag_name, init postion to player start positon.
        GameObject tmpobj = GameObject.FindWithTag(tag_name);
        tmpobj.transform.position = start_pos;
    }   

    public void init_obj_position(string tag_name, Vector3 init_pos) 
    {
        //find object matching the tag_name, and init postion to the value of init_pos argument.
        GameObject tmpobj = GameObject.FindWithTag(tag_name);
        tmpobj.transform.position = init_pos;
    }

    void OnGUI()
    {
        if (stage_end) {
            GUILayout.BeginArea(new Rect((Screen.width/2)-75, Screen.height/2, 150, 200));

            GUILayout.Label("스테이지 클리어!");

            if(GUILayout.Button("처음으로")) {
                SceneManager.LoadScene("testgame", LoadSceneMode.Single);
                stage_end = false;
            }

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
