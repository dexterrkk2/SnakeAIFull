using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SnakeMaker : MonoBehaviour
{
    public static SnakeMaker instance;
    public Snake snakeHead;
    public GameObject applePrefab;
    public Vector3 randPos;
    public float bounds;
    public int score;
    public float radius;
    public Text text;
    public List<GameObject> apples;
    public List<GameObject> snakes;
    // Start is called before the first frame update
    void Start()
    {
        AppleSpawn();
        instance = this;
    }
    public void AppleSpawn()
    {
        Randomize();
	    GameObject apple =Instantiate(applePrefab, randPos, Quaternion.identity);
        AppleDeletion(apple);
        snakeHead.avoidance.myTarget = apple;
    }
    public void Randomize()
    {
        float randX = Random.Range(0, bounds);
        float randZ = Random.Range(0, bounds);
        randPos = new Vector3(randX, 1, randZ);
    }
    private void FixedUpdate()
    {
        if((snakeHead.transform.position - apples[0].transform.position).magnitude < radius)
        {
            AppleSpawn();

        }
    }
    void AppleDeletion(GameObject apple)
    {
        if (apples.Count > 0)
        {
            GameObject delete = apples[0];
            apples.RemoveAt(0);
            Destroy(delete);
            score++;
            text.text = "Apples Collected " + (score);
            Randomize();
            snakeHead.SpawnSnake();
        }
        apples.Add(apple);
    }
    public void GameOver()
    {
        Debug.Log("GameOver");
        SceneManager.LoadScene(0);
    }
}
