using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public GameObject player;

    

    public void Print()
    {
        player.GetComponent<PlayerStatus>().isTalking = true;
        StartCoroutine("talking");
    }

    IEnumerator talking()
    {
        Debug.Log("I'm " + gameObject.name);
        yield return new WaitForSeconds(1.0f);
        player.GetComponent<PlayerStatus>().isTalking = false;
    }
}
