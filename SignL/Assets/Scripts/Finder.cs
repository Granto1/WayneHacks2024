using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp.Demo;
using OpenCvSharp;
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
using OpenCvSharp;


public class Finder : WebCamera
{
    [SerializeField] private float Threshold = 96.4f;
    // [SerializeField] private bool ShowProcessingImage = true;

    private Mat image;
    private Mat processImage = new Mat();
    private HierarchyIndex[] hierarchy;
    static Texture2D tex1;
    static Texture2D tex2;
    static Texture2D tex3;
    static Texture2D tex4;            //BACKGROUND MOG2
    static Texture2D tex5;            //BACKGROUND MOG2

    static Mat frame = new Mat();
    Mat canny = new Mat();
    Mat threshold_output = new Mat();
    Mat threshold_Load = new Mat();
    Mat fgMaskMOG2 = new Mat();       // output for backgroundSUBMOG2
    Mat img1 = new Mat();
    Mat img2 = new Mat();
    public static char[] upLetters = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'}; 


    int DIFF_THRESH = 230;
    int Diff_MAX = 360;
    double THRESH = 200;
    public static int maxIndex = 0;
    int MAX_LETTERS = 26;

    int frames = 0;   // frames varaible to count how many frames processed
    int SAMPLE_RATE = 1;

    OpenCvSharp.Point[][] letters = new OpenCvSharp.Point[26][];

    /* OpenCvSharp.Point[][] letters;*/
    OpenCvSharp.Point[][] feature_image;
    OpenCvSharp.Point[][] contours;

    OpenCvSharp.HierarchyIndex[] hierarchy1;

    //Creates MOG2 Background Subtractor.
    BackgroundSubtractorMOG2 ptrBackgroundMOG2 = BackgroundSubtractorMOG2.Create(10000, 200, false);

    public UnityEngine.Object[] buffer;


    static List<char> aslList = new List<char>();

    static char asl_letter;             // Changed here 7/29/21          

    public static Image imageA;
    public static TMPro.TextMeshProUGUI letterTxt;

    protected override bool ProcessTexture(WebCamTexture input, ref Texture2D output)
    {
        image = OpenCvSharp.Unity.TextureToMat(input);

        if (output == null)
        {
            output = OpenCvSharp.Unity.MatToTexture(image);
        }
        else 
        {
            OpenCvSharp.Unity.MatToTexture(image, output);
        }

        //predict();

        return true;
    }

    void Start()
    {
        letterTxt = GameObject.Find("lett").GetComponent<TextMeshProUGUI>();
        imageA = GameObject.Find("lettimg").GetComponent<Image>();
        LoadSign();
    }

    // void Update()
    // {

    // }

    public void LoadSign()
    {
        int randomLetter = UnityEngine.Random.Range(0, 25);
        letterTxt.text = "" + upLetters[randomLetter];
        imageA.sprite = Resources.Load<Sprite>( "Letters/Col/" + upLetters[randomLetter] + 's');
        //load_ASL();
    }
    // void load_ASL()
    // {

    //     buffer = Resources.LoadAll("Letters/BW", typeof(Texture2D));
    //     int i = 0;

    //     foreach (var image in buffer)
    //     {
    //         img1 = OpenCvSharp.Unity.TextureToMat(duplicateTexture((Texture2D)buffer[i]));

    //         Cv2.CvtColor(img1, img2, ColorConversionCodes.BGR2GRAY);

    //         Cv2.Threshold(img2, threshold_Load, THRESH, 255, ThresholdTypes.Binary);
            
    //         Cv2.FindContours(threshold_Load, out contours, out hierarchy1, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple, new OpenCvSharp.Point(0, 0));

    //         letters[i] = contours[0];

    //         i++;
    //     }
    // }
//     void predict()
//     {
//         OpenCvSharp.Rect myROI = new OpenCvSharp.Rect(200, 200, 200, 200);

//         Mat cropFrame = image[myROI];

//         Cv2.CvtColor(image, processImage, ColorConversionCodes.BGR2GRAY);
//         Cv2.Threshold(processImage, processImage, Threshold, 255, ThresholdTypes.BinaryInv);
//         Cv2.FindContours(processImage, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple, null);
//         Cv2.ImShow("Crop Frame", cropFrame);

//         tex1 = OpenCvSharp.Unity.MatToTexture(cropFrame);

//         display1.GetComponent<Renderer>().material.mainTexture = tex1;

//         // Update & Apply the background model
//         ptrBackgroundMOG2.Apply(cropFrame, fgMaskMOG2, 0);

//         Cv2.ImShow("Foregound Mask", fgMaskMOG2);
       
//         tex2 = OpenCvSharp.Unity.MatToTexture(fgMaskMOG2);
//         display2.GetComponent<Renderer>().material.mainTexture = tex2;

//         // Finds edges in an image using Canny algorithm.

//         Cv2.Canny(cropFrame, canny, 50, 200);

//         Cv2.ImShow("Canny", canny);
        
//         tex3 = OpenCvSharp.Unity.MatToTexture(canny);
//         display3.GetComponent<Renderer>().material.mainTexture = tex3;

//         // Detect edges using Threshold:/// Applies a fixed-level threshold to each array element.

//         Cv2.Threshold(fgMaskMOG2, threshold_output, THRESH, 255, ThresholdTypes.Binary);

//         // Find contours
//         Cv2.FindContours(threshold_output, out feature_image, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple, new OpenCvSharp.Point(0, 0));

//         tex5 = OpenCvSharp.Unity.MatToTexture(fgMaskMOG2);
//         display5.GetComponent<Renderer>().material.mainTexture = tex5;

//         double largest_area = 0;

//         for (int j = 0; j < feature_image.Length; j++)
//         {
//             double area = Cv2.ContourArea(feature_image[j], false); // Find the area of the contour
//             if (area > largest_area)
//             {
//                 largest_area = area;
//                 maxIndex = j; // Store the index of largest contour
//             }
//         }

//         // creates Mat of Zeros = Black frame to draw on 
//         Mat contourImg = Mat.Zeros(cropFrame.Size(), MatType.CV_8UC(3));
//         // Draw Contours         
//         Cv2.DrawContours(contourImg, feature_image, maxIndex, new Scalar(0, 0, 255), 2, LineTypes.Link8, hierarchy, 0, new OpenCvSharp.Point(0, 0));

//         tex4 = OpenCvSharp.Unity.MatToTexture(contourImg);

//         display4.GetComponent<Renderer>().material.mainTexture = tex4;

//         Cv2.ImShow("Countour Img", contourImg);

//         if (feature_image.Length > 0 && frames++ > SAMPLE_RATE && feature_image[maxIndex].Length >= 5)
//         {
//              frames = 0;
//             double lowestDiff = double.PositiveInfinity;

//                 for (int i = 0; i < MAX_LETTERS; i++)
//                 {
//                     double difference = distance(letters[i], feature_image[maxIndex]);

//                     if (letters[i].Length == 0)
//                         continue;

//                     if (difference < lowestDiff)
//                     {
//                         lowestDiff = difference;

//                         asl_letter = (char)(((int)'a') + i);
//                     }
//                 }
//             // Reset if Not Matching
//             if (lowestDiff < DIFF_THRESH && lowestDiff > Diff_MAX)
//                 { 
//                     asl_letter = (char)(((int)0));
//                 }

// /*                Debug.Log("The letter is: " + asl_letter + " | difference: " + lowestDiff);*/

//                 Debug.Log(asl_letter);
//     }}

    // static Texture2D duplicateTexture(Texture2D source)
    // {
    //     RenderTexture renderTex = RenderTexture.GetTemporary(
    //                 source.width,
    //                 source.height,
    //                 0,
    //                 RenderTextureFormat.Default,
    //                 RenderTextureReadWrite.Linear);

    //     Graphics.Blit(source, renderTex);
    //     RenderTexture previous = RenderTexture.active;
    //     RenderTexture.active = renderTex;
    //     Texture2D readableText = new Texture2D(source.width, source.height);
    //     readableText.ReadPixels(new UnityEngine.Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
    //     readableText.Apply();
    //     RenderTexture.active = previous;
    //     RenderTexture.ReleaseTemporary(renderTex);
    //     return readableText;
    // }
//     double distance(OpenCvSharp.Point[] a, OpenCvSharp.Point[] b){

//                 int maxDistAB = distance_2(a, b);

//                 int maxDistBA = distance_2(b, a);

//                 int maxDist = Math.Max(maxDistAB, maxDistBA);

//                 return Math.Sqrt((double)maxDist);
//     }
//     int distance_2(OpenCvSharp.Point[] a, OpenCvSharp.Point[] b)
//             {
//                 int maxDist = 0;
//                 for (int i = 0; i < a.Length; i++)
//                 {
//                     int min = 100000;
//                     for (int j = 0; j < b.Length; j++)
//                     {
//                         int dx = (a[i].X - b[j].X);

//                         int dy = (a[i].Y - b[j].Y);

//                         int tmpDist = dx * dx + dy * dy;

//                         if (tmpDist < min)
//                         {
//                             min = tmpDist;
//                         }

//                         if (tmpDist == 0)
//                         {
//                             break; // can't get better than equal.
//                         }
//                     }
//                     maxDist += min;
//                 }
//                 return maxDist;
//      }

//     ~Finder() {
//         frame.Dispose();
//         frame.Release();
//         Cv2.DestroyAllWindows();
//     }
}
