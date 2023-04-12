using UnityEngine;

public class Lobo : MonoBehaviour
{
    public float velocidadLenta = 3f;
    public float velocidadRapida = 6f;
    public float distanciaDeteccion = 10f;
    public float tiempoEntreVueltas = 3f;
    public float tiempoAntesDesaparicion = 20f;
    public float tiempoEntreOvejas = 120f;
    public float tiempoEntreOvejasMaximo = 900f;

    public GameObject puntoLobo;

    private GameObject ovejaCercana;
    private GameObject[] ovejas;

    private bool loboMoviendo = true;
    private float tiempoUltimaVuelta = 0f;
    private float tiempoOvejaActual = 0f;

    private Quaternion targetRotation;

    void Start()
    {
        // Obtener todas las ovejas con el tag "Oveja"
        ovejas = GameObject.FindGameObjectsWithTag("Oveja");
    }

    void Update()
    {
        if (loboMoviendo)
        {
            // Detectar la oveja más cercana
            DetectarOvejaCercana();

            // Si hay una oveja cercana, moverse hacia ella
            if (ovejaCercana != null)
            {
                // Calcular la distancia a la oveja
                float distancia = Vector3.Distance(transform.position, ovejaCercana.transform.position);

                // Calcular la velocidad a la que se moverá el lobo
                float velocidadActual = distancia > 2f ? velocidadLenta : velocidadRapida;

                // Moverse hacia la oveja
                targetRotation = Quaternion.LookRotation(ovejaCercana.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // Rotar suavemente hacia la dirección de la oveja
                transform.position = Vector3.MoveTowards(transform.position, ovejaCercana.transform.position, velocidadActual * Time.deltaTime);


                // Si el lobo está cerca de la oveja, instanciarla en su mano y detener su movimiento
                if (distancia < 3f)
                {
                    ovejaCercana.GetComponent<Rigidbody>().isKinematic = true; // desactivar rigidbody de la oveja

                    ovejaCercana.GetComponent<Seguir>().enabled = false; // desactivar script Seguir de la oveja
                    ovejaCercana.transform.position = puntoLobo.transform.position;
                    ovejaCercana.transform.parent = puntoLobo.transform;
                    loboMoviendo = false;
                    tiempoOvejaActual = Time.time;

                    // Dejar de seguir la oveja
                    Seguir seguirScript = ovejaCercana.GetComponent<Seguir>();
                    if (seguirScript != null)
                    {
                        seguirScript.enabled = false;
                    }
                }

            }
        }
        else
        {
            // Si el lobo no está moviendo, esperar para dar la vuelta
            if (Time.time - tiempoUltimaVuelta > tiempoEntreVueltas)
            {
                // Dar la vuelta y volver a moverse aleatoriamente
                targetRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
                loboMoviendo = true;
                tiempoUltimaVuelta = Time.time;
            }
        }

        if (puntoLobo.transform.childCount > 0 && Time.time - tiempoOvejaActual > tiempoAntesDesaparicion)
        {
            // Desaparecer la oveja y resetear el movimiento del lobo
            Destroy(puntoLobo.transform.GetChild(0).gameObject);
            loboMoviendo = true;
            tiempoUltimaVuelta = Time.time;
            tiempoOvejaActual = Time.time + Random.Range(tiempoEntreOvejas, tiempoEntreOvejasMaximo);
        }
    }

    void DetectarOvejaCercana()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, distanciaDeteccion);

        GameObject nuevaOvejaCercana = null;
        float distanciaMenor = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Oveja")
            {
                float distanciaActual = Vector3.Distance(transform.position, collider.transform.position);

                if (distanciaActual < distanciaMenor)
                {
                    nuevaOvejaCercana = collider.gameObject;
                    distanciaMenor = distanciaActual;
                }
            }
        }

        // Verifica si la oveja cercana sigue existiendo antes de asignarla
        if (ovejaCercana != null && ovejaCercana == nuevaOvejaCercana)
        {
            return;
        }

        ovejaCercana = nuevaOvejaCercana;
    }
}
