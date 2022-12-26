using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yükleme : MonoBehaviour
{
    public List<GameObject> sensörler;
    public Vector3 bas;
    public Vector3 bit;
    public Vector3 ort;
    Vector3 dir;
    bool vardi;
    public bool girdi;

    private void Start() {
        girdi = false;
    }
    void Update()
    {
        if(sensörler[0].GetComponent<Kzıılötesi>().var && sensörler[1].GetComponent<Kzıılötesi>().var && !girdi){
            girdi = true;
            vardi = true;
        }

        if(girdi){
            if(!sensörler[0].GetComponent<Kzıılötesi>().var && !sensörler[1].GetComponent<Kzıılötesi>().var && vardi){
                bas = GetComponent<Transform>().position;
                dir = GetComponent<Transform>().forward;
            }
            else if(sensörler[0].GetComponent<Kzıılötesi>().var && sensörler[1].GetComponent<Kzıılötesi>().var && !vardi){
                bit = GetComponent<Transform>().position;
                ort = bas + dir * (Vector3.Distance(bas, bit) / 2);
            }

            vardi = sensörler[0].GetComponent<Kzıılötesi>().var && sensörler[1].GetComponent<Kzıılötesi>().var;
        }
    }
}
