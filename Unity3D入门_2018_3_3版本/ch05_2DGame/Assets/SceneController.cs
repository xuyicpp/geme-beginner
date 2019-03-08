using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public const int gridRows = 2;
    public const int gridCols = 4;
    public const float offsetX = 2.0f;
    public const float offsetY = 2.5f;

    private int _score = 0;     //添加到SceneController项部

    private MemoryCard _firstRealed;
    private MemoryCard _secondRealed;

    [SerializeField] private MemoryCard originalCard;   //用于引用场景中的卡片
    [SerializeField] private Sprite[] images;           //引用精灵资源的数组
    [SerializeField] private TextMesh scoreLabel;
    public bool canRealed
    {
        get { return _secondRealed == null; }       //当存在第二张翻开的卡片,getter方法返回false
    }

    public void CardRevealed(MemoryCard card)
    {
        //initially empty
        if(_firstRealed == null)
        {
            _firstRealed = card;
        }
        else
        {
            _secondRealed = card;

            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        if(_firstRealed.id == _secondRealed.id)
        {
            _score++;
            scoreLabel.text = "Score: " + _score;       //text显示设置在文本对象上的属性
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            _firstRealed.Unreveal();
            _secondRealed.Unreveal();
        }

        _firstRealed = null;
        _secondRealed = null;
    }


    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPos = originalCard.transform.position;     //第一张卡片的位置；

        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };     //使用ID对所有四中卡片精灵声明一个整形数组
       
        numbers = ShuffleArray(numbers);

        for(int i = 0; i < gridCols; ++i)
        {
            for(int j = 0; j < gridRows; ++j)
            {
                MemoryCard card;
                if (i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MemoryCard;
                }

                int index = j * gridCols + i;
                //int id = Random.Range(0, images.Length);
                int id = numbers[index];
                card.SetCard(id, images[id]);       //调用添加到MemoryCard中的公有方法

                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for(int i = 0; i < newArray.Length; ++i)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    public void Restart()
    {
       // Application.LoadLevel("SampleScene");
        //SceneManager.LoadScence("Scene");
        //LoadScene();
        SceneManager.LoadScene("SampleScene");

    }
}
