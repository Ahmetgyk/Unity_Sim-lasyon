using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;  
using System.Runtime.Serialization.Formatters.Binary;

public class Rapor : MonoBehaviour
{
    public List<Vector3> noktalar = new List<Vector3>();
    public GameObject araç;
    public List<GameObject> çizgiS;

    Vector3 nokta;

    public Transform bas;
    public List<Vector3> Nav = new List<Vector3>();

    void Update()
    {
        bool kenarKizilÖtesi_Algiladimi = çizgiS[0].GetComponent<ÇizgiIzleme>().deydi;
        int çizgi_genişliği = 2;
        Vector3 aracin_Anlik_Konumu = new Vector3(Mathf.Round(araç.transform.position.x), Mathf.Round(araç.transform.position.y), Mathf.Round(araç.transform.position.z));

        if(kenarKizilÖtesi_Algiladimi){
            if(noktalar.Count != 0){
                if(Vector3.Distance(noktalar[noktalar.Count - 1], nokta) > çizgi_genişliği){
                    Vector3 nokta = new Vector3(aracin_Anlik_Konumu.x + çizgi_genişliği / 2, 0, aracin_Anlik_Konumu.z);
                    noktalar.Add(aracin_Anlik_Konumu);
                }
            }
            else{
                noktalar.Add(aracin_Anlik_Konumu);
            }
        }
    }

    public List<Vector3> nav(Vector3 hedef){
        Nav.Add(hedef);

        if((bas.position.z - hedef.z) < 1){
            araç.GetComponent<Kontrol>().hareket(5);
        }

        if((-hedef.z + bas.position.z) < 0){
            for (int i = 1; i < noktalar.Count; i++)
            {
                if(((noktalar[i].x - hedef.x) > -2 && (noktalar[i].x - hedef.x) < 2) && noktalar[i].z < 0){
                    Nav.Add(noktalar[i]);
                }
            }
        }
        else{
            for (int i = 1; i < noktalar.Count; i++)
            {
                if(((noktalar[i].x - hedef.x) > -2 && (noktalar[i].x - hedef.x) < 2) && noktalar[i].z > 0){
                    Nav.Add(noktalar[i]);
                }
            }
        }

        for (int j = 1; j < noktalar.Count; j++)
        {
            if(((noktalar[j].z - Nav[1].z) > -2 && (noktalar[j].z - Nav[1].z) < 2) && ((noktalar[j].x - bas.position.x) > -2 && (noktalar[j].x - bas.position.x) < 2)){
                Nav.Add(noktalar[j]);
            }
        }

        return Nav; 
    }
}