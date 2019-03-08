using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;       //引用一个用于通知单击的目标对象
    [SerializeField] private string targetMessage;
    public Color highlightColor = Color.cyan;

    public void OnMouseExit()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if(sprite != null)
        {
            sprite.color = Color.white;     
        }
    }

    public void OnMouseEnter()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if(sprite != null)
        {
            sprite.color = highlightColor;
        }
    }

    public void OnMouseDown()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);       //单击，变大
    }

    public void OnMouseUp()
    {
        transform.localScale = Vector3.one;
        if(targetObject != null)
        {
            targetObject.SendMessage(targetMessage);    //单击，将消息发送到目标对象
        }
    }
}
