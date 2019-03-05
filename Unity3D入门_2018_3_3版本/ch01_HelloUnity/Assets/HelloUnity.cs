//System.Collections 命名空间包含接口和类，这些接口和类定义各种对象（如列表、队列、位数组、哈希表和字典）的集合。
using System.Collections;  
//System.Collections.Generic 命名空间包含定义泛型集合的接口和类，泛型集合允许用户创建强类型集合，它能提供比非泛型强类型集合更好的类型安全性和性能。
using System.Collections.Generic;
//导入unity引擎程序的命名空间
using UnityEngine;

public class HelloUnity : MonoBehaviour //继承unity脚本的基类
{
    // Start is called before the first frame update
    void Start()    //游戏开始的时候执行
    {
        Debug.Log("Hello Unity");
    }

    // Update is called once per frame
    void Update()   //游戏每帧运行的代码
    {
        
    }
}