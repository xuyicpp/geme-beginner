using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceOperator : MonoBehaviour
{
    public float radius = 1.5f; //玩家激活设施的距离

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Fire3");
        if (Input.GetButtonDown("Fire3"))    //相应Unity输入设置中定义的输入按钮，右cmd
        {
            //Debug.Log("Fire3");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);    //返回一个附近对象的列表
            foreach(Collider hitCollider in hitColliders)
            {
                Vector3 direction = hitCollider.transform.position - transform.position;
                if(Vector3.Dot(transform.forward,direction) > 0.5f)     //计算朝向
                {
                    hitCollider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver); 
                    //尝试调用指定的函数不管目标对象的类型
                }

            }
        }
    }
}
