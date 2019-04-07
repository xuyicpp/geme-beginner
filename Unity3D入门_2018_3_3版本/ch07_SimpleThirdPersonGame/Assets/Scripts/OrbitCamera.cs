using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] private Transform target;      //为环绕的对象序列化引用
    public float rotSpeed = 1.5f;

    private float _rotY;
    private float _rotX;
    private Vector3 _offset;

    public float minimumVert = -15.0f;
    public float maximumVert = 15.0f;


    // Start is called before the first frame update
    void Start()
    {
        _rotY = transform.eulerAngles.y;
        _offset = target.position - transform.position; //摄像机与目标之间的偏移
    }

    private void LateUpdate()   //确保目标移动之后摄像机才更新
    {
        //float horInput = Input.GetAxis("Horizontal");
        //if(horInput != 0)
        //{
        //   _rotY += horInput * rotSpeed;
        //}
        //else
        {
            _rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;
            _rotX += Input.GetAxis("Mouse Y") * rotSpeed * 1.5f;
            _rotX = Mathf.Clamp(_rotX, minimumVert, maximumVert);     //限制角度
        }

        Quaternion rotation = Quaternion.Euler(_rotX, _rotY, 0);
        transform.position = target.position - (rotation * _offset);
        transform.LookAt(target);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
