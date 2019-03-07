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

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");       //屏幕中显示文本
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
                GameObject hitObject = hit.transform.gameObject;      //获取射线击中的对象
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

                if (target != null)
                {
                    target.ReactToHit();
                }
                else
                {
                    StartCoroutine(SphereIndicator(hit.point));
                }
            }
        }
    }

    private IEnumerator SphereIndicator(Vector3 pos)        //协程使用IEnumerator方法
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;

        yield return new WaitForSeconds(1); //yield关键字告诉协程在何处暂停

        Destroy(sphere);        //移除GameObject并清除它占用的内存
    }
}
