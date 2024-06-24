using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrops : MonoBehaviour
{
    /*
    private float timer;
    private Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        rigidBody = gameObject.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        rigidBody.AddForce(new Vector3(0.0f, -1f, 0.0f));
        timer += Time.deltaTime;
        if (timer >= 3f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("Water"))
        {
            if (other.tag.Equals("Fire"))
            {
                other.GetComponent<Flame>().GetsWater(0.5f);
            }
            Destroy(this.gameObject);
        }
    }
    */
    /////////////////////////////////////////////////////

    void OnParticleCollision(GameObject other)
    {
        //Revisa si el otro objeto es una objeto de fuego
        Flame hasFlameScript = other.GetComponent<Flame>();
        if (hasFlameScript != null)
        {
            Debug.Log("Particle to Flame");
            other.GetComponent<Flame>().GetsWater(5f);
        }

        //Revisa si el otro objeto es una objeto de esponja
        Sponge hasSpongeScript = other.GetComponent<Sponge>();
        if (hasSpongeScript != null)
        {
            Debug.Log("Particle to Sponge");
            other.GetComponent<Sponge>().GetsWater(5f);
        }

        //Revisa si el otro objeto es una objeto movil
        MovableObjectHitBoxScript hasMovableObjectHitBoxScript = other.GetComponent<MovableObjectHitBoxScript>();
        if (hasMovableObjectHitBoxScript != null)
        {
            Debug.Log("Particle to Movable Object");
            other.GetComponent<MovableObjectHitBoxScript>().GetsWater(5f);
        }
    }
} 