using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Windows;
using System.Numerics;
using System.Drawing;
using UnityEngine.UI;
using TMPro;

public class ImageUpdate : MonoBehaviour
{
    public static char[] upLetters = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};
    public static char[] dLetters = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'};
    public static Image imageA;
    [SerializeField] 
        public static TMPro.TextMeshProUGUI lett;

    public static void updatePic(int spot)
    {
        imageA = GameObject.Find("handimg").GetComponent<Image>();
        imageA.sprite  = Resources.Load<Sprite>( "Letters/Col/" + upLetters[spot] + 's');
        Debug.Log("Letters/BW/" + dLetters[spot]);
    }
    public static void updateTxt(int spot)
    {
        lett = GameObject.Find("lett").GetComponent<TextMeshProUGUI>();
        lett.text = "" + upLetters[spot];
    }
}
