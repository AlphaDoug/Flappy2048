using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControll : MonoBehaviour
{
    public GameObject countDown;
    public GameObject gameOver;
    public AudioSource gameOverAudio;
    public AudioSource countDown_1;
    public AudioSource countDown_2;
    public AudioSource getNumber;
    public Text tip;
    public Text grade;
    public Text finalScore;
    private bool isCountingDown;
    private float totalTime = 0;
    private int currentGrade;
    private int second;
    void Start ()
    {
#if UNITY_ANDROID
        tip.text = "通过点击屏幕进行交互";
#elif UNITY_STANDALONE_WIN
        tip.text = "通过空格键进行交互";
#endif
        isCountingDown = true;
        currentGrade = 0;
        second = 4;
        countDown.GetComponent<Text>().text = second.ToString();
        Time.timeScale = 0;//开始时候游戏暂停，出现倒计时
    }
	
	void Update ()
    {
        if (isCountingDown)
        {
            Timer2();
        }
        
    }
    private void FixedUpdate()
    {
        currentGrade += (int)GlobalVariable.bgMoveSpeed;
        grade.text = currentGrade.ToString();
    }
    private void Timer2()
    {
        //累加每帧消耗时间
        totalTime += Time.unscaledDeltaTime;
        if (totalTime >= 1)//每过1秒执行一次
        {
            second--;
            if (second == 0)
            {
                countDown.GetComponent<Text>().text = "开始";
                countDown_2.Play();
            }
            else if(second == -1)
            {
                countDown.SetActive(false);
                Time.timeScale = 1;
                grade.enabled = true;
                isCountingDown = false;
            }
            else
            {
                countDown.GetComponent<Text>().text = second.ToString();
                countDown_1.Play();
            }          
            totalTime = 0;
        }
        
    }
    /// <summary>
    /// 创建新的节点
    /// </summary>
    public void CreatNode(int num)
    {
        getNumber.Play();
        var son = new GameObject("Player");

        son.AddComponent<Image>();
        son.AddComponent<Rigidbody2D>();
        son.AddComponent<BoxCollider2D>();
        son.AddComponent<SonNode>();
        son.AddComponent<PlayerControll>();

        son.transform.SetParent(GameObject.FindGameObjectWithTag("Nodes").transform);
        son.GetComponent<SonNode>().delayFrame = GameObject.FindGameObjectWithTag("Player").GetComponent<SonNode>().delayFrame;
        son.GetComponent<SonNode>().number = num;
        son.GetComponent<Rigidbody2D>().mass = 1;
        son.GetComponent<Rigidbody2D>().gravityScale = 650;
        son.GetComponent<Rigidbody2D>().angularDrag = 2;
        son.GetComponent<Rigidbody2D>().drag = 8;
        son.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        son.GetComponent<BoxCollider2D>().size = son.GetComponent<RectTransform>().rect.size;
        son.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (GlobalVariable.sons.Count == 0)//没有子节点时候，将子节点上的SonNode脚本上的lastNode变量赋值为Player
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<SonNode>().lastNode = son;
            GlobalVariable.sons.Add(GameObject.FindGameObjectWithTag("Player"));
        }
        else//存在子节点了,那么将末节点赋给新节点上的lastNode
        {
            GlobalVariable.sons[GlobalVariable.sons.Count - 1].GetComponent<SonNode>().lastNode = son;
        }
        GameObject.FindGameObjectWithTag("Player").name = "Son";
        GameObject.FindGameObjectWithTag("Player").transform.parent = GameObject.FindGameObjectWithTag("Sons").transform;
        GameObject.FindGameObjectWithTag("Player").tag = "Son";
        son.tag = "Player";
        GlobalVariable.sons.Add(son);
        //之前的节点上删除player控制权
        Destroy(GlobalVariable.sons[GlobalVariable.sons.Count - 2].GetComponent<Rigidbody2D>());
        Destroy(GlobalVariable.sons[GlobalVariable.sons.Count - 2].GetComponent<BoxCollider2D>());
        Destroy(GlobalVariable.sons[GlobalVariable.sons.Count - 2].GetComponent<PlayerControll>());

    }
    /// <summary>
    /// 删除尾部节点
    /// </summary>
    public void DestroyNode()
    {
        if (GlobalVariable.sons.Count > 1)
        {
            GlobalVariable.sons[GlobalVariable.sons.Count - 2].transform.position = GlobalVariable.sons[GlobalVariable.sons.Count - 1].transform.position;
            Destroy(GlobalVariable.sons[GlobalVariable.sons.Count - 1]);
            GlobalVariable.sons.RemoveAt(GlobalVariable.sons.Count - 1);

            GlobalVariable.sons[GlobalVariable.sons.Count - 1].AddComponent<Rigidbody2D>();
            GlobalVariable.sons[GlobalVariable.sons.Count - 1].AddComponent<BoxCollider2D>();
            GlobalVariable.sons[GlobalVariable.sons.Count - 1].AddComponent<PlayerControll>();

            GlobalVariable.sons[GlobalVariable.sons.Count - 1].GetComponent<Rigidbody2D>().mass = 1;
            GlobalVariable.sons[GlobalVariable.sons.Count - 1].GetComponent<Rigidbody2D>().gravityScale = 650;
            GlobalVariable.sons[GlobalVariable.sons.Count - 1].GetComponent<Rigidbody2D>().angularDrag = 2;
            GlobalVariable.sons[GlobalVariable.sons.Count - 1].GetComponent<Rigidbody2D>().drag = 8;
            GlobalVariable.sons[GlobalVariable.sons.Count - 1].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GlobalVariable.sons[GlobalVariable.sons.Count - 1].GetComponent<SonNode>().lastNode = null;

            GlobalVariable.sons[GlobalVariable.sons.Count - 1].name = "Player";
            GlobalVariable.sons[GlobalVariable.sons.Count - 1].tag = "Player";
            GlobalVariable.sons[GlobalVariable.sons.Count - 1].transform.parent = GameObject.FindGameObjectWithTag("Nodes").transform;

        }
    }
    /// <summary>
    /// 检查末尾两个节点数字是否相同，若一样则删除末尾节点并将新末尾节点数字乘二
    /// </summary>
    public void CheckNodes()
    {
        if (GlobalVariable.sons.Count < 2)
        {
            return;
        }
        //Debug.Log(GlobalVariable.sons[GlobalVariable.sons.Count - 1].GetComponent<SonNode>().number);
        //Debug.Log(GlobalVariable.sons[GlobalVariable.sons.Count - 2].GetComponent<SonNode>().number);
        if (GlobalVariable.sons[GlobalVariable.sons.Count - 1].GetComponent<SonNode>().number ==
                GlobalVariable.sons[GlobalVariable.sons.Count - 2].GetComponent<SonNode>().number)
        {
            Invoke("DestroyNode", 0.3f);
            Invoke("SetDouble", 0.4f);
            Invoke("CheckNodes", 0.5f);
        }
    }
    private void SetDouble()
    {
        GlobalVariable.sons[GlobalVariable.sons.Count - 1].GetComponent<SonNode>().SetDouble();
    }

    public void SetGameOver()
    {
        gameOverAudio.Play();
        gameOver.SetActive(true);
        Time.timeScale = 0;
        finalScore.text = currentGrade.ToString();
    }

    public void ReStart()
    {
        GlobalVariable.bgMoveSpeed = 0;
        GlobalVariable.sons.Clear();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
