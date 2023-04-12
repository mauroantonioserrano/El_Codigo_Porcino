using UnityEngine;

public class CamaraTerceraPersona : MonoBehaviour
{
    public Transform objetivo;
    public float distancia = 6.0f;
    public float altura = 2.0f;
    public float velocidadRotacion = 3.0f;
    public float suavidad = 0.3f;
    public float sensibilidadMouse = 1.0f;
    public float velocidadMovimiento = 5.0f;
    public float suavidadMovimiento = 0.1f;
    public float damping = 1.0f;

    private Vector3 offset;
    private Vector3 objetivoPosicion;
    private Vector3 movimientoSuavizado;

    void Start()
    {
        offset = new Vector3(0, altura, -distancia);
        objetivoPosicion = objetivo.position + offset;
    }

    void FixedUpdate()
    {
        // Calcular la posición objetivo de la cámara en función de la posición del personaje
        Quaternion rotacionObjetivo = Quaternion.Euler(0, objetivo.eulerAngles.y, 0);
        Vector3 posicionObjetivo = objetivo.position + (rotacionObjetivo * offset);

        // Suavizar la transición entre la posición actual de la cámara y la posición objetivo
        Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionObjetivo, Time.deltaTime * damping);

        // Actualizar la posición y rotación de la cámara
        transform.position = posicionSuavizada;
        transform.LookAt(objetivo);

        // Rotar la cámara alrededor del personaje en función del input del jugador y la sensibilidad del mouse
        float rotacionHorizontal = Input.GetAxis("HorizontalCamara") * velocidadRotacion * sensibilidadMouse;
        objetivo.eulerAngles += new Vector3(0, rotacionHorizontal, 0);

        // Mover al personaje en función del input del jugador y la velocidad de movimiento
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");
        Vector3 movimiento = new Vector3(movimientoHorizontal, 0, movimientoVertical);
        movimiento = Vector3.ClampMagnitude(movimiento, 1f);
        movimiento *= velocidadMovimiento * Time.deltaTime;

        // Suavizar el movimiento del personaje
        movimientoSuavizado = Vector3.Lerp(movimientoSuavizado, movimiento, suavidadMovimiento);

        // Convertir el movimiento relativo a la rotación de la cámara en movimiento relativo al mundo
        movimientoSuavizado = Quaternion.Euler(0, objetivo.eulerAngles.y, 0) * movimientoSuavizado;

        // Aplicar el movimiento al personaje
        objetivo.GetComponent<Rigidbody>().MovePosition(objetivo.position + movimientoSuavizado);
    }
}

