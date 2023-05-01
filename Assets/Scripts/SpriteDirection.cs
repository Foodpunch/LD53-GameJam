using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDirection : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D _rb;    
    SpriteRenderer _sr;
    float angle;
    // Start is called before the first frame update
    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();   
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Vector2.Dot(_rb.velocity.normalized,Vector2.right));
        _sr.flipX = (Vector2.Dot(_rb.velocity.normalized,transform.right) > 0);
    }
}
