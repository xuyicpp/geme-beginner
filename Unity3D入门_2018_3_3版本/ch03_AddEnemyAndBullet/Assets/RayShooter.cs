using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();       //访问相同对象上附加的其他组件
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);    //屏幕中心是宽高的一半
            Ray ray = _camera.ScreenPointToRay(point);  //在摄像机所在位置创建射线
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))       //Raycast 给引用的变量填充信息
            {
                Debug.Log("Hit " + hit.point);
            }
        }
    }
}
