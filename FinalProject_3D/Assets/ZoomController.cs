using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomController : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get the current mouse position in screen space
        Vector3 mousePos = Input.mousePosition;

        // Convert the screen space mouse position to world space
        Vector3 mousePosInWorld = Camera.main.ScreenToWorldPoint(mousePos);

        Debug.Log(mousePosInWorld);

        // Set the position of the game object to the mouse position in world space
        //transform.position = new Vector3(mousePosInWorld.x, mousePosInWorld.y, -20);

        //Debug.Log(transform.position);
    }
}
