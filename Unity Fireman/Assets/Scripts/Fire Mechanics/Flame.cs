using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    private float flameLife;
    //Referencia para destruir el objeto un frame luego de ser destruido
    public bool extinguished;
    public bool destroyNextFrame;

    // Start is called before the first frame update
    void Start()
    {
        extinguished = false;
        destroyNextFrame = false;
        flameLife = 20;
        //flameParticles = GameObject.Find("Fire_Particles");
        //transform.localScale = new Vector3(Random.Range(0.4f, 1f), Random.Range(.9f, 1.5f), Random.Range(0.5f, 1.1f));
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyNextFrame)
        {
            Destroy(gameObject);
        }
        if (extinguished)
        {
            destroyNextFrame = true;
        }
    }

    /*public void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            Debug.Log("Recibe daño");
            other.GetComponent<PlayerController>().TakeDamage(1f);
        }
    }*/

    public void GetsWater(float waterQuantity)
    {
        flameLife -= waterQuantity;
        if(flameLife <= 0)
        {
            //Esto es super janky, si se destruye el objeto, no cuenta como que salga de la colision, entonces lo que hago es moverlo muy hacia arriba, desactivar el mesh, y activar una bandera
            //para que se destruya el siguiente frame. Esto no pasaria si esta parte de unity estuviera bien programada.
            //Cuando uno elmina el objeto el sistema deberia asegurarse de sacarlo de todas las collisones, pero no, esta mal diseñado, como Dark Souls
            Debug.Log("Fire Extinguished");
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 4000, transform.position.z);
            //flameParticles.gameObject.SetActive(false);
            Invoke("Extinguish", 0.1f);
        }
    }

    public void Extinguish()
    {
        extinguished = true;
    }
}


