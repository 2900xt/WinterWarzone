using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] public float health = 50f;

    public void TakeDamage(float damage){
        health -= damage;
        if(health <= 0){
            Die();
        }
    }

    void Die(){
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 100, 0));
        Debug.Log("Fried");
    }
}
