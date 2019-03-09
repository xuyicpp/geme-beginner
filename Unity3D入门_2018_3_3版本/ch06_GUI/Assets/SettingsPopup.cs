using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPopup : MonoBehaviour
{
    public void Open()
    {
        gameObject.SetActive(true);     //开启对象打开窗口
    }

    public void Close()
    {
        gameObject.SetActive(false);    //使对象无效，关闭窗口
    }

    public void OnSubmitName(string name)
    {
        Debug.Log(name);
    }

    public void OnSpeedValue(float speed)
    {
        Messenger<float>.Broadcast(GameEvent.SPEED_CHANGED, speed);     //把滑动条的值作为<float>事件发送
    }
}
