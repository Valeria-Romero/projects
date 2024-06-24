using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sponge : MonoBehaviour
{
    public float maxScaleX;
    public float maxScaleY;
    public float maxScaleZ;

    private float infaltionRateX;
    private float infaltionRateY;
    private float infaltionRateZ;
    // Start is called before the first frame update
    void Start()
    {
        infaltionRateX = 0.01f * maxScaleX;
        infaltionRateY = 0.01f * maxScaleY;
        infaltionRateZ = 0.01f * maxScaleZ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetsWater(float waterQuantity)
    {
        if(maxScaleX >= this.transform.localScale.x)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x + infaltionRateX, this.transform.localScale.y, this.transform.localScale.z);
        }
        if (maxScaleY >= this.transform.localScale.y)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y + infaltionRateY, this.transform.localScale.z);
        }
        if (maxScaleZ >= this.transform.localScale.z)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z + infaltionRateZ);
        }
    }
}
