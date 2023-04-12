using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    public float tiempoInicial = 560f;
    private float tiempoRestante;
    public Text textoTiempo;

    // Start is called before the first frame update
    void Start()
    {
        tiempoRestante = tiempoInicial;
    }

    // Update is called once per frame
    void Update()
    {
        tiempoRestante -= Time.deltaTime;
        textoTiempo.text = "Tiempo restante: " + tiempoRestante.ToString("f0");

        if (tiempoRestante <= 0)
        {
            ReiniciarJuego();
        }
    }

    void ReiniciarJuego()
    {
        // Aquí puedes reiniciar el juego, por ejemplo:
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        tiempoRestante = tiempoInicial;
    }
}


