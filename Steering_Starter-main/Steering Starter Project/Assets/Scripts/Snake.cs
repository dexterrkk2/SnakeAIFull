using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public Snake snakeScript;
    public Kinematic avoidance;
    public Vector3 offset;
    public float slowdown;
    // Start is called before the first frame update
    private void Start()
    {
        //snakeScript.avoidance.maxSpeed = avoidance.maxSpeed;
    }
    public void SpawnSnake()
    {
        snakeScript.offset = offset;
        snakeScript.avoidance.myTarget = gameObject;
        snakeScript.avoidance.maxSpeed = snakeScript.avoidance.maxSpeed / slowdown;
        Vector3 position = new Vector3(transform.position.x + offset.x, 1, transform.position.z + offset.z);
        GameObject snakeBody = Instantiate(snakeScript.gameObject, position, Quaternion.identity);
    }
    public void Update()
    {
        //if(this.gameObject.transform.position.y < 0)
        //{
            //SnakeMaker.instance.GameOver();
        //}
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colided");
        if (collision.gameObject.tag == "Player")
        {
            SnakeMaker.instance.GameOver();
        }
    }
}
