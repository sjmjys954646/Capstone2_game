using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    //F������ �ǻ���� �õ���
    public bool isInteracting;
    //��ȭ��
    public bool isTalking;

    [SerializeField]
    private int health = 100;

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isInteracting = true;
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            isInteracting = false;
        }

    }

    public void getDamage(int val)
    {
        Health -= val;
    }

}
