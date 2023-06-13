using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
public class ARCursor : MonoBehaviour
{
    public GameObject cursorChildObject;
    public GameObject objectTopPlace;
    public ARRaycastManager raycastManager;

    public bool useCursor = true;

    // Start is called before the first frame update
    void Start()
    {
        cursorChildObject.SetActive(useCursor);
    }

    // Update is called once per frame
    void Update()
    {
        if (useCursor)
        {
            UpdateCursor();
        }

        if (Input.touchCount >0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
            if (useCursor)
            {
                GameObject.Instantiate(objectTopPlace, transform.position, transform.rotation);

            }
            else
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
                if (hits.Count >0)
                {
                    GameObject.Instantiate(objectTopPlace, hits[0].pose.position, hits[0].pose.rotation);
                }
            }
        }
    }
    void UpdateCursor()
    {
        Vector2 scrennPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits= new List<ARRaycastHit>();
        raycastManager.Raycast(scrennPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        if (hits.Count>0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation= hits[0].pose.rotation;
        }
    }
}
