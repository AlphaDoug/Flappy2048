using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour 
{
    public GameObject gameStartObstacles;
    public List<GameObject> obstacles = new List<GameObject>();
    public Transform creatPosition;
    public float maxSpeed = 15.0f;
    public float initialMoveSpeed = 5.0f;
    private List<float> startPosition = new List<float>();
	// Use this for initialization
	void Start () 
    {
        GlobalVariable.bgMoveSpeed = initialMoveSpeed;
        for (int i = 0; i < obstacles.Count; i++)
        {
            obstacles[i].transform.position += new Vector3(0, Random.Range(-250, 250), 0);
        }
        InvokeRepeating("SpeedUp", 5.0f, 5.0f);
        
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            obstacles[i].transform.Translate(new Vector3(-GlobalVariable.bgMoveSpeed, 0, 0));
        }
        if (obstacles[0].transform.position.x < -50)
        {
            var buff = obstacles[0];
            buff.SetActive(true);
            obstacles.RemoveAt(0);
            buff.transform.position = new Vector3(0, Random.Range(-300, 300), 0) + creatPosition.position;
            obstacles.Add(buff);
        }

	}
    private void SpeedUp()
    {
        if (GlobalVariable.bgMoveSpeed >= maxSpeed && IsInvoking("SpeedUp"))
        {
            CancelInvoke("SpeedUp");
        }
        GlobalVariable.bgMoveSpeed += 0.5f;
    }
}
