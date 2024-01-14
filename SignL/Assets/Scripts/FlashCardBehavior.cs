using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Windows;
using System.Numerics;
using System.Drawing;
using UnityEngine.UI;

public class FlashCardBehavior : MonoBehaviour
{
    public int currentIndex = 0;

    // Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    public void Previous()
    {
        Debug.Log(currentIndex);
        currentIndex--;
        if (currentIndex == -1)
        {
            currentIndex = 25;
        }
        Debug.Log(currentIndex);
        ImageUpdate.updatePic(currentIndex);
        ImageUpdate.updateTxt(currentIndex);
    }
    
    public void Next()
    {
        currentIndex++;
        if (currentIndex == 26)
        {
            currentIndex = 0;
        }
        Debug.Log(currentIndex);
        ImageUpdate.updatePic(currentIndex);
        ImageUpdate.updateTxt(currentIndex);
    }
}
