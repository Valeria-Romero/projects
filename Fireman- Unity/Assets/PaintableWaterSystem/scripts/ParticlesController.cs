using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesController: MonoBehaviour
    {
    public float minRadius = 0.05f;
    public float maxRadius = 0.2f;
    public float strength = 1;
    public float hardness = 1;
    public Color color = Color.white;

    [Space]
    public Texture2D splash;
    [Space]
    ParticleSystem part;
    List<ParticleCollisionEvent> collisionEvents;

    void Start(){
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other) {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        Paintable paintable = other.GetComponent<Paintable>();
        
        if(paintable != null){
            for  (int i = 0; i< numCollisionEvents; i++){
                Vector3 pos = collisionEvents[i].intersection;
                float radius = Random.Range(minRadius, maxRadius);
                PaintManager.instance.paint(paintable, pos, radius, hardness, strength, color);
            }
        }


        //WIP
        WaterPaintable waterPaintable = other.GetComponent<WaterPaintable>();
        if (waterPaintable != null)
        {
            for (int i = 0; i < numCollisionEvents; i++)
            {
                Vector3 position = collisionEvents[i].intersection;
                Vector3 direction = collisionEvents[i].velocity;
                other.GetComponent<WaterPaintable>().Painting(position, direction);
            }
        }

    }
}