using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStats : MonoBehaviour
{
    public float Damage = 0.01f;
    public float verticalRecoil = 2f;

    public void OnTriggerStay(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            //Vector3 playerActualMove = other.GetComponent<PlayerController>().moveDir;
            //float playerActualSpeed = other.GetComponent<PlayerController>().speed;
            //Vector3 playerNewMove = new Vector3(-playerActualMove.x * 2, -playerActualMove.y, -playerActualMove.z * 2);

            //other.GetComponent<PlayerController>().controller.Move(playerNewMove * playerActualSpeed * Time.deltaTime);
            //Debug.Log("Recibe daño");
            other.GetComponent<PlayerController>().TakeDamage(Damage);
        }
    }
}
