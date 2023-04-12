using UnityEngine;

public class CameraSmoothFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;
    public float delayTime = 0.5f;

    private Vector3 velocity = Vector3.zero;
    private Transform parentTransform;

    void Start()
    {
        parentTransform = new GameObject("Camera Container").transform;
        parentTransform.position = transform.position;
        transform.parent = parentTransform;
    }

    void LateUpdate()
    {
        // Esperar un tiempo antes de comenzar a suavizar el movimiento
        if (Time.time < delayTime) return;

        // Calcular la posición y rotación objetivo
        Vector3 targetPosition = target.position;
        Quaternion targetRotation = target.rotation;

        // Suavizar la transición entre la posición y rotación actual y la posición y rotación objetivo
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        Quaternion smoothRotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothTime);

        // Actualizar la posición y rotación de la cámara
        transform.position = smoothPosition;
        transform.rotation = smoothRotation;

        // Actualizar la posición del objeto padre para que siga a la cámara
        parentTransform.position = Vector3.Lerp(parentTransform.position, targetPosition, smoothTime);
    }
}
