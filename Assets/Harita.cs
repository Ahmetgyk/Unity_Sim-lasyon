using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harita : MonoBehaviour
{
    public List<Transform> points;
    public List<Vector3> noktalar = new List<Vector3>();
    public GameObject araç;
    public List<GameObject> çizgiS;

    Vector3 baslanic;
    public bool haritala;

    Vector3 nokta;
    string isim;

    public Transform hed;
    public Transform bas;
    public List<Vector3> Nav = new List<Vector3>();
    LineRenderer lr;

    private void Start() {
        baslanic = araç.transform.position;
        haritala = true;

        lr = gameObject.GetComponent<LineRenderer>();
    }
    void Update()
    {
        if(!haritala && hed != null){
            Nav.Clear();

            nav(hed.position);

            lr.positionCount = Nav.Count + 1;
            lr.SetPosition(0, bas.position);
            for (int i = 0; i < Nav.Count; i++)
            {
                lr.SetPosition(i + 1, Nav[Nav.Count - 1 - i]);
            }
        }


        if(noktalar.Count > 3){
            if(Vector3.Distance(baslanic, araç.transform.position) < 2f){
                haritala = false;
            }
        }

        if(haritala){
            for (int i = 0; i < points.Count; i++)
            {
                if(Vector3.Distance(points[i].position, araç.transform.position) < 2f){
                    isim = points[i].name;
                }
            }

            for (int s = 0; s < çizgiS.Count; s++)
            {
                if(çizgiS[s].GetComponent<ÇizgiIzleme>().deydi){
                    nokta = new Vector3(Mathf.Round(araç.transform.position.x), Mathf.Round(araç.transform.position.y), Mathf.Round(araç.transform.position.z));
                    
                    if(!noktalar.Contains(nokta)){
                        if(noktalar.Count != 0){
                            if(Vector3.Distance(noktalar[noktalar.Count - 1], nokta) > 2){
                                noktalar.Add(nokta);
                                break;
                            }
                        }
                        else{
                            noktalar.Add(nokta);
                        }
                    }
                }
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