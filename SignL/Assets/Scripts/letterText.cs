using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Linq;
using TMPro;

public class letterText : MonoBehaviour
{
    public static TMP_Text changingText;
    void Start()
    {
        changingText = GetComponent<TextMeshProUGUI>();
        //Debug.Log(changingText);

      /*  StartCoroutine(callText());*/
    }
    public static void predictText(Char str) {

        int asciiCode = (int)(Convert.ToChar(str));
        if (asciiCode == 0)
        {
            changingText.text = "..";
        }
        else
        {
            changingText.text = str.ToString();
        }
    }
}
