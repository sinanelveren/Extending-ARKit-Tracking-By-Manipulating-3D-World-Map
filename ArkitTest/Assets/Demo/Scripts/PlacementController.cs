using System.Collections;
using System.Collections.Generic;
//using TMPro.EditorUtilities;
using UnityEngine;
//using UnityEngine.TestTools.Utils;
//using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARFoundation;


using UnityEngine.XR.ARSubsystems;

using UnityEngine.SceneManagement;

[RequireComponent(typeof(ARRaycastManager))]
/**
 * @author Sinan Elveren
 * Controller for reference point of carcass
 */
public class PlacementController : MonoBehaviour
{

    [SerializeField]
    private GameObject placedPrefab;

    public GameObject PlacedPrefab
    {
        get 
        {
            return placedPrefab;
        }
        set 
        {
            placedPrefab = value;
        }
    }

    public ARRaycastManager arRaycastManager;
    
    [SerializeField]
    private GameObject placedFrame;
    
    //*
    
    private Pose placementPose;
    private bool placementPoseIsValid = false;

    private Vector3 cameraForward;
    private Vector3 cameraBearing;
   
    public GameObject placementIndicator;
    private GameObject robot;
    private GameObject frame;
    private Vector3 initialScale;
    private Vector3 initialScaleF;
    private int scaleCount = 1;
    private Pose initialPose;

    private int objectCount = 0;
    //*
    static string myLog = "";
    private string output;
    private string stack;
    Queue myLogQueue = new Queue();

    
    
    void Awake() 
    {
        //arRaycastManager = GetComponent<ARRaycastManager>();
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
    }


    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        
        
        arRaycastManager.Raycast(screenCenter, hits, TrackableType.FeaturePoint);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;
            cameraForward = Camera.current.transform.forward;
            cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }
    
    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();
        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            //if(arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            objectCount++;
            
            if (objectCount == 1)
            {
                robot = Instantiate(placedPrefab, placementPose.position, placementPose.rotation);
                frame = Instantiate(placedFrame, placementPose.position, placementPose.rotation);
                robot.transform.localScale /= 10;
                frame.transform.localScale /= 10;
                initialScale = robot.transform.localScale;
                initialScaleF = frame.transform.localScale;
                initialPose = placementPose;

                //  Debug.Log("pose " + placementPose.position);
            }
            
            //robot.transform.localScale += new Vector3(10.0f, 10.0f, 10.0f);
            //robot.transform.position += new Vector3(100.0f, 100.0f, 100.0f);
        }

       

    }
    
    
    public void Log(string logString, string stackTrace, LogType type)
    {
        myLog = logString;
        string newString = "\n [" + type + "] : " + myLog;
        myLogQueue.Enqueue(newString);
        if (type == LogType.Exception)
        {
            newString = "\n" + stackTrace;
            myLogQueue.Enqueue(newString);
        }
        myLog = string.Empty;
        foreach(string mylog in myLogQueue){
            myLog += mylog;
        }
    }
        
        
    void OnGUI()
    {
        
        if (GUI.Button(new Rect(Screen.width - 300, Screen.height - 200 - 250, 250, 100), "BÜYÜT"))
        {
            if (scaleCount < 10)
            {
                ++scaleCount;
                robot.transform.localScale =  initialScale * scaleCount;
                frame.transform.localScale =  initialScaleF * scaleCount;
                
                //The SceneManager loads your new Scene as a single Scene (not overlapping). This is Single mode.
                //SceneManager.LoadScene("front", LoadSceneMode.Single);
            }
        }

    
        if (GUI.Button(new Rect(Screen.width - 300, Screen.height - 200 - 100, 250, 100), "KÜÇÜLT"))
        {
            if (scaleCount > 1)
            {
                --scaleCount;
                robot.transform.localScale = initialScale * scaleCount;
                frame.transform.localScale = initialScaleF * scaleCount;
                //The SceneManager loads your new Scene as a single Scene (not overlapping). This is Single mode.
                //SceneManager.LoadScene("front", LoadSceneMode.Single);
            }
        }
        

        //if (!Application.isEditor) //Do not display in editor ( or you can use the UNITY_EDITOR macro to also disable the rest)
       /* {
             GUI.Label(new Rect(10, 10, Screen.width - 10, Screen.height - 10), myLog);
        }*/
    }


    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
}