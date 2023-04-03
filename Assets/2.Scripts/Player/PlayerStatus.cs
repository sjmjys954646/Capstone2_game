using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{

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
        
    }

    public void getDamage(int val)
    {
        Health -= val;
    }
}
