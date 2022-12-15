using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour
{
    [SerializeField] Transform laserStartPoint;
    public float dist = 50;
    public float angle = 15;
    Vector3 direction;
    LineRenderer lr;
    bool bitti = false;

    public float mesafe;


    private void Start() {
        lr = gameObject.GetComponent<LineRenderer>();
        lr.positionCount = 2; 

        mesafe = 0;

        bitti = false;
        StartCoroutine(sonar());
    }
    private void Update() {
        //StopCoroutine(sonar());

        if (bitti){
            StartCoroutine(sonar());
            bitti = false;
        }
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

                mesafe = Vector3.Distance(laserStartPoint.position, hit.point);
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
