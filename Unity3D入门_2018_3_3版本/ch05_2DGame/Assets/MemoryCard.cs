using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] private GameObject cardBack;
    //[SerializeField] private Sprite image;      //引用将加载的精灵资源
    [SerializeField] private SceneController controller;

    private int _id;

    public int id
    {
        get { return _id; } //添加getter函数
    }

    public void SetCard(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    public void OnMouseDown()
    {
        if (cardBack.activeSelf && controller.canRealed)
        {
            cardBack.SetActive(false);
            controller.CardRevealed(this);      //当卡片显示时通知controller
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       //GetComponent<SpriteRenderer>().sprite = image;      //设置这个SpriteRender组件的属性
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Unreveal()      //一个公有的方法，因此SceneController可以再次隐藏卡片
    {
        cardBack.SetActive(true);
    }
}
