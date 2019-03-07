using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    //通过设计脚本调用的方法
    public void ReactToHit()    
    {
        WanderingAI behavior = GetComponent<WanderingAI>();
        if(behavior != null)
        {
            behavior.SetAlive(false);
        }
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        this.transform.Rotate(-75, 0, 0);
        yield return new WaitForSeconds(1.5f);

        Destroy(this.gameObject);   //对象能销毁自己，就像一个分开独立的对象
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
