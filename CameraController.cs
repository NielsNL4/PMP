using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;

    float rotateSpeed = 30f;
    // Start is called before the first frame update
    void Start()
    {
        // player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);

        float mouseX = Input.GetAxis("Mouse X");
        float dpadX = Input.GetAxis("Dpad X");

        if(mouseX > 0.01 || Input.GetKey(KeyCode.X)){
            transform.Translate(Vector3.right * Time.deltaTime * rotateSpeed);   
        }else if(mouseX < -0.01 || Input.GetKey(KeyCode.Z)){
            transform.Translate(Vector3.left * Time.deltaTime * rotateSpeed); 
        }
        
        if(Input.GetKey(KeyCode.Q) && transform.position.y < 30){
            transform.Translate(Vector3.up* Time.deltaTime * 5f, Space.World);
        }else if(Input.GetKey(KeyCode.E) && transform.position.y > 15){
            transform.Translate(Vector3.up* Time.deltaTime * -5f, Space.World);
        }

        
    }
}
