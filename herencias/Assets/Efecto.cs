using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Efecto : MonoBehaviour
{
    public PostProcessVolume volumen;
    private Grain granos;
    private ColorGrading colores;
    // Start is called before the first frame update
    void Start()
    {
        volumen.profile.TryGetSettings(out granos);
        volumen.profile.TryGetSettings(out colores);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            granos.intensity.value = 1;
            colores.temperature.value = -93;
        }
    }
}
