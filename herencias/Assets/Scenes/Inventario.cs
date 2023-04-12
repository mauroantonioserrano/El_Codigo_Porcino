using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    public List<GameObject> objetosDisponibles;
    public List<GameObject> objetosEnInventario;
    public int objetoActual = 0;

    void Update()
    {
        // Agregar objeto al inventario al presionar el número 1
        if (Input.GetKeyDown(KeyCode.Alpha1) && objetoActual < objetosDisponibles.Count)
        {
            objetosEnInventario.Add(objetosDisponibles[objetoActual]);
            objetoActual++;
        }

        // Cambiar objeto en el inventario con la rueda del mouse
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0 && objetosEnInventario.Count > 1)
        {
            // Actualizar índice de objeto actual en el inventario
            if (scroll > 0)
            {
                objetoActual--;
                if (objetoActual < 0)
                {
                    objetoActual = objetosEnInventario.Count - 1;
                }
            }
            else if (scroll < 0)
            {
                objetoActual++;
                if (objetoActual >= objetosEnInventario.Count)
                {
                    objetoActual = 0;
                }
            }
        }
    }
}
