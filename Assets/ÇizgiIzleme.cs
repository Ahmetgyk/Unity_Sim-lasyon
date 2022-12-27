using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ÇizgiIzleme : MonoBehaviour
{
    public bool deydi;
    private void Start() {
        deydi = false;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Cizgi") deydi = true;
    }
    private void OnCollisionStay(Collision other) {
        if(other.gameObject.tag == "Cizgi") deydi = true;
    }
    private void OnCollisionExit(Collision other) {
        if(other.gameObject.tag == "Cizgi") deydi = false;
    }
}
