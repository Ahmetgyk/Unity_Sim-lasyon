 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lidar : MonoBehaviour
{
    [SerializeField] Transform laserStartPoint;
    public float dist = 50;
    Vector3 direction;
    LineRenderer lr;

    public bool calistir = true;

    private void Start() {
        lr = gameObject.GetComponent<LineRenderer>();
        lr.positionCount = 2;
    }
    void Update()
    {
        if(calistir){
            direction = laserStartPoint.forward;
            lr.SetPosition(0, laserStartPoint.position);

            RaycastHit hit;
            if(Physics.Raycast(laserStartPoint.position, direction, out hit, Mathf.Infinity)){
                lr.SetPosition(1, hit.point);
            }
            else{
                lr.SetPosition(1, laserStartPoint.position + (direction * dist));
            }
        }
    }
}