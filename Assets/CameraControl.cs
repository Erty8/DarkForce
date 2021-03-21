using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float scrollSpeed = 1f ;
    public float topBarrier;
    public float bottomBarrier;
    public float leftBarrier;
    public float rightBarrier;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // camera movement
        if (Input.mousePosition.y >= Screen.height*topBarrier)
        {
            transform.Translate(Vector2.up * Time.deltaTime * scrollSpeed, Space.World);
        }
        if (Input.mousePosition.y <= Screen.height * bottomBarrier)
        {
            transform.Translate(Vector2.down * Time.deltaTime * scrollSpeed, Space.World);
        }
        if (Input.mousePosition.x >= Screen.height * rightBarrier)
        {
            transform.Translate(Vector2.right * Time.deltaTime * scrollSpeed, Space.World);
        }
        if (Input.mousePosition.x <= Screen.height * leftBarrier)
        {
            transform.Translate(Vector2.left * Time.deltaTime * scrollSpeed, Space.World);
        }
    }
}
