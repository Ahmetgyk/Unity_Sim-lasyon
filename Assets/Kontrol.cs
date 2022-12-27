using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kontrol : MonoBehaviour
{
    public GameObject kaldirac;

    public List<GameObject> sonarlar;
    public List<GameObject> kizilötesiler;
    public List<GameObject> cizgiTakip;

    public CharacterController controller;

    public bool solda;
    public bool sağda;
    public int dönüşk, sint, kint;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float mouseSen = 100f;
    float xRot = 0f;
    float kaldiracyükseklik = 0.45f;
    float mouseY = 0;
    float z = 0;
    public bool cizgi, sonar;
    bool anlikileri = false;

    Vector3 velocity;
    bool isGrounded;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; 
        solda = false;
        sağda = false;   
        cizgi = true;
        sonar = false;  
        anlikileri = true;  
    }   
    void Update()
    {
        if (sonarlar[1].GetComponent<Sonar>().tekaciSonar() < 2){           
            sonar = true;
            cizgi = false;
        }

        if(cizgi){
            cizgitakip();
        }
        else if(sonar){
            string yazi = System.Math.Round(sonarlar[0].GetComponent<Sonar>().tekaciSonar(), 2).ToString() + "  " + sonarlar[2].GetComponent<Transform>().name;
            //Debug.Log(sonarlar[2].GetComponent<Sonar>().tekaciSonar());

            if(anlikileri){
                if(sonarlar[2].GetComponent<Sonar>().tekaciSonar() < sonarlar[0].GetComponent<Sonar>().tekaciSonar()){
                    dönüşk = -1;
                    sint = 0;
                    kint = 0;
                }
                else if(sonarlar[2].GetComponent<Sonar>().tekaciSonar() == sonarlar[0].GetComponent<Sonar>().tekaciSonar()){
                    dönüşk = +1;
                    sint = 2;
                    kint = 1;
                }
                else {
                    dönüşk = +1;
                    sint = 2;
                    kint = 1;
                }
                anlikileri = false;
            }

            if(sonarlar[sint].GetComponent<Sonar>().tekaciSonar() < 2.5){
                dönüs(0.2f * dönüşk * mouseSen * Time.deltaTime);
            }        
            else if(sonarlar[sint].GetComponent<Sonar>().tekaciSonar() >= 2.5){
                if(kizilötesiler[kint].GetComponent<Kzıılötesi>().var){
                    hareket(1f);
                }
                else{
                    if(anlikileri){
                        controller.Move(transform.forward * 0); 
                        anlikileri = false;
                    } 
                    dönüs(-0.2f * dönüşk *  mouseSen * Time.deltaTime);
                }

                for (int c = 0; c < cizgiTakip.Count / 2; c++)
                    {
                        if (!cizgiTakip[c].GetComponent<ÇizgiIzleme>().deydi)
                        {
                            sağda = true;
                            break;
                        }
                        else sağda = false;
                    }
                    for (int c = cizgiTakip.Count / 2; c < cizgiTakip.Count; c++)
                    {
                        if (!cizgiTakip[c].GetComponent<ÇizgiIzleme>().deydi)
                        {
                            solda = true;
                            break;
                        }
                        else solda = false;
                    }
                    if(!sağda || !solda){
                        cizgi = true;
                        sonar = false;

                        anlikileri = true;
                    }
            }
        }       

        mouseY = Input.GetAxis("Horizontal") * mouseSen * Time.deltaTime;
        dönüs(mouseY);

        z = Input.GetAxis("Vertical");
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
    float SonarOkuma(int n){
        float mes = 0;
        sonarlar[n].GetComponent<Sonar>().calistir = true;
        StartCoroutine(Sbekle());
        mes = sonarlar[n].GetComponent<Sonar>().mesafe;
        sonarlar[n].GetComponent<Sonar>().calistir = false;

        return mes;
    }

    void cizgitakip(){
        for (int c = 0; c < cizgiTakip.Count / 2; c++)
        {
            if (!cizgiTakip[c].GetComponent<ÇizgiIzleme>().deydi)
            {
                sağda = true;
                break;
            }
            else sağda = false;
        }
        for (int c = cizgiTakip.Count / 2; c < cizgiTakip.Count; c++)
        {
            if (!cizgiTakip[c].GetComponent<ÇizgiIzleme>().deydi)
            {
                solda = true;
                break;
            }
            else solda = false;
        }

        if(sağda && solda){
            mouseY = 1 * mouseSen * Time.deltaTime;
            dönüs(mouseY);
        }
        else if(sağda){
            mouseY = -1 * mouseSen * Time.deltaTime;
            dönüs(mouseY);
        }
        else if(solda){
            mouseY = 1 * mouseSen * Time.deltaTime;
            dönüs(mouseY);
        }
        if(!sağda && !solda){
            z = 1;
            hareket(z);
        }
    }

    public void hareket(float z){
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

    IEnumerator bekle(){
        yield return new WaitForSecondsRealtime(2f);
    }
    IEnumerator Sbekle(){
        yield return new WaitForSecondsRealtime(2f);
    }
}