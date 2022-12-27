using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harita : MonoBehaviour
{
    public List<Transform> points;
    public List<Vector3> noktalar = new List<Vector3>();
    public GameObject araç;
    public List<GameObject> çizgiS;

    public Transform parent;
    public GameObject duvar;
    public bool ciz;

    Vector3 baslanic;
    public bool haritala;

    Vector3 nokta;
    string isim;

    public Transform hed;
    public Vector3 bas;
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

            bas = araç.transform.position;
            nav(hed.position, bas);

            lr.positionCount = Nav.Count + 1;
            lr.SetPosition(0, bas);
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

        if(ciz && !haritala){
            for (int i = 0; i < noktalar.Count; i++)
            {
                GameObject noktaG =  Instantiate(duvar, noktalar[i], Quaternion.identity);
                noktaG.transform.parent = parent;
            }
            ciz = false;
        }
    }

    public List<Vector3> nav(Vector3 hedef, Vector3 baslangic){
        Nav.Add(hedef);

        if((baslangic.z - hedef.z) < 1){
            araç.GetComponent<Kontrol>().hareket(0.1f);
        }

        if((-hedef.z + baslangic.z) < 0){
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
            if(((noktalar[j].z - Nav[1].z) > -2 && (noktalar[j].z - Nav[1].z) < 2) && ((noktalar[j].x - baslangic.x) > -2 && (noktalar[j].x - baslangic.x) < 2)){
                Nav.Add(noktalar[j]);
            }
        }

        return Nav; 
    }
}