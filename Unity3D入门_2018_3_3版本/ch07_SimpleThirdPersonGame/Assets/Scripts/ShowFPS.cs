using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFPS : MonoBehaviour
{
    public float updateInterval = 0.5f;
    private float accum = 0;
    private int frames = 0;
    private float timeleft;
    private string stringFps;

    // Start is called before the first frame update
    void Start()
    {
       Application.targetFrameRate=60;      //关闭了垂直同步project setting
        timeleft = updateInterval;
    }

    // Update is called once per frame
    void Update()
    {
        timeleft -= Time.deltaTime;     //变量deltaTime表示为unity本地变量
        accum += Time.timeScale / Time.deltaTime;
        //Debug.Log(Time.timeScale); 默认为一，时间的缩放。这可以用于减慢运动效果。
        //当timeScale传递时间1.0时和实时时间一样快。当timeScale传递时间0.5时比实时时间慢一半。
        ++frames;
        if(timeleft <= 0.0)
        {
            float fps = accum / frames;     //每0.5s 每一帧的FPS总和/帧数
            string format = System.String.Format("{0:F2} FPS", fps);
            stringFps = format;
            timeleft = updateInterval;
            accum = 0.0F;
            frames = 0;
        }

    }

    private void OnGUI()
    {
        GUIStyle guiStyle = GUIStyle.none;
        guiStyle.fontSize = 30;
        guiStyle.normal.textColor = Color.red;
        guiStyle.alignment = TextAnchor.UpperLeft;
        Rect rt = new Rect(40, 0, 100, 100);
        GUI.Label(rt, stringFps, guiStyle);
    }
}
