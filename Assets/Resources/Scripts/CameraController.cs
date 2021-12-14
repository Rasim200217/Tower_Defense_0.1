using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 30f;
    public float border = 10f;

    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maXY = 50f;
    


    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - border)
        {
            transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime, Space.World );
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= border)
        {
            transform.Translate(Vector3.left * cameraSpeed * Time.deltaTime, Space.World );
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - border)
        {
            transform.Translate(Vector3.back * cameraSpeed * Time.deltaTime, Space.World );
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= border)
        {
            transform.Translate(Vector3.forward * cameraSpeed * Time.deltaTime, Space.World );
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maXY);

        transform.position = pos;
    }
}
