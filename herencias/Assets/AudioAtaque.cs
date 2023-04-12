using UnityEngine;

public class AudioAtaque : MonoBehaviour
{
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;

    private AudioSource audioSource;
    private float timeSinceLastClip;
    private bool isClip2Playing;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timeSinceLastClip = Time.time;
        isClip2Playing = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            audioSource.clip = clip1;
            audioSource.Play();
            timeSinceLastClip = Time.time;
            isClip2Playing = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isClip2Playing)
            {
                audioSource.Stop();
                timeSinceLastClip = Time.time;
            }
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            audioSource.clip = clip3;
            audioSource.Play();
            timeSinceLastClip = Time.time;
            isClip2Playing = false;
        }

        if (Time.time - timeSinceLastClip > 0.6f && audioSource.isPlaying)
        {
            audioSource.Stop();
            isClip2Playing = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isClip2Playing)
        {
            audioSource.clip = clip2;
            audioSource.Play();
            isClip2Playing = true;
        }
    }
}
