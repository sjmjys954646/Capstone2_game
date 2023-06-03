using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPortal : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnPos;
    [SerializeField]
    private GameObject drWorldHouse;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.player.transform.position = spawnPos.transform.position;
            drWorldHouse.SetActive(false);
            GameManager.Instance.isInDRWorldHouse = false;
            GameManager.Instance.isOnGround = true;
        }
    }
}
