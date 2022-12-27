using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haritalama : MonoBehaviour
{
    public Transform arac;
    public float dis = 49;
    public bool ciz;
    Transform sen;
    public List<GameObject> sensörler;
    public List<Vector3> noktalar = new List<Vector3>();

    public Transform parent;
    public GameObject duvar;

     Vector3 baslanic;
    public bool haritala;

    int i;

    void Start()
    {
        sensörler[0].GetComponent<Lidar>().calistir = false;
        sensörler[1].GetComponent<Lidar>().calistir = true;

        sen = sensörler[1].transform;
        i = 0;

        baslanic = transform.position;
        haritala = true;
    }
    void Update()
    {
        if(noktalar.Count > 20){
            if(Vector3.Distance(baslanic, transform.position) < 2f){
                haritala = false;
            }
        }

        if(haritala){
            float l = Vector3.Distance(sensörler[1].GetComponent<LineRenderer>().GetPosition(1), sensörler[1].GetComponent<LineRenderer>().GetPosition(0));
            float x = Mathf.Cos((arac.eulerAngles.y * Mathf.PI)/180);
            float z = -1 * Mathf.Sin((arac.eulerAngles.y * Mathf.PI)/180);

            if(l < 49) noktalar.Add(new Vector3(sen.position.x + (l * x), 0, sen.position.z + (l * z)));
            else if(noktalar.Count > 0 && noktalar[noktalar.Count - 1] != new Vector3(50, 50, 50)) noktalar.Add(new Vector3(50, 50, 50));

            if(noktalar.Count > i + 2){
                Debug.Log(i);
                if(noktalar[i] != new Vector3(50, 50, 50) && noktalar[i + 2] != new Vector3(50, 50, 50)){
                    float dx = (noktalar[i].x - noktalar[i + 2].x);
                    if(dx == 0) dx = 0.0000001f;

                    float m = (noktalar[i].z - noktalar[i + 2].z) / dx;
                    float n = noktalar[i].z - (m * noktalar[i].x);

                    if((noktalar[i + 1].z - (n + (m * noktalar[i + 1].x))) > -2 && (noktalar[i + 1].z - (n + (m * noktalar[i + 1].x))) < 2) noktalar.RemoveAt(i + 1);
                    else i = i + 1;
                }
                else i = i + 1;
            }
        }

        if(ciz && !haritala){
            for (int i = 0; i < noktalar.Count; i++)
            {
                if(noktalar[i] != new Vector3(50, 50, 50)){
                    GameObject noktaG = Instantiate(duvar, noktalar[i], Quaternion.identity);
                    noktaG.transform.parent = parent;
                }
            }
            ciz = false;
        }
    }
}
