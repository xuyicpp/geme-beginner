using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }
    public RotationAxes axes = RotationAxes.MouseXAndY;

    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;

    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    private float _rotationX = 0;   //垂直私有变量

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
            body.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(axes == RotationAxes.MouseX)
        {
            //horizontal rotation here
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
        }
        else if(axes == RotationAxes.MouseY)
        {
            //vertiacl rotation here
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;   //基于鼠标增加垂直角度
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);     //限制角度
            float rotationY = transform.localEulerAngles.y;     //保持Y的角度一样

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
        else
        {
            //both horizontal and vertical rotation here
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;   //基于鼠标增加垂直角度
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);     //限制角度

            float delta = Input.GetAxis("Mouse X") * sensitivityHor;
            float rotationY = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
    }
}
