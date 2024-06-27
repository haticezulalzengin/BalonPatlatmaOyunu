using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class yonetici : MonoBehaviour
{
    public GameObject balon;
    public Text skor_txt;
    public Text saniye_txt;

    int skor = 0;
    int saniye = 5;

    List<GameObject> balonlar;

    public AudioSource ses;

    public GameObject yeniden_oyna_pnl;

    public float balon_hizi = 5.0f;
    float balon_ekleme_hizi = 1.0f;

    public GameObject patlama_efekti;
    public List<GameObject> patlama_efektleri_listesi;


    void Start()
    {
        balonlar = new List<GameObject>();
        patlama_efektleri_listesi = new List<GameObject>();
        skor_txt.text = skor.ToString();
        saniye_txt.text = saniye.ToString();

        for (int i=0; i<10;i++)
        {
            GameObject efekt = Instantiate(patlama_efekti);
            patlama_efektleri_listesi.Add(efekt);
            efekt.SetActive(false);
        }


        for (int i = 0; i < 20.0f; i++)
        {
            float rast= Random.Range(-3.5f, 3.5f);

            GameObject y_balon = Instantiate(balon, new Vector3(rast,0,1.0f), Quaternion.Euler(0,0,180.0f));

            balonlar.Add(y_balon);
            y_balon.SetActive(false);
        }

        InvokeRepeating("balon_goster", 0.0f, balon_ekleme_hizi);
        InvokeRepeating("saniye_goster", 0.0f, 1.0f);
        InvokeRepeating("zorlugu_artir", 30.0f, 30.0f);
    }

    void zorlugu_artir()
    {
        balon_ekleme_hizi -= 0.1f;
        balon_hizi += 1.0f;

        if (balon_ekleme_hizi <= 0.2f)
            balon_ekleme_hizi = 0.2f;

        if (balon_hizi >= 10.0f)
            balon_hizi = 10.0f;

        CancelInvoke("balon_goster");
        InvokeRepeating("balon_goster", 0.0f, balon_ekleme_hizi);

    }

    public void skor_degistir(int deger)
    {
        skor += deger;
        skor_txt.text = skor.ToString();
    }

    public void saniye_degistir(int deger)
    {
        saniye += deger;
        saniye_txt.text = saniye.ToString();
    }

    void saniye_goster()
    {
        saniye--;
        saniye_txt.text = saniye.ToString();

        if (saniye <= 0)
        {
            yeniden_oyna_pnl.SetActive(true);
            Time.timeScale = 0.0f;
        }
            
    }
    void balon_goster()
    {
        foreach(GameObject bln in balonlar)
        {
            if(bln.activeSelf == false)
            {
                bln.SetActive(true);
                float rast = Random.Range(-3.5f, 3.5f);

                bln.transform.position = new Vector3(rast,-3.0f,1.0f);

                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.touchCount>0)
        //{
        //    Touch touch = Input.GetTouch(0);
        //    RaycastHit nesne;

        //    if (Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out nesne))
        //    {
        //        if (nesne.collider.tag == "balon")
        //        {
        //            ses.Play();
        //            nesne.collider.GetComponent<balon>().patlatildi = true;
        //            nesne.collider.gameObject.SetActive(false);
                    

        //        }
        //    }
           
        //}
    }

    public void yeniden_oyna_btn()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }
}
