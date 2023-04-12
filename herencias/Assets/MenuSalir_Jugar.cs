using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSalir_Jugar : MonoBehaviour
{
    // Start is called before the first frame update
    public void IrJugar()
    {
        SceneManager.LoadScene(1);
    }

    public void Salir()
    {
        Application.Quit();
    }
}
