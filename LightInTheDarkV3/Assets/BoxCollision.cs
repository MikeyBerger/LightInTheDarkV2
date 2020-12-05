using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxCollision : MonoBehaviour
{
    public bool HasCollided;
    private SceneManaging SM;
    public int Index;
    private CursorController CC;

    // Start is called before the first frame update
    void Start()
    {
        CC = GameObject.FindGameObjectWithTag("Cursor").GetComponent<CursorController>();
        SM = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneManaging>();
        //Index = PlayerPrefs.GetInt("levelAt");
    }

    // Update is called once per frame
    void Update()
    {
        if (HasCollided)
        {
            SM.ChangeScene(Index);
            //SceneManager.LoadScene(Index);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Cursor" && CC.IsPressed)
        {
            HasCollided = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cursor" && CC.IsPressed)
        {
            HasCollided = true;
        }
    }
}
