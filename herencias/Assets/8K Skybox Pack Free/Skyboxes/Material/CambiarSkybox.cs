using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarSkybox : MonoBehaviour
{
    public Material[] skyboxMaterials;
    public float tiempoEntreCambio = 10.0f;

    private int currentIndex = 0;
    private float tiempoUltimoCambio = 0.0f;

    void Start()
    {
        // Establecemos el primer skybox
        RenderSettings.skybox = skyboxMaterials[currentIndex];
    }

    void Update()
    {
        // Verificamos si ha pasado suficiente tiempo para cambiar el skybox
        if (Time.time - tiempoUltimoCambio > tiempoEntreCambio)
        {
            // Incrementamos el índice del material
            currentIndex++;

            // Si llegamos al final del array, volvemos al primer material
            if (currentIndex >= skyboxMaterials.Length)
            {
                currentIndex = 0;
            }

            // Cambiamos el material del skybox
            RenderSettings.skybox = skyboxMaterials[currentIndex];

            // Actualizamos el tiempo del último cambio
            tiempoUltimoCambio = Time.time;
        }
    }
}
