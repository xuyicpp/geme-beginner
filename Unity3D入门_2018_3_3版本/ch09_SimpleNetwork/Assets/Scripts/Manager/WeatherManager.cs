using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON;

public class WeatherManager : MonoBehaviour,IGameManager
{
    public ManagerStatus status { get; private set; }

    public float cloudValue { get; private set; }

    //add cloud value here
    private NetworkService _network;

    public void Startup(NetworkService serivece)
    {
        Debug.Log("Weather manager starting...");

        _network = serivece;
        StartCoroutine(_network.GetWeatherJSON(OnJSONDataLoaded));  //网络请求

        status = ManagerStatus.Initializing;
    }

    public void OnJSONDataLoaded(string data)
    {
        Dictionary<string, object> dict;
        dict = Json.Deserialize(data) as Dictionary<string, object>;

        Dictionary<string,object> clouds = (Dictionary<string,object>)dict["clouds"];
        cloudValue = (long)clouds["all"] / 100f;

        Debug.Log("Value: " + cloudValue);

        Messenger.Broadcast(GameEvent.WEATHER_UPDATED);

        status = ManagerStatus.Started;
    }
}
