using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    float speed = 6f;
    float rotateSpeed = 7f;

    float jetpackFuel = 100;
    float accelerationTime = 60;

    public ParticleSystem jetpack;

    public Transform camPivot;
    float heading = 0;
    public Transform cam;

    Transform shield;

    Rigidbody rb;

    Vector3 input;

    float time;

    public float gravity = 10f;
    public float JetForce = 0.9f;

    float maxHeight;

    float min = 8f;
    float max = 8.5f;

    float t;

    Transform playerGun;

    Quaternion CameraRotation;

    bool shieldIsActive = false;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
        rb.freezeRotation = true;

        CameraRotation = Camera.main.transform.rotation;

        playerGun = transform.Find("PlayerGun");
        shield = transform.Find("Shield");
        shield.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Vector3 camF = cam.forward;
        Vector3 CamR = cam.right;

        camF.y = 0;
        CamR.y = 0;

        camF = camF.normalized;
        CamR = CamR.normalized;

        transform.position += (camF * input.z + CamR * input.x) * Time.deltaTime * speed;

        Quaternion rotation = Quaternion.LookRotation((camF * input.z + CamR * input.x));
        if(input != Vector3.zero){
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
        }

         if(transform.position.x >= 40){
            transform.position = new Vector3(40, transform.position.y, transform.position.z);
        } else if(transform.position.z >= 40){
            transform.position = new Vector3(transform.position.x, transform.position.y, 40);
        }else if(transform.position.x <= 0){
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        } else if(transform.position.z <= 0){
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }  

        t += 1f * Time.deltaTime;
        maxHeight = Mathf.Lerp(min, max, t);

        if (t > 1.0f)
        {
            float temp = max;
            max = min;
            min = temp;
            t = 0.0f;
        }
        
        if(transform.position.y > 8){
            transform.position = new Vector3(transform.position.x, maxHeight, transform.position.z);
        }

        float aim = Input.GetAxis("Fire2");
        if(aim == 1 && Camera.main.orthographicSize < 15f){
            var duration = 0.5f;
            float ti = 0;
            ti += Time.deltaTime/duration;
            
            Camera.main.transform.LookAt(transform.position);
            Camera.main.orthographicSize = 5.01f;
        }
        if(aim < 0 && Camera.main.orthographicSize > 5f) {
            var duration = 0.5f;
            float ti = 0;
            ti += Time.deltaTime/duration;

            Camera.main.orthographicSize = 14.9f;

        }
    }

    void FixedUpdate(){
        if(Input.GetAxis("Jump") > 0 && jetpackFuel != 0){
            jetpack.Play();

            Vector3 force = JetForce * transform.up;
            rb.AddForce(force, ForceMode.VelocityChange);
            
        }else{
            jetpack.Stop();
        }

        rb.AddForce(new Vector3(0, -gravity, 0));

        if(Input.GetKeyUp("joystick button 1") || Input.GetKeyUp(KeyCode.Space)){
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }

        if(!shieldIsActive){
            if(Input.GetKeyUp(KeyCode.G)){
                StartCoroutine("Shield");
                shieldIsActive = true;
            }
        }

    }

    private void OnTriggerEnter(Collider other) {   
        if(other.tag == "Weapon"){
            PickUpWeapon();
            Destroy(other.gameObject);
        }
    }

    void PickUpWeapon(){
        Debug.Log("Picked Up Weapon");
        playerGun.gameObject.SetActive(true);
    }

    IEnumerator Shield(){
        shield.gameObject.SetActive(true);
        yield return new WaitForSeconds(10);
        shield.gameObject.SetActive(false); 
        shieldIsActive = false;
    }
        
}
