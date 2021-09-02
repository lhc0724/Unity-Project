using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Triggers : MonoBehaviour
{
    public GameObject Player;

    Vector3 start_pos;
    Vector3 end_pos;
    Quaternion start_rot;
    bool flag_start;

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

        start_pos = new Vector3(start_pos.x, start_pos.y+1f, start_pos.z);

        //Debug.Log("postion" + start_pos);

        //create player character
        Instantiate(Player, start_pos, start_rot);

        std_cam.SetActive(true);
    }
}
