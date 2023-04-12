using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvaDeJugar : MonoBehaviour
{
    private bool pausaActive;
    public GameObject menuJugar;
    public GameObject menuPausa;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TooglePause();
    }

    void TooglePause()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(pausaActive == true)
            {
                ResumirJuego();
            }
            else
            {
                PausarJuego();
            }
        }
    }

    void ResumirJuego()
    {
        menuPausa.SetActive(false);
        menuJugar.SetActive(true);
        pausaActive = false;
        Time.timeScale = 1;
    }

    void PausarJuego()
    {
        menuPausa.SetActive(true);
        menuJugar.SetActive(false);
        pausaActive = true;
        Time.timeScale = 0;
    }
}
