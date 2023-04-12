using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventosUnity : MonoBehaviour
{
    public void ActivarObjetos(GameObject obj)
    {
        obj.SetActive(true);
        Debug.Log("Activar Objeto en la esena");
    }

    public void CambiarColor(GameObject obj)
    {
        obj.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
        Debug.Log("Cambiar el color a negro");
    }

    public void DesactivarObjeto(GameObject obj)
    {
        obj.SetActive(false);
        Debug.Log("Desactivar un objeto");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
