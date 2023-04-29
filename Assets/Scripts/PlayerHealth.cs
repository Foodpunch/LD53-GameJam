using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField]
    GameObject morselPrefab;

    public int MorselCount = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    [Button]
    void ExplodeMorsels(){
        for(int i =0; i< MorselCount; ++i){
            GameObject morselClone = Instantiate(morselPrefab,transform.position+(Vector3)Random.insideUnitCircle,Quaternion.identity);
            morselClone.GetComponent<Morsel>().isPlayerSpawned = true;
            morselClone.GetComponent<Rigidbody2D>().AddForce((morselClone.transform.position-transform.position).normalized * 5f,ForceMode2D.Impulse);
        }
    }
    public void OnTakeDamage(float damage){
        
    }


    void Die(){

    }
}
