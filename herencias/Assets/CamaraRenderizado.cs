using UnityEngine;

public class CamaraRenderizado : MonoBehaviour
{
    public float maxDistance = 10f; // distancia m�xima de renderizado
    public float smoothTime = 3f; // tiempo de suavizado en segundos
    private Camera mainCamera;
    private float currentDistance;
    private float velocity;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
        // obtener todos los objetos en la escena
        GameObject[] objects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in objects)
        {
            // si el objeto est� dentro del campo de visi�n de la c�mara
            if (mainCamera.WorldToViewportPoint(obj.transform.position).z > 0
                && mainCamera.WorldToViewportPoint(obj.transform.position).x > 0
                && mainCamera.WorldToViewportPoint(obj.transform.position).x < 1
                && mainCamera.WorldToViewportPoint(obj.transform.position).y > 0
                && mainCamera.WorldToViewportPoint(obj.transform.position).y < 1)
            {
                // calcular la distancia entre la c�mara y el objeto
                float distance = Vector3.Distance(transform.position, obj.transform.position);

                // si la distancia es mayor que la distancia m�xima de renderizado
                if (distance > maxDistance)
                {
                    // desactivar el renderer del objeto
                    Renderer renderer = obj.GetComponent<Renderer>();
                    if (renderer != null) renderer.enabled = false;
                }
                else
                {
                    // activar el renderer del objeto
                    Renderer renderer = obj.GetComponent<Renderer>();
                    if (renderer != null) renderer.enabled = true;
                }
            }
            else
            {
                // desactivar el renderer del objeto si no est� en el campo de visi�n de la c�mara
                Renderer renderer = obj.GetComponent<Renderer>();
                if (renderer != null) renderer.enabled = false;
            }
        }

        // suavizar el efecto de no renderizado
        currentDistance = Mathf.SmoothDamp(currentDistance, maxDistance, ref velocity, smoothTime);
        mainCamera.farClipPlane = currentDistance;

    }

    // dibujar un gizmo en la escena para controlar el radio de renderizado
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
}
