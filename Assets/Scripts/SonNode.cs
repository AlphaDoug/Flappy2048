using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SonNode : MonoBehaviour
{
    public GameObject lastNode;
    public int delayFrame = 10;
    public List<Vector2> thisPosition = new List<Vector2>();
    public int number;
    private int frame;
    private int lastNum;
    private Image image;
    // Use this for initialization
    private void Awake()
    {
        frame = 1;
        number = 2;
        image = GetComponent<Image>();
        image.overrideSprite = Resources.Load("Textures/2", typeof(Sprite)) as Sprite;
        lastNum = number;
    }
    void Start ()
    {
        thisPosition.Add(transform.position);
        if (lastNum != number)//此节点上的数字发生改变
        {
            image.overrideSprite = Resources.Load("Textures/" + number.ToString(), typeof(Sprite)) as Sprite;
        }
    }
	
	void Update ()
    {
        frame++;
        if (lastNum != number)//此节点上的数字发生改变
        {
            image.overrideSprite = Resources.Load("Textures/" + number.ToString(), typeof(Sprite)) as Sprite;
        }
        if (frame <= delayFrame)
        {
            thisPosition.Add(transform.position);
        }
        else
        {
            thisPosition.RemoveAt(0);
            thisPosition.Add(transform.position);
        }
        if (lastNode != null)
        {
            if (lastNode.GetComponent<SonNode>().thisPosition.Count == delayFrame)
            {
                transform.position = lastNode.GetComponent<SonNode>().thisPosition[0] + new Vector2(-100, 0);
            }            
        }
        
	}

    public void SetDouble()
    {
        number *= 2;
    }

    public void SetHalf()
    {
        number /= 2;
    }
}
