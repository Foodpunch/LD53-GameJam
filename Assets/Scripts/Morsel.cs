using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morsel : MonoBehaviour
{
    float spawnTime;
    Transform playerTransform;
    public bool isPlayerSpawned = false;
    Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = PlayerMovement.instance.transform;
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddTorque(Random.Range(-12f,12f),ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position,PlayerMovement.instance.transform.position) <= 0.2f){
            gameObject.SetActive(false);
        }
        spawnTime+=Time.deltaTime;
        if(spawnTime >= 1f && !isPlayerSpawned){
            transform.position += (playerTransform.position - transform.position).normalized * Time.deltaTime * 5f;
        }
    }
}
