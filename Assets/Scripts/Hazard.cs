using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision){
        if(collision !=null){
            if(collision.collider.CompareTag("Player")){
                Debug.Log("You're TOAST!");
            }
            if(collision.collider.GetComponent<IDamageable>()!=null){
                collision.collider.GetComponent<IDamageable>().OnTakeDamage(999);
            }
        }
    }
}
