using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRoam : MonoBehaviour
{
    public float camSpeeed = 20;
    public float screenSizeThickness = 10;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        //UP
        if(Input.mousePosition.y >= Screen.height - screenSizeThickness)
        {
            //pos.x -= camSpeeed * Time.deltaTime;


            //TEST THIS
            pos.z += camSpeeed * Time.deltaTime;
        }
        //DOWN
        
        if (Input.mousePosition.y <= screenSizeThickness)
        {
            //pos.x += camSpeeed * Time.deltaTime;


            //TEST THIS
            pos.z -= camSpeeed * Time.deltaTime;
        }
        
        //LEFT
        
        if (Input.mousePosition.x <= screenSizeThickness)
        {
            //pos.z -= camSpeeed * Time.deltaTime;


            //TEST THIS
            pos.x -= camSpeeed * Time.deltaTime;
        }
        
        //RIGHT
        
        if (Input.mousePosition.x >= Screen.height - screenSizeThickness)
        {
            //pos.z += camSpeeed * Time.deltaTime;


            //TEST THIS
            pos.x += camSpeeed * Time.deltaTime;
        }
        
        transform.position = pos;
    }   
}
