using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSpriteScript : MonoBehaviour
{
    SpriteRenderer _sr;
    float angle = 0;
    // Start is called before the first frame update
    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        angle = transform.rotation.eulerAngles.z;
        _sr.flipY = (angle>90f && angle < 270f);
    }
}
