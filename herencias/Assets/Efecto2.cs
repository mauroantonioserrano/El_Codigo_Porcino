using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Efecto2 : MonoBehaviour
{
    public PostProcessVolume volumen;
    private Vignette viñeta;
    private ColorGrading colores;
    // Start is called before the first frame update
    void Start()
    {
        volumen.profile.TryGetSettings(out viñeta);
        volumen.profile.TryGetSettings(out colores);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            viñeta.intensity.value = 0.50f;
            viñeta.smoothness.value = 0.65f;
            colores.contrast.value = -40;
        }
    }
}
