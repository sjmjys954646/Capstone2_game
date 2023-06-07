using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPortal : MonoBehaviour
{
    [SerializeField]
    private GameObject keyLockerSpawnPos;
    [SerializeField]
    private GameObject groundSpawnPos;
    [SerializeField]
    private GameObject drWorldHouse;
    [SerializeField]
    private GameObject keyLockerRoom;
    [SerializeField]
    private GameObject outSide;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if ( GameManager.Instance.isInDRWorldHouse)
            {
                keyLockerRoom.SetActive(true);
                StartCoroutine(startConverse());
                GameManager.Instance.player.transform.position = keyLockerSpawnPos.transform.position;          
                GameManager.Instance.isInDRWorldHouse = false;
                GameManager.Instance.isInKeyLockerRoom = true;
                return;
            }
            
            if(GameManager.Instance.isInKeyLockerRoom)
            {
                outSide.SetActive(true);
                GameManager.Instance.player.transform.position = groundSpawnPos.transform.position;
                keyLockerRoom.SetActive(false);
                GameManager.Instance.isInKeyLockerRoom = true;
                GameManager.Instance.isOnGround = true;
                return;
            }
        }
    }

    IEnumerator startConverse()
    {
        yield return new WaitForSeconds(0.2f);
        ScenarioManager.Instance.ConversationStart(5);
        drWorldHouse.SetActive(false);
    }
}
