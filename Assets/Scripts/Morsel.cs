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
        _rb.AddTorque(Random.Range(-1f,1f),ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime+=Time.deltaTime;
        if(spawnTime >= 1f && !isPlayerSpawned){
            transform.position += (playerTransform.position - transform.position).normalized * Time.deltaTime * 5f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.CompareTag("Player")){
            //Add to player morsel score
            ScoreManager.instance.AddMorselScore();
            gameObject.SetActive(false);
        }
        if(collision.collider.CompareTag("Lava")){
            //Play sizzling sound
            gameObject.SetActive(false);
        }
    }
}
