using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seguir : MonoBehaviour
{
    public float velocidadPatrulla;
    public float velocidadPersecucion;
    public float distanciaDePersecucion;
    public float distanciaDeDetencion;
    public Transform[] puntosPatrulla;
    public float radioPatrulla;
    public AudioClip sonido;
    public AudioSource source;
    private int puntoActual = 0;
    private Transform jugador;
    private bool persiguiendo = false;
    private bool reproduciendo = false;
    private Rigidbody rb;

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        source.clip = sonido;
    }

    void Update()
    {
        if (!persiguiendo)
        {
            Patrullar();
        }
        else
        {
            Perseguir();
        }

        if (Input.GetKeyDown(KeyCode.C) && !reproduciendo)
        {
            source.Play();
            reproduciendo = true;
            StartCoroutine(EsperarFinSonido());
        }
    }

    void Patrullar()
    {
        Vector3 direccion = puntosPatrulla[puntoActual].position - transform.position;
        transform.LookAt(new Vector3(puntosPatrulla[puntoActual].position.x, transform.position.y, puntosPatrulla[puntoActual].position.z));
        rb.MovePosition(transform.position + direccion.normalized * velocidadPatrulla * Time.deltaTime);

        if (Vector3.Distance(transform.position, puntosPatrulla[puntoActual].position) < 0.3f)
        {
            puntoActual++;
            if (puntoActual >= puntosPatrulla.Length)
            {
                puntoActual = 0;
            }
        }

        // Comprobar si hay un punto de patrulla cercano
        for (int i = 0; i < puntosPatrulla.Length; i++)
        {
            if (i != puntoActual && Vector3.Distance(transform.position, puntosPatrulla[i].position) < radioPatrulla)
            {
                puntoActual = i;
            }
        }

        if (!reproduciendo && Vector3.Distance(transform.position, jugador.position) < distanciaDePersecucion)
        {
            persiguiendo = true;
        }
    }


    IEnumerator EsperarFinSonido()
    {
        while (source.isPlaying)
        {
            yield return null;
        }

        reproduciendo = false;

        if (!persiguiendo) // Si no se está persiguiendo al jugador, volver a la patrulla
        {
            puntoActual = 0;
        }
    }

    void Perseguir()
    {
        if (reproduciendo) // Si se está reproduciendo el sonido, la oveja se queda quieta
        {
            rb.velocity = Vector3.zero;
            return;
        }

        Vector3 direccion = jugador.position - transform.position;

        // Calcular rotación para mirar hacia el jugador
        Quaternion rotacionDeseada = Quaternion.LookRotation(direccion, Vector3.up);

        if (Vector3.Distance(transform.position, jugador.position) > distanciaDeDetencion)
        {
            // Girar hacia el jugador de forma suave
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada, Time.deltaTime * 2f);

            rb.MovePosition(transform.position + transform.forward * velocidadPersecucion * Time.deltaTime);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        if (Vector3.Distance(transform.position, jugador.position) >distanciaDePersecucion)
{
if (!source.isPlaying)
{
persiguiendo = false;
rb.velocity = Vector3.zero;
StartCoroutine(VolverAPatrullar());
}
}
}


IEnumerator VolverAPatrullar()
{
    yield return new WaitForSeconds(3f);
    if (!persiguiendo)
    {
        puntoActual = Random.Range(0, puntosPatrulla.Length);
    }
}

void OnDrawGizmosSelected()
{
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, distanciaDePersecucion);

    Gizmos.color = Color.blue;
    Gizmos.DrawWireSphere(transform.position, radioPatrulla);
}
}

