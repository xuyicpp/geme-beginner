using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    public float speed = 3.0f;      //移动速度
    public float obstacleRange = 5.0f;  //对墙壁做出反应的距离

    public const float baseSpeed = 3.0f;      //移动速度

    [SerializeField] private GameObject fireballPrefab;
    private GameObject _fireball;

    private bool _alive;

    // Start is called before the first frame update
    void Start()
    {
        _alive = true;
    }

    private void OnSpeedChanged(float value)
    {
        speed = baseSpeed * value;
    }

    private void Awake()
    {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    private void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    // Update is called once per frame
    void Update()
    {
        if (_alive)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);  //持续向前

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;     //很大的横切面的射线
            if (Physics.SphereCast(ray, 0.75f, out hit))   //这个半径参数绝地在射线多远进行碰撞检测
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    if(_fireball == null)
                    {
                        _fireball = Instantiate(fireballPrefab) as GameObject;
                        _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);    //将火球放置在敌人前面并指向同一方向
                        _fireball.transform.rotation = transform.rotation;
                    }
                }
                else if (hit.distance < obstacleRange)
                {
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }
        }

    }

    public void SetAlive(bool alive)
    {
        _alive = alive;
    }
}
