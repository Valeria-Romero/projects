using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpread : MonoBehaviour
{
    private float timerSpread;
    //[SerializeField]
    public int fuelNearby;
    //[SerializeField]
    public int fireNearby;

    private float spreadTime;

    public GameObject flamePrefab;
    public Transform flameSpawnPoint;

    void Start()
    {
        timerSpread = Random.Range(0,2);
        fuelNearby = 0;
        fireNearby = 0;
        spreadTime = Random.Range(10, 15);
    }

    // Update is called once per frame
    void Update()
    {
        timerSpread += Time.deltaTime;
        if(timerSpread >= spreadTime)
        {

            timerSpread = Random.Range(0, 2);
            spreadTime = Random.Range(10, 15);
            /*if (collider.activeSelf)
            {
                collider.SetActive(false);
            }
            else
            {
                collider.SetActive(true);
            }*/
            if (fuelNearby > 0 && fireNearby <= 0)
            {
                Instantiate(flamePrefab, flameSpawnPoint.position, new Quaternion(0,0,0,0));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Fire"))
        {
            //Debug.Log("Fire Enters");
            fireNearby++;
        }
        if (other.tag.Equals("Fuel"))
        {
            //Debug.Log("Fuel Enters: " + other.name);
            fuelNearby++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Fire"))
        {
            //Debug.Log("Fire Exits");
            fireNearby--;
        }
        if (other.tag.Equals("Fuel"))
        {
            //Debug.Log("Fuel Exits");
            fuelNearby--;
        }
    }
}
