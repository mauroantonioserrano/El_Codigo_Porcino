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

        // Calcular la posici�n y rotaci�n objetivo
        Vector3 targetPosition = target.position;
        Quaternion targetRotation = target.rotation;

        // Suavizar la transici�n entre la posici�n y rotaci�n actual y la posici�n y rotaci�n objetivo
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        Quaternion smoothRotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothTime);

        // Actualizar la posici�n y rotaci�n de la c�mara
        transform.position = smoothPosition;
        transform.rotation = smoothRotation;

        // Actualizar la posici�n del objeto padre para que siga a la c�mara
        parentTransform.position = Vector3.Lerp(parentTransform.position, targetPosition, smoothTime);
    }
}
