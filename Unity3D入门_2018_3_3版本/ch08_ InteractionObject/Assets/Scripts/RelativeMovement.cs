using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//放置RequireComponent()方法的环境
[RequireComponent(typeof(CharacterController))]

public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform target;      //引用相对移动的对象
    public float rotSpeed = 10.0f;
    public float moveSpeed = 6.0f;

    private Animator _animator;

    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;
    private float _vertSpeed;

    public float pushForce = 3.0f;      //要应用的力量值

    private ControllerColliderHit _contact;     //需要在函数之间存储碰撞数据

    private CharacterController _charController;

    private const float EPSILON = 0.000001f;
    // Start is called before the first frame update
    void Start()
    {
        _charController = GetComponent<CharacterController>();
        _vertSpeed = minFall;       //初始化为最小的下落速度
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.zero;        //从向量(0,0,0)开始并逐步添加移动组件

        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        if (System.Math.Abs(horInput) > EPSILON || System.Math.Abs(vertInput) > EPSILON)
        {
            movement.x = horInput * moveSpeed;
            movement.z = vertInput * moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed);

            Quaternion tmp = target.rotation;       //保存初始旋转，以便在处理完目标对象后还原旋转

            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);   //只保留摄像头坐标的y旋转

            movement = target.TransformDirection(movement);     //将世界坐标系转化为摄像头坐标
            target.rotation = tmp;      //恢复摄像头的朝向坐标

            //transform.rotation = Quaternion.LookRotation(movement);       
            Quaternion direction = Quaternion.LookRotation(movement);
            //lerp线性插值
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
        }
        // raycast down to address steep slopes and dropoff edge
        bool hitGround = false;
        RaycastHit hit;
        if (_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            //float check = (_charController.height + _charController.radius) / 1.9f;
            float check = (_charController.height/2 + _charController.radius) +0.15f;
            hitGround = hit.distance <= check;  // to be sure check slightly beyond bottom of capsule
        }
        _animator.SetFloat("Speed", movement.sqrMagnitude);

        if (hitGround)    //控制器是否在地面上,检查光线投射结果，代替isGround检查
        {
            if(Input.GetButtonDown("Jump"))     //当在地面时相应Jump按钮
            {
                _vertSpeed = jumpSpeed;
            }
            else
            {
                _vertSpeed = minFall;       //需要一个向下的力，才可以在凹凸不平的地形上行走
                _animator.SetBool("Jumping", false);
            }
        }
        else
        {
            _vertSpeed += gravity * 5 * Time.deltaTime;
            if(_vertSpeed < terminalVelocity)
            {
                _vertSpeed = terminalVelocity;
            }

            if(_contact != null)        //不要再关卡的开始处触发这个值
            {
                _animator.SetBool("Jumping", true);
            }

            // 根据角色是否是否面向接触点，相应稍微不同
            if (_charController.isGrounded)
            {
                if (Vector3.Dot(movement, _contact.normal) < 0)
                {
                    movement = _contact.normal * moveSpeed;
                }
                else
                {
                    movement += _contact.normal * moveSpeed;
                }
            }
        }

        movement.y = _vertSpeed;

        movement *= Time.deltaTime;
        _charController.Move(movement);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _contact = hit;

        Rigidbody body = hit.collider.attachedRigidbody;        //检查碰撞对象是否有Rig
        if(body != null && !body.isKinematic)   //是否使用运动学
        {
            body.velocity = hit.moveDirection * pushForce;      //将速度应用到物理对象上
        }
    }
}
