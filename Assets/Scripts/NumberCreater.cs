using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberCreater : MonoBehaviour
{

    public Transform creatPosition;
    private int[] allNumbers = { 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048 };
    // Use this for initialization
    void Start ()
    {
        InvokeRepeating("Creat",0f, 3.0f);
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        var num = GameObject.FindGameObjectsWithTag("Number");
        if (num.Length != 0)
        {
            for (int i = 0; i < num.Length; i++)
            {
                num[i].transform.Translate(new Vector3(-GlobalVariable.bgMoveSpeed, 0, 0));
            }          
        }

    }
    void Creat()
    {
        if (GameObject.FindGameObjectWithTag("Numbers").transform.childCount >= 1)//场景中存在了数字，那么直接返回不创建
        {
            return;
        }
        var number = new GameObject("number");
        number.AddComponent<Image>();
        number.AddComponent<Number>();
        //number.AddComponent<Rigidbody2D>();
        number.AddComponent<BoxCollider2D>();

        number.layer = LayerMask.NameToLayer("UI");
        number.transform.parent = GameObject.FindGameObjectWithTag("Numbers").transform;
        number.tag = "Number";
        number.transform.position = creatPosition.position;
        number.GetComponent<BoxCollider2D>().size = number.GetComponent<RectTransform>().rect.size;
        //number.GetComponent<Rigidbody2D>().simulated = false;
        if (GlobalVariable.sons.Count == 0)
        {
            number.GetComponent<Number>().num = 2;
        }
        else
        {
            for (int i = 0; i < allNumbers.Length; i++)
            {
                if (GlobalVariable.sons[GlobalVariable.sons.Count - 1].GetComponent<SonNode>().number == allNumbers[i])
                {
                    number.GetComponent<Number>().num = allNumbers[Random.Range(0, i + 1)];
                    break;
                }
            }
        }
       

    }
}
