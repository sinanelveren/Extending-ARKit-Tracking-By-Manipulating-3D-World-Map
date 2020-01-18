
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

namespace UnityEngine.XR.iOS
{
    public class EnableAnchorSpawner : MonoBehaviour
    {
        [SerializeField] public GameObject arWorldMapManager;
        [SerializeField] public GameObject anchorObject;

        private HashSet<string> m_AnchorObjects;
        private int objectCount = 0;

        private Pose pose;
        
        private void OnEnable()
        {
            // Handles.Instance.EnableSelection = false;
        }

        private void OnDisable()
        {
            //Handles.Instance.EnableSelection = true;
        }

        private void LateUpdate()
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
                {
                    objectCount++;

                    //screen point
                    var screenPosition = Camera.main.ScreenToViewportPoint(touch.position);
                    var point = new ARPoint
                    {
                        x = screenPosition.x,
                        y = screenPosition.y
                    };

                    // Prioritize result types.
                    ARHitTestResultType[] resultTypes =
                    {
                        //ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingGeometry,
                        //ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent,
                        //ARHitTestResultType.ARHitTestResultTypeExistingPlane,
                        ARHitTestResultType.ARHitTestResultTypeEstimatedHorizontalPlane,
                        ARHitTestResultType.ARHitTestResultTypeEstimatedVerticalPlane,
                        ARHitTestResultType.ARHitTestResultTypeFeaturePoint
                    };
                    

                    foreach (ARHitTestResultType resultType in resultTypes)
                    {
                        if (TryHitWithResultType(point, resultType))
                        {
                            /*   
                                 arWorldMapManager.GetComponent<>().AddWorldAnchor(pose);
                                  arWorldMapManager.GetComponent(WorldMapManager_OnWorldAnchorAdded);
                                 
                                var worldMapManager = arWorldMapManager.GetComponent();
                                if (worldMapManager)
                                {
                                    worldMapManager.WorldAnchorAdded += WorldMapManager_OnWorldAnchorAdded;
                                }
    
                                break;
                            */
                            return;
                        }
                    }
                }
                
                if (objectCount > 0)
                {
                    Instantiate(anchorObject, pose.position, pose.rotation);
                    //Instantiate(anchorObject, m_HitTransform.position, m_HitTransform.rotation);

                    UnityARUserAnchorComponent component = anchorObject.GetComponent<UnityARUserAnchorComponent>();
                    m_AnchorObjects.Add(component.AnchorId);
                }
            }
        }

        //static
        private bool TryHitWithResultType(ARPoint point, ARHitTestResultType resultTypes)
        {
            

            var hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface().HitTest(point, resultTypes);
            if (hitResults.Count > 0)
            {
                foreach (var hitResult in hitResults)
                {
                    pose.position = UnityARMatrixOps.GetPosition(hitResult.worldTransform);
                    pose.rotation = UnityARMatrixOps.GetRotation(hitResult.worldTransform);
                    return true;
                }
            }

            return false;
        }
    }
}