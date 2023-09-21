using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARscript : MonoBehaviour
{

    public ARPlaneManager planemanager;
    public ARRaycastManager raycastmanager;

    public GameObject Netherportal;

    private bool placed = false;
    
    // Start is called before the first frame update
    void Start()
    {
        planemanager = GameObject.Find("AR Session Origin").GetComponent<ARPlaneManager>();
        raycastmanager = GameObject.Find("AR Session Origin").GetComponent<ARRaycastManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            bool hit_true = raycastmanager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon);

            if (hit_true && placed == false)
            {
                ARRaycastHit hit = hits[0];
                ARPlane Touch_Plane = planemanager.GetPlane(hit.trackableId);
                Instantiate(Netherportal, hit.pose.position, Touch_Plane.transform.rotation);
                
                planemanager.SetTrackablesActive(false);
                planemanager.enabled = false;
                placed = true;
            }
        }
        
    }
}
