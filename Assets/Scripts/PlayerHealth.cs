using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField]
    GameObject morselPrefab;
    
    int IFrameLayer;
    int PlayerLayer;
    public bool isInvulnerable = false;

    public GameObject GameOverScreen;
    public GameObject WinScreen;

    public GameObject randomAudioObject;

    Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        IFrameLayer = LayerMask.NameToLayer("IFrame");
        PlayerLayer = LayerMask.NameToLayer("Player");
        _rb = GetComponent<Rigidbody2D>();
        // _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    [Button]
    void ExplodeMorsels(){
        CamShaker.instance.Trauma+= 0.2f;
        StartCoroutine(InvulnerabilityFrames());
        for(int i =0; i< ScoreManager.instance.MorselCount; ++i){
            GameObject morselClone = Instantiate(morselPrefab,transform.position+(Vector3)Random.insideUnitCircle,Quaternion.identity);
            morselClone.transform.SetParent(ObjectManager.instance.transform);
            morselClone.GetComponent<Morsel>().isPlayerSpawned = true;
            morselClone.GetComponent<Rigidbody2D>().AddForce((morselClone.transform.position-transform.position).normalized * 5f,ForceMode2D.Impulse);
        }
    }
    public void OnTakeDamage(float damage){
        if(isInvulnerable) return;
        if(ScoreManager.instance.MorselCount <= 0){
            Die();
            Debug.Log("Player ded");
            if(!ScoreManager.instance.isGameOver){
                // AudioManager.instance.PlaySoundAtLocation(
                //     AudioManager.instance.MiscSounds[5],1f,transform.position
                // );
                GameOverScreen.SetActive(true);
                ScoreManager.instance.isGameOver = true;
                GetComponent<CapsuleCollider2D>().enabled = false;
            }
        } 
        else {
            ExplodeMorsels();
            ScoreManager.instance.LoseAllMorsels();
        }
    }

    IEnumerator InvulnerabilityFrames(){
        //Play IFrame anim here
        isInvulnerable = true;
        gameObject.layer = IFrameLayer;
        yield return new WaitForSecondsRealtime(2f);
        gameObject.layer = PlayerLayer;
        isInvulnerable = false;

    }


    void Die(){

    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.CompareTag("Win")) {
            GetComponent<Animator>().SetTrigger("Win");
            ScoreManager.instance.isGameOver = true;
        }
    }

    public void Win(){
        WinScreen.SetActive(true);
        randomAudioObject.SetActive(true);
    }
}
