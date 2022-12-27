using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour
{
    [SerializeField] Transform laserStartPoint;
    public float dist = 5;
    public float angle = 15;

    public bool tekaci = true;

    Vector3 direction;
    LineRenderer lr;
    bool bitti = false;

    public bool calistir = false;

    public float mesafe;


    private void Start() {
        lr = gameObject.GetComponent<LineRenderer>();
        lr.positionCount = 2; 

        mesafe = 0;

        bitti = false;
        //StartCoroutine(sonar());
    }
    private void Update() {
        //StopCoroutine(sonar());
        if(!tekaci){
            if(calistir){
                if (bitti){
                    StartCoroutine(sonar());
                    bitti = false;
                } 
            }
        }
        else{
            //tekaciSonar();
        }
        
    }

    public float sensör(){
        mesafe = 0;
        for (float a = 0; a <= angle; a++)
        {
            laserStartPoint.transform.localRotation = Quaternion.Euler(0f, (-1 * angle / 2) + a, 0f);

            direction = laserStartPoint.forward;
            lr.SetPosition(0, laserStartPoint.position);

            RaycastHit hit;
            if(Physics.Raycast(laserStartPoint.position, direction, out hit, Mathf.Infinity)){
                lr.SetPosition(1, hit.point);
                
                if(mesafe < Vector3.Distance(laserStartPoint.position, hit.point)){
                    mesafe = Vector3.Distance(laserStartPoint.position, hit.point);
                }               
            }
            else{
                lr.SetPosition(1, laserStartPoint.position + (direction * dist));

                mesafe = Mathf.Infinity;
            }
        }
        return mesafe;
    }

    public float tekaciSonar(){
        mesafe = 0;
        laserStartPoint.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

        direction = laserStartPoint.forward;
        lr.SetPosition(0, laserStartPoint.position);

        RaycastHit hit;
        if(Physics.Raycast(laserStartPoint.position, direction, out hit, Mathf.Infinity)){
            lr.SetPosition(1, hit.point);
            
            if(mesafe < Vector3.Distance(laserStartPoint.position, hit.point)){
                mesafe = Vector3.Distance(laserStartPoint.position, hit.point);
            }               
        }
        else{
            lr.SetPosition(1, laserStartPoint.position + (direction * dist));

            mesafe = Mathf.Infinity;
        }
        return mesafe;
    }


    IEnumerator sonar(){
        for (float a = 0; a <= angle; a++)
        {
            laserStartPoint.transform.localRotation = Quaternion.Euler(0f, (-1 * angle / 2) + a, 0f);

            direction = laserStartPoint.forward;
            lr.SetPosition(0, laserStartPoint.position);

            RaycastHit hit;
            if(Physics.Raycast(laserStartPoint.position, direction, out hit, Mathf.Infinity)){
                lr.SetPosition(1, hit.point);
                
                if(mesafe < Vector3.Distance(laserStartPoint.position, hit.point)){
                    mesafe = Vector3.Distance(laserStartPoint.position, hit.point);
                }               
            }
            else{
                lr.SetPosition(1, laserStartPoint.position + (direction * dist));

                mesafe = Mathf.Infinity;
            }

            yield return new WaitForSecondsRealtime(.1f);
        }

        bitti = true;
    }
}
