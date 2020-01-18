using UnityEngine;

namespace UnityEngine.XR.iOS
{
    public class AnchorSpawner : MonoBehaviour
    {
 /*      // public ARWorldAnchorObject AnchorPrefab;
        public GameObject AnchorPrefab;
        public serializableARWorldMap arWorldMap;

        private int _AnchorCount = 0;
        
        private void OnEnable()
        {
            var worldMapManager = arWorldMap.Instance;
            if (worldMapManager)
            {
                worldMapManager.WorldAnchorAdded += WorldMapManager_OnWorldAnchorAdded;
            }
        }

        private void OnDisable()
        {
            var worldMapManager = ARWorldMapManager.Instance;
            if (worldMapManager)
            {
                worldMapManager.WorldAnchorAdded -= WorldMapManager_OnWorldAnchorAdded;
            }
        }
        
        private void WorldMapManager_OnWorldAnchorAdded(object sender, WorldAnchorAddedEventArgs e)
        {
            var anchorObject = Instantiate(AnchorPrefab, e.WorldAnchor.Position, e.WorldAnchor.Rotation);
            anchorObject.WorldAnchor = e.WorldAnchor;

            if (string.IsNullOrWhiteSpace(anchorObject.WorldAnchor.Tag))
            {
                anchorObject.WorldAnchor.Tag = (++_AnchorCount).ToString();    
            }
        }
  */  }
}