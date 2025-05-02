using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCameraSprite : MonoBehaviour
{
    Vector3 cameraDir;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cameraDir = cam.transform.forward;
        cameraDir.y = 0;

        transform.rotation = Quaternion.LookRotation(cameraDir);
    }
}