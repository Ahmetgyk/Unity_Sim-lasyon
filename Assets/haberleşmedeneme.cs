using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class haberleşmedeneme : MonoBehaviour
{
    public GameObject arac;

    bool bitti;

    int veriii = 50;

    private void Start() {
        veriii = 50;
        veri.verii = veriii;
        bitti = true;
    }
    void Update()
    {
        if (bitti){
            StartCoroutine(bekle());
            bitti = false;
        }

        
        if(Input.GetKeyDown(KeyCode.Q)){

            haberleşme.load();

            Debug.Log(veri.haritaX);
            
        }
    }

    
    IEnumerator bekle(){
        veriii ++;
        Debug.Log(veriii);

        veri.x = arac.transform.position.x;
        veri.z = arac.transform.position.z;
        veri.haritalama = veriii;
        veri.haritaX = arac.transform.position.x + 10;
        veri.haritaY = arac.transform.position.z + 10;

        haberleşme.save2();

        yield return new WaitForSecondsRealtime(0.2f);

        bitti = true;
    }
}
