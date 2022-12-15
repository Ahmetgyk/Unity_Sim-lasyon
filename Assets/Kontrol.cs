using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kontrol : MonoBehaviour
{
    public GameObject kaldirac;

    public List<GameObject> sonarlar;

    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float mouseSen = 100f;
    float xRot = 0f;
    float kaldiracyükseklik = 0.45f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;        
    }   
    void Update()
    {
        for (int s = 0; s < sonarlar.Count; s++)
        {
            if (sonarlar[s].GetComponent<Sonar>().mesafe < 2)
            {
                Debug.Log(sonarlar[s].GetComponent<Sonar>().mesafe);
            }
            else Debug.Log("uzak");
        }

        float mouseY = Input.GetAxis("Horizontal") * mouseSen * Time.deltaTime;
        dönüs(mouseY);

        float z = Input.GetAxis("Vertical");
        hareket(z);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if(Input.GetKey(KeyCode.Q)){
            kaldiracyükseklik += 0.3f * Time.deltaTime;
            kaldıracyükseltme(kaldiracyükseklik);
        }
        if(Input.GetKey(KeyCode.E)){
            kaldiracyükseklik -= 0.3f * Time.deltaTime;
            kaldıracyükseltme(kaldiracyükseklik);
        }

    }

    void hareket(float z){
        //Vector3 move = transform.right * x + transform.forward * z;
        Vector3 move = transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }

    void dönüs(float mouseY){
        xRot -= mouseY;
        transform.localRotation = Quaternion.Euler(0f, xRot, 0f);
    }

    void kaldıracyükseltme(float kaldiracyükseklik){
        kaldiracyükseklik = Mathf.Clamp(kaldiracyükseklik, 0.45f, 2.4f);
        kaldirac.transform.localScale = new Vector3(1.2f, kaldiracyükseklik, 1.2f);
    }

    void zıplama(){
                isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        if(Input.GetButtonDown("Jump") && isGrounded){
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }
}