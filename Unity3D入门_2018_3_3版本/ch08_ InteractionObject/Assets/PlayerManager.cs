using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour,IGameManager
{
    public ManagerStatus status { get; private set; }

    public int health { get; private set; }
    public int maxHealth { get; private set; }

    public void Startup()
    {
        Debug.Log("Player manager starting...");

        health = 50;
        maxHealth = 100;        //可以使用保存的数据来初始化这些值

        status = ManagerStatus.Started;
    }

    public void ChangeHealth(int value)     //其他脚本不能直接设置血量但是可以调用这个方法
    {
        health += value;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        else if(health < 0)
        {
            health = 0;
        }

        Debug.Log("health: " + health + "/" + maxHealth);
    }
}
