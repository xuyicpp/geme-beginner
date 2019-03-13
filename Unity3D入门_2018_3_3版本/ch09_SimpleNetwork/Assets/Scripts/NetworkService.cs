using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NetworkService
{
    private const string jsonApi = "https://samples.openweathermap.org/data/2.5/weather?q=London&appid=b6907d289e10d714a6e88b30761fae22";

    private bool IsResponseValid(WWW www)   //在响应中检查错误
    {
        if(!string.IsNullOrEmpty(www.error))
        {
            Debug.Log("bad connection");
            return false;
        }
        else if(string.IsNullOrEmpty(www.text))
        {
            Debug.Log("bad data");
            return false;
        }
        else
        {   //all good
            return true;
        }
    }

    private IEnumerator CallAPI(string url, Action<string> callback)
    {
        WWW www = new WWW(url);     //通过创建WWW对象发送HTTP请求
        yield return www;   //下载时暂停

        if (!IsResponseValid(www))
            yield break;

        callback(www.text);     //可以像原始函数一样调用委托
    }

    public IEnumerator GetWeatherJSON(Action<string> callback)
    {
        return CallAPI(jsonApi, callback);
    }
}
