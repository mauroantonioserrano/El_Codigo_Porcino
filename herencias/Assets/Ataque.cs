using UnityEngine;

public class Ataque : MonoBehaviour
{
    private Transform playerTransform;
    private Animator anim;
    private bool isAttacking = false;
    private int hits = 0;
    public GameObject particlePrefab;
    public float radius = 10f;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Si se presiona el bot�n izquierdo del rat�n, el cerdo ataca
        if (Input.GetMouseButtonDown(0))
        {
            isAttacking = true;
            anim.SetBool("ataque", true);

            // Detectar todos los objetos dentro del radio del cerdo
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider collider in colliders)
            {
                // Si el objeto detectado es un lobo, el cerdo lo ataca
                if (collider.CompareTag("Lobo"))
                {
                    hits++; // Incrementar el contador de golpes
                    if (hits == 6)
                    {
                        // Mostrar la part�cula en la posici�n del lobo
                        Instantiate(particlePrefab, collider.transform.position, Quaternion.identity);
                        // Eliminar el lobo
                        Destroy(collider.gameObject);
                        // Reiniciar el contador de golpes
                        hits = 0;
                    }
                }
            }
        }

        // Si el cerdo deja de atacar, se desactiva la animaci�n
        if (Input.GetMouseButtonUp(0))
        {
            isAttacking = false;
            anim.SetBool("ataque", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Dibujar un c�rculo en el editor para mostrar el radio de detecci�n del cerdo
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
