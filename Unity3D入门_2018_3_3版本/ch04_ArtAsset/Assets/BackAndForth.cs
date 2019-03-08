using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForth : MonoBehaviour
{
    public float speed = 3.0f;
    public float maxZ = 6.0f;
    public float minZ = -6.0f;     //这些是对象移动的范围

    private int _direction = 1;     //当前对象往哪个方向移动

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, _direction * speed * Time.deltaTime);
        bool bounced = false;

        if(transform.position.z > maxZ || transform.position.z < minZ)
        {
            _direction = -_direction;
            bounced = true;
        }

        if(bounced)
        {
            transform.Translate(0, 0, _direction * speed * Time.deltaTime);
        }
    }
}
