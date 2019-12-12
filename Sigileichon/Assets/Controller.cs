using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    void Start()
    {
        
    }

    public float speed = 0.1f;
    void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * speed;

        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= Vector3.right * speed;
            
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed;
            
        }
        
    }
}
