using UnityEngine;

public class Particula : MonoBehaviour
{
    [SerializeField] private Transform pigTransform;
    private bool isActive = false;
    private ParticleSystem particles;

    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isActive = true;
            transform.position = pigTransform.position;
            transform.rotation = pigTransform.rotation;
            particles.Play();
        }

        if (Input.GetMouseButtonUp(0))
        {
            Invoke("DeactivateParticle", 0.5f);
        }
    }

    private void DeactivateParticle()
    {
        isActive = false;
        particles.Stop();
    }
}
