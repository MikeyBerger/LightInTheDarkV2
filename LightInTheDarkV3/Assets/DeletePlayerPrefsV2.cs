using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePlayerPrefsV2 : MonoBehaviour
{
    private CursorController CC;
    private SpriteRenderer SR;

    // Start is called before the first frame update
    void Start()
    {
        CC = GameObject.FindGameObjectWithTag("Cursor").GetComponent<CursorController>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (CC.IsPressed && collision.gameObject.tag == "Cursor")
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
