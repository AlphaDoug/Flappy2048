using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    private GameObject audioJump;
    private Rigidbody2D mainPlayerRigidbody2D;
	// Use this for initialization
	void Awake ()
    {
        mainPlayerRigidbody2D = GetComponent<Rigidbody2D>();
        audioJump = GameObject.Find("Jump");
	}
	
	// Update is called once per frame
	void Update () 
    {
#if UNITY_ANDROID
        if (Input.GetMouseButtonDown(0))
        {
            audioJump.GetComponent<AudioSource>().Play();
            mainPlayerRigidbody2D.AddForce(new Vector2(0, 130000));
        }
#elif UNITY_STANDALONE_WIN
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioJump.GetComponent<AudioSource>().Play();
            mainPlayerRigidbody2D.AddForce(new Vector2(0, 130000));
        }
#endif
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("撞上障碍物");
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControll>().SetGameOver();
        }
        if (collision.gameObject.tag == "Number")
        {
            
            Debug.Log("撞上数字");
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControll>().CreatNode(collision.gameObject.GetComponent<Number>().num);
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControll>().CheckNodes();
            Destroy(collision.gameObject);
        }
    }

}

