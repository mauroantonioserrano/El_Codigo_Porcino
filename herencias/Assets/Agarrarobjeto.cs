using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agarrarobjeto : MonoBehaviour
{
    public GameObject puntomano;
    private GameObject objetoagarrado;
    private bool isTriggerActivated = true;
    public float lanzamientoFuerza = 500f;
    private Dictionary<KeyCode, GameObject> teclaObjeto = new Dictionary<KeyCode, GameObject>();
    private List<GameObject> inventario = new List<GameObject>();
    private int inventarioActivo = -1;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            foreach (GameObject objeto in inventario)
            {
                inventario.Remove(objeto);
                objeto.SetActive(true);
            }
            inventarioActivo = -1;
        }

        // Agregar objeto al inventario al presionar "I"
        if (objetoagarrado != null && Input.GetKeyDown(KeyCode.I))
        {
            if (inventario.Count < 9)
            {
                int indice = inventario.Count;
                inventario.Add(objetoagarrado);
                objetoagarrado.SetActive(false);
                objetoagarrado = null;
                isTriggerActivated = true;

                if (indice < 9)
                {
                    teclaObjeto[(KeyCode)((int)KeyCode.Alpha1 + indice)] = inventario[indice];
                }
            }
        }

        // Mostrar inventario al presionar "1" a "9"
        for (int i = 1; i <= 9; i++)
        {
            KeyCode keyCode = (KeyCode)((int)KeyCode.Alpha0 + i);
            if (teclaObjeto.ContainsKey(keyCode) && Input.GetKeyDown(keyCode))
            {
                GameObject objeto = teclaObjeto[keyCode];
                objeto.SetActive(true);
            }
        }

        // Quitar objeto del inventario al presionar "O"
        if (inventario.Count > 0 && Input.GetKeyDown(KeyCode.O))
        {
            // Crear una lista temporal y copiar los objetos del inventario a ella
            List<GameObject> tempInventario = new List<GameObject>(inventario);

            // Obtener el objeto a quitar del inventario
            GameObject objeto = tempInventario[tempInventario.Count - 1];

            // Eliminar el objeto del inventario
            inventario.RemoveAt(inventario.Count - 1);

            // Copiar los objetos de la lista temporal de vuelta al inventario
            inventario = new List<GameObject>(tempInventario);

            // Asignar el objeto agarrado y configurar su transformación
            objetoagarrado = objeto;
            objetoagarrado.SetActive(true);
            objetoagarrado.transform.SetParent(puntomano.transform);
            objetoagarrado.transform.localPosition = Vector3.zero;
            objetoagarrado.transform.localRotation = Quaternion.identity;
            isTriggerActivated = false;
        }


        // Crear una nueva lista para almacenar los objetos a eliminar
        List<GameObject> objetosAEliminar = new List<GameObject>();

        // Recorrer el inventario para verificar los objetos que se deben eliminar
        foreach (GameObject objeto in inventario)
        {
            if (objeto == null)
            {
                objetosAEliminar.Add(objeto);
            }
        }

        // Eliminar los objetos de la lista de inventario
        foreach (GameObject objeto in objetosAEliminar)
        {
            inventario.Remove(objeto);
        }



        // Soltar objeto al presionar "R"
        if (objetoagarrado != null && Input.GetKeyDown(KeyCode.R))
        {
            objetoagarrado.GetComponent<Rigidbody>().useGravity = true;
            objetoagarrado.GetComponent<Rigidbody>().isKinematic = false;
            objetoagarrado.transform.SetParent(null);
            objetoagarrado.GetComponent<Collider>().isTrigger = false;
            objetoagarrado = null;
            isTriggerActivated = true;
        }

        // Lanzar objeto al presionar "Y"
        if (objetoagarrado != null && Input.GetKeyDown(KeyCode.Y))
        {
            objetoagarrado.GetComponent<Rigidbody>().useGravity = true;
            objetoagarrado.GetComponent<Rigidbody>().isKinematic = false;
            objetoagarrado.transform.SetParent(null);
            objetoagarrado.GetComponent<Collider>().isTrigger = false;
            objetoagarrado.GetComponent<Rigidbody>().AddForce(transform.forward * lanzamientoFuerza, ForceMode.Impulse);
            objetoagarrado = null;
            isTriggerActivated = true;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (isTriggerActivated && other.gameObject.CompareTag("Objeto"))
        {
            if (Input.GetKeyDown(KeyCode.E) && objetoagarrado == null)
            {
                other.GetComponent<Rigidbody>().useGravity = false;
                other.GetComponent<Rigidbody>().isKinematic = true;
                other.transform.position = puntomano.transform.position;
                other.transform.SetParent(puntomano.gameObject.transform);
                objetoagarrado = other.gameObject;
                isTriggerActivated = false;
            }
        }
    }
}