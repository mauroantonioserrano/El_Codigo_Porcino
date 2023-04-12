using UnityEngine;

public class Cerdo : MonoBehaviour
{
    public ParticleSystem particula;
    public float fuerzaSalto = 5f;
    public int vidas = 4;

    private Rigidbody cerdoRigidbody;
    private Animator cerdoAnimator;

    void Start()
    {
        cerdoRigidbody = GetComponent<Rigidbody>();
        cerdoAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cerdoAnimator.SetTrigger("Ataque");
            particula.Play();
        }

        if (Input.GetMouseButtonUp(0))
        {
            cerdoAnimator.SetTrigger("FinAtaque");
            particula.Stop();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Objeto"))
        {
            vidas--;
            if (vidas <= 0)
            {
                Destroy(gameObject);
            }
        }
        else if (other.gameObject.CompareTag("Suelo"))
        {
            cerdoRigidbody.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }
    }
}
