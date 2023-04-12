using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverCerdito : MonoBehaviour
{
    public float velocidad;
    public float velocidadCarrera;
    public float fuerzaSalto;
    public float fuerzaSaltoDoble;
    public int saltosRestantes = 2;
    public float tiempoEspera = 0.8f;
    private float tiempoUltimoSalto = 0.0f;
    private Rigidbody rb;
    private bool puedeSaltar = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Mover();
        Saltar();
        Rotar();
    }

    void Mover()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 mover = new Vector3(hor, 0, ver);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            mover = mover.normalized * velocidadCarrera * Time.deltaTime;
        }
        else
        {
            mover = mover.normalized * velocidad * Time.deltaTime;
        }

        transform.Translate(mover, Space.Self);
    }

    void Rotar()
    {
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseX * 50 * Time.deltaTime, 0);
    }

    void Saltar()
    {
        if (Input.GetKeyDown(KeyCode.Space) && puedeSaltar)
        {
            if (saltosRestantes > 0 && Time.time - tiempoUltimoSalto > tiempoEspera)
            {
                if (saltosRestantes == 2)
                {
                    rb.AddForce(transform.up * fuerzaSalto, ForceMode.Impulse);
                }
                else if (saltosRestantes == 1)
                {
                    rb.AddForce(transform.up * fuerzaSaltoDoble, ForceMode.Impulse);
                }

                saltosRestantes--;
                tiempoUltimoSalto = Time.time;
            }
        }

        if (Physics.Raycast(transform.position, Vector3.down, 1.1f))
        {
            saltosRestantes = 2;
            puedeSaltar = true;
        }
        else
        {
            puedeSaltar = false;
        }
    }
}
