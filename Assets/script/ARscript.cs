using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARscript : MonoBehaviour
{

    public ARPlaneManager planemanager;
    public ARRaycastManager raycastmanager;

    public Text Debugtext;
    
    public GameObject Netherportal;

    private bool placed = false;
    private bool hitrue = false;
    // Start is called before the first frame update
    void Start()
    {
        planemanager = GameObject.Find("AR Session Origin").GetComponent<ARPlaneManager>();
        raycastmanager = GameObject.Find("AR Session Origin").GetComponent<ARRaycastManager>();

        Debugtext.text = "Application started";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Debugtext.text = Input.GetTouch(0).position.ToString();
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            hitrue = raycastmanager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon);

            
            if (hitrue && placed == false)
            {
                ARRaycastHit hit = hits[0];
                ARPlane Touch_Plane = planemanager.GetPlane(hit.trackableId);
                GameObject portal = Instantiate(Netherportal, hit.pose.position, Touch_Plane.transform.rotation);
                
                Debugtext.text = Touch_Plane.transform.rotation.ToString();
                planemanager.SetTrackablesActive(false);
                planemanager.enabled = false;
                placed = true;
            }
        }
        
    }
}
