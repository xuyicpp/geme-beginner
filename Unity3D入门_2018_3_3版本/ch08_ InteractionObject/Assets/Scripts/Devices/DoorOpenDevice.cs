using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenDevice : MonoBehaviour
{
    [SerializeField] private Vector3 dPos;      //当打开门时要位移到的位置

    private bool _open;         //布尔参数追踪门的状态

    public void Operate()
    {
        if (_open)
        {
            Vector3 pos = transform.position - dPos;
            transform.position = pos;
        }
        else
        {
            Vector3 pos = transform.position + dPos;
            transform.position = pos;
        }

        _open = !_open;
    }

    public void Activate()
    {
        if (!_open)
        {
            Vector3 pos = transform.position + dPos;
            transform.position = pos;
        }
        _open = true;
    }

    public void Deactivate()
    {
        if (_open)
        {
            Vector3 pos = transform.position - dPos;
            transform.position = pos;
        }
        _open = false;
    }
}
