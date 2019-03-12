using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour,IGameManager
{
    public ManagerStatus status { get; private set; }   //属性可以从任何地方获取，但只能在这个脚本设置
    public string equippedItem { get; private set; }
    private Dictionary<string,int> _items;

    public void Startup()
    {
        Debug.Log("Inventory manager starting...");
        _items = new Dictionary<string, int>();

        status = ManagerStatus.Started;     //如果是长任务，状态改变为"Initialinzing"
    }

    private void DisplayItem()      //打印当前仓库的控制台消息
    {
        string itemDisplay = "Items: ";
        foreach (KeyValuePair<string, int> item in _items)
        {
            itemDisplay += item.Key + "("+item.Value+") ";
        }
        Debug.Log(itemDisplay);
    }

    public void AddItem(string name)
    {
        if(_items.ContainsKey(name))
        {
            _items[name] += 1;
        }
        else
        {
            _items[name] = 1;
        }
        DisplayItem();
    }

    public List<string> GetItemList()
    {
        List<string> list = new List<string>(_items.Keys);  //返回所有Dictionary键的列表
        return list;
    }

    public int GetItemCount(string name)    //返回仓库中物品的个数
    {
        if(_items.ContainsKey(name))
        {
            return _items[name];
        }
        return 0;
    }

    public bool EquipItem(string name)
    {
        if(_items.ContainsKey(name) && equippedItem != name)    //检查仓库有，并未装备
        {
            equippedItem = name;
            Debug.Log("Equipped " + name);
            return true;
        }

        equippedItem = null;
        Debug.Log("Unequipped");
        return false;
    }

    public bool ConsumeItem(string name)
    {
        if (_items.ContainsKey(name))   //检查是否在仓库
        {
            _items[name]--;
            if (_items[name] == 0)       //为0，移除
            {
                _items.Remove(name);
            }
        }
        else
        {
            Debug.Log("cantconsume " + name);
            return false;
        }

        DisplayItem();
        return true;

    }
}
