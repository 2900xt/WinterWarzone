using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    void Update(){
        Debug.Log("Hello Snowball here :-");
    }
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collided :)");
    }
}
