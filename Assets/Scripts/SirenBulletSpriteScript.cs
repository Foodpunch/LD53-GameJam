using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirenBulletSpriteScript : MonoBehaviour
{
    public List<Sprite> bulletSprites = new List<Sprite>();
    SpriteRenderer _sr;
    // Start is called before the first frame update
    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _sr.sprite = bulletSprites[Random.Range(0,bulletSprites.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
