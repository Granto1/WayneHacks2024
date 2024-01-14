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


public class QuizManager : MonoBehaviour
{
    public static char[] dLetters = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'};
    public static char[] upLetters = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'}; 
    // public static string[] filLetters = {'As', 'Bs', 'Cs', 'Ds', 'Es', 'Fs', 'Gs', 'Hs', 'Is','Js','Ks','Ls','Ms','Ns','Os','Ps','Qs','Rs','Ss','Ts','Us','Vs','Ws','Xs','Ys','Zs'}; 
    public static int answerIndex;
    public static int pt;
    public static TMPro.TextMeshProUGUI letterTxt, correctTxt, ptTxt;
    public static Image imageA, imageB, imageC, imageD;
    // Start is called before the first frame update
    void Start()
    {
        pt = 0;
        // Text boxes
        letterTxt = GameObject.Find("letterTxt").GetComponent<TextMeshProUGUI>();
        correctTxt = GameObject.Find("CorrectTxt").GetComponent<TextMeshProUGUI>();
        ptTxt = GameObject.Find("ptTxt").GetComponent<TextMeshProUGUI>();
        // Images
        imageA = GameObject.Find("a1").GetComponent<Image>();
        imageB = GameObject.Find("a2").GetComponent<Image>();
        imageC = GameObject.Find("a3").GetComponent<Image>();
        imageD = GameObject.Find("a4").GetComponent<Image>();

        generateQuestion();
    }

    public static void generateQuestion()
    {
        // letter values
        int[] a = {-1, -1, -1, -1};
        
        // Arraylist of possible letters
        ArrayList arlist = new ArrayList(); 
        for (int l = 0; l < 26; l++)
        {
            arlist.Add(l);
        }

        // Randomizing letters
        for (int v = 0; v < 4; v++)
        {
            int someNum = UnityEngine.Random.Range(0, 25);
            a[v] = (int)arlist[someNum];
            // Debug.Log((int)arlist[someNum]);
            arlist.Remove(someNum);
        }

        // randomizing the answer
        answerIndex = UnityEngine.Random.Range(0, 3);
        
        letterTxt.text = "" + upLetters[a[answerIndex]];
        Debug.Log(answerIndex);
        Debug.Log(upLetters[a[answerIndex]]);
        Debug.Log( dLetters[a[0]] + " " +  dLetters[a[1]] + " " +  dLetters[a[2]] + " " +  dLetters[a[3]]);
        Debug.Log(answerIndex);

        // load the sprites onto the answer choices
        imageA.sprite = Resources.Load<Sprite>( "Letters/Col/" + upLetters[a[0]] + 's');
        imageB.sprite = Resources.Load<Sprite>( "Letters/Col/" + upLetters[a[1]] + 's');
        imageC.sprite = Resources.Load<Sprite>( "Letters/Col/" + upLetters[a[2]] + 's');
        imageD.sprite = Resources.Load<Sprite>( "Letters/Col/" + upLetters[a[3]] + 's');
    }


    public static void answered(int p)
    {
        Debug.Log(p + " " + answerIndex);
        if (p == answerIndex)
        {
            Debug.Log("Balls");
            correctTxt.text = "Correct!";
            pt++;
            ptTxt.text = "" + pt;
            generateQuestion();
        }
        else
        {
            correctTxt.text = "Wrong!";
        }
    }
}
