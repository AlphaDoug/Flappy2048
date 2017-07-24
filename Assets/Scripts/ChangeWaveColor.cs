using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWaveColor : MonoBehaviour
{
    public Material material;
    private float curTime = 0;
    public float colorChangeRate = 2;
    private bool b2w = true;
    Color color;

    // Use this for initialization
    void Start()
    {
        curTime = Time.time;

        //material.SetColor("_Color", new Color(Random.Range(0.5f, 1), Random.Range(0.5f, 1), Random.Range(0.5f, 1), Random.Range(0.5f, 1)));
    }

    //Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time - curTime > colorChangeRate)
        {
            curTime = Time.time;
            //material.color=new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            //material.SetColor("_Color", new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
            color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            b2w = !b2w;
           
        }
        if (b2w)
        {
            material.color = Color.Lerp(material.color, color, 0.01f);
        }
        


    }
    }