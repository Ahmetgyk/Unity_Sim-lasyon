using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kzıılötesi : MonoBehaviour
{
    [SerializeField] Transform laserStartPoint;
    public float dist = 20;
    Vector3 direction;
    LineRenderer lr;
    public bool var;


    private void Start() {
        lr = gameObject.GetComponent<LineRenderer>();
        lr.positionCount = 2;
        var = false;
    }
    void Update()
    {
        direction = laserStartPoint.forward;
        lr.SetPosition(0, laserStartPoint.position);

        RaycastHit hit;
        if(Physics.Raycast(laserStartPoint.position, direction, out hit, Mathf.Infinity)){
            lr.SetPosition(1, hit.point);

            if(Vector3.Distance(laserStartPoint.position, hit.point) < dist){
                var = true;
            } 
            else{
                var = false;
            }
        }
        else{
            lr.SetPosition(1, laserStartPoint.position + (direction * dist));
            var = false;
        }
    }
}