using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionV2 : MonoBehaviour
{
    public GameObject[] Buttons;

    // Start is called before the first frame update
    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 2); /* < Change this int value to whatever your
                                                             level selection build index is on your
                                                             build settings */

        for (int i = 0; i < Buttons.Length; i++)
        {
            if (i + 2 > levelAt)
#pragma warning disable CS0618 // Type or member is obsolete
                Buttons[i].active = false;
#pragma warning restore CS0618 // Type or member is obsolete
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
