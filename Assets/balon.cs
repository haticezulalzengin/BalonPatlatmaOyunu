using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class balon : MonoBehaviour
{
    float hiz;
    Color[] colors;

    yonetici yonetici = new yonetici();
    MeshRenderer renk;
    public bool patlatildi = false;

    private void OnMouseDown()
    {         
        patlatildi = true;
        yonetici.ses.Play();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        yonetici = GameObject.Find("yonetici").GetComponent<yonetici>();
        renk = gameObject.GetComponent<MeshRenderer>();

        hiz = yonetici.balon_hizi;

        renk_degisimi();

        CancelInvoke("sil");

        Invoke("sil", 3.0f);
    }

    [System.Obsolete]
    private void OnDisable()
    {
        if (patlatildi)
        {

            foreach(GameObject efekt in yonetici.patlama_efektleri_listesi)
            {
                if (efekt.activeSelf ==false)
                {
                    efekt.SetActive(true);
                    efekt.transform.position= transform.position;
                    efekt.GetComponent<ParticleSystem>().startColor=renk.material.color;
                    break;
                }
            }

            if (renk.material.color == colors[0])
            {
                yonetici.saniye_degistir(-1);
                yonetici.skor_degistir(-10);
            }
            else
            {
                yonetici.saniye_degistir(1);
                yonetici.skor_degistir(10);
            }
            patlatildi=false;
        }        
    }
    void renk_degisimi()
    {
        colors = new Color[4];
        colors[0] = Color.red;
        colors[1] = Color.blue;
        colors[2] = Color.green;
        colors[3] = Color.yellow;

        int rand = Random.Range(0, colors.Length);

        renk.material.color = colors[rand];
    }

    void sil()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.Translate(0, -hiz*Time.deltaTime,0);
    }
}
