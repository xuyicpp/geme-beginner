using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    [SerializeField] private Material sky;  //引用对象
    [SerializeField] private Light sun;

    private float _fullIntensity;
    private float _cloudValue = 0f;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.WEATHER_UPDATED, OnWeatherUpdated);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.WEATHER_UPDATED, OnWeatherUpdated);
    }

    private void OnWeatherUpdated()
    {
        SetOvercast(Managers.Weather.cloudValue-0.1f);
    }

    // Start is called before the first frame update
    void Start()
    {
        _fullIntensity = sun.intensity;     //初始灯光最开始为满强度    
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetOvercast(float value)    //同时调整材质的Blend值和灯光强度
    {
        sky.SetFloat("_Blend", value);
        sun.intensity = _fullIntensity - (_fullIntensity * value);
    }
}
