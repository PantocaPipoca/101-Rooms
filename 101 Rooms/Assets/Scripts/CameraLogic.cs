using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    Camera cam;
    GameObject player;
    public float step;

    void Start()
    {
        cam = GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void LateUpdate()
    {
        CameraFollow();
    }

    private void CameraFollow()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        mousePos.z = -10;
        Vector3 middle = new Vector3((mousePos.x - player.transform.position.x) / 3 + player.transform.position.x, (mousePos.y - player.transform.position.y) / 3 + player.transform.position.y, mousePos.z);
        cam.transform.position = Vector3.MoveTowards(cam.transform.position, middle, step);
    }
}