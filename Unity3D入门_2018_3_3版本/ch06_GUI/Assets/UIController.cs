using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;       //导入UI代码框架

public class UIController : MonoBehaviour
{
    [SerializeField] private Text scoreLabel;       //在场景中引用文本对象，设置其属性
    [SerializeField] private SettingsPopup settingsPopup;

    private int _score;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);     //申明相应时间ENEMY_HIT的方法
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);  //对象销毁，清除监听器，方出错
    }

    private void OnEnemyHit()
    {
        _score += 1;
        scoreLabel.text = _score.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        scoreLabel.text = _score.ToString();

        settingsPopup.Close();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnOpenSettings()
    {
        settingsPopup.Open();
    }
}
