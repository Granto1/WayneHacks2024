using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;

public class FaceDetector : MonoBehaviour
{
    // Start is called before the first frame update
    WebCamTexture _webCamTexture;
    //CascadeClassifier cascade;
    //OpenCvSharp.Rect MyFace;
    
    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        //cascade = new CascadeClassifier(Application.dataPath + @"haarcasecade_frontalface_default.xml");

        _webCamTexture = new WebCamTexture(devices[0].name);
        _webCamTexture.Play();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.mainTexture = _webCamTexture;
        //Mat frame = OpenCvSharp.Unity.TextureToMat(_webCamTexture);

        //findNewFace(frame);
        //display(frame);
    }
    // void findNewFace(Mat frame)
    // {
    //     var faces = cascade.DetectMultiScale(frame, 1.1, 2, HaarDetectionType.ScaleImage);

    //     if(faces.length >= 1)
    //     {
    //         Debug.Log(faces[0].Location);
    //         MyFace = faces[0];
    //     }
    // }

    // void display(Mat frame)
    // {
    //     if (MyFace != null)
    //     {
    //         frame.Rectangle(MyFace, new Scalar(250, 0, 0), 2);
    //     }
    //     Texture newtexture = OpenCvSharp.Unitiy.MatToTexture(frame);
    //     GetComponent<Renderer>().material.mainTexture = newtexture;
    // }
}
