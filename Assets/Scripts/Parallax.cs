using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Camera cam = null;
    public float movementFactor = 1.0f;
    public bool verticalDisplacement = false;
    private float width = 0.0f;
    private float height = 0.0f;
    private Vector2 startPosition;

    void Awake() {
        if (cam == null) {
            cam = Camera.main;
        }

        startPosition = new Vector2(transform.position.x, transform.position.y);

        Vector3 size = GetComponent<SpriteRenderer>().bounds.size;
        width = size.x;
        height = size.y;
        Debug.Log(size);
        Debug.Log("Start Pos" + startPosition);
        Debug.Log("Cam Pos" + cam.transform.position);
    }

    void Start()
    {
    }

    void Update()
    {
        float horizontalDist = cam.transform.position.x * movementFactor;
        float verticalDist = cam.transform.position.y * movementFactor;
        float hTemp = (cam.transform.position.x * (1 - movementFactor));
        float vTemp = (cam.transform.position.y * (1 - movementFactor));
        
        // width >= abs(cam.transform.position.x * movementFactor - transform.position.x) // reset position
        // reset position transform.position.x += width * movementFactor
        transform.position = new Vector3(startPosition.x + horizontalDist, startPosition.y + verticalDist, transform.position.z);
        if (hTemp > startPosition.x + width) startPosition.x += width;
        else if (hTemp < startPosition.x - width) startPosition.x -= width;

        if (vTemp > startPosition.y + height) startPosition.y += height;
        else if (vTemp < startPosition.y - height) startPosition.y -= height;

        // if (width / 2 <= cam.transform.position.x - transform.position.x) {
        //     Debug.Log("changed width");
        //     // Debug.Log("Distance" + (cam.transform.position.x * movementFactor - transform.position.x));
        //     transform.position += new Vector3(width, 0.0f, 0.0f); 
        //     startPosition.x += width;
        // }
        // if (-width/2 >= cam.transform.position.x - transform.position.x) {
        //     Debug.Log("changed -width");
        //     // Debug.Log("Distance " + (cam.transform.position.x * movementFactor - transform.position.x));
        //     //transform.position -= new Vector3(horizontalDist + width, 0.0f, 0.0f); 
        //     startPosition.x -= width;
        // }

        // if (verticalDisplacement) {
        //     float verticalDist = cam.transform.position.y * movementFactor;

        //     if (verticalDist >= height + transform.position.y) {
        //         transform.position += new Vector3(0.0f, verticalDist + height, 0.0f); 
        //     }
        //     if (verticalDist >= height - transform.position.x) {
        //         transform.position -= new Vector3(0.0f, verticalDist + height, 0.0f); 
        //     }
        // }
    }
}
