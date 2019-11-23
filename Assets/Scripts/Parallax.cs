using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Camera cam = null;
    /// Movement relative to camera movement
    public float movementRelToCamera = 1.0f;
    /// Movement of the texture by itself, like clouds moving at the background while the camera is locked
    public float movementFactorX = 0.0f;
    public float movementFactorY = 0.0f;

    /// Offset carried.
    /// TODO: Overflows? it is to hard to get there? 
    private Vector2 _offset;
    /// Last camera position to get the new displacement
    private Vector2 _lastCameraPos;
    /// TODO: Take a look at the performance of mainTextureOffset and if it should better a custom shader
    private MeshRenderer _renderer;

    void Awake() {
        if (cam == null) {
            cam = Camera.main;
        }

        _lastCameraPos = new Vector2(cam.transform.position.x, cam.transform.position.y);
        
        _renderer = GetComponent<MeshRenderer>();

        _offset = new Vector2();
    }

    void Start()
    {
    }

    void Update()
    {
        Vector2 newCameraPos = new Vector2(cam.transform.position.x, cam.transform.position.y);
        Vector2 distance = newCameraPos - _lastCameraPos;
        _lastCameraPos = newCameraPos; 

        transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, transform.position.z);

        _offset += distance * movementRelToCamera + new Vector2(movementFactorX * Time.deltaTime, movementFactorY * Time.deltaTime);
        _renderer.material.mainTextureOffset = _offset;
    }
}
