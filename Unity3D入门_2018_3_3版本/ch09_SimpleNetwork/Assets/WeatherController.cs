using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    [SerializeField] private Material sky;  //引用对象
    [SerializeField] private Light sun;

    private float _fullIntensity;
    private float _cloudValue = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _fullIntensity = sun.intensity;     //初始灯光最开始为满强度    
    }

    // Update is called once per frame
    void Update()
    {
        SetOvercast(_cloudValue);
        if(_cloudValue < 1.0f)
            _cloudValue += 0.005f;
    }

    public void SetOvercast(float value)    //同时调整材质的Blend值和灯光强度
    {
        sky.SetFloat("_Blend", value);
        sun.intensity = _fullIntensity - (_fullIntensity * value);
    }
}
