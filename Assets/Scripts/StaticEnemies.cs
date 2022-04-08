using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemies : MonoBehaviour
{
    private Rigidbody2D assEater;

    [SerializeField]
    Vector2[] jackArray;

    [SerializeField]
    int howManyjack;

    void Start()
    {
        assEater = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
    }
}
