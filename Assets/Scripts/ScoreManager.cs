using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI MorselScoreUI;
    int morselCount = 1;
    [SerializeField]
    Animator PlayerAnim;

    bool isChonk = false;
    bool isBones = false;

    public bool isGameOver = false;
    void Awake(){
        instance  = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MorselScoreUI.text = morselCount.ToString();
    }

    public void AddMorselScore(){
        if(morselCount == 0 && isBones){
            VFXManager.instance.Poof(PlayerMovement.instance.transform.position);
            PlayerAnim.SetTrigger("Normal");
        }
        morselCount++;
        if(morselCount >=8 && !isChonk){
            PlayerAnim.SetTrigger("Chonk");
            VFXManager.instance.Poof(PlayerMovement.instance.transform.position);
            AudioManager.instance.PlaySoundAtLocation(
                AudioManager.instance.MiscSounds[9],
                PlayerMovement.instance.transform.position
            );
            isChonk = true;
        } 
        isBones = false;
    }
    public void LoseAllMorsels(){
        morselCount = 0;
        PlayerAnim.SetTrigger("Hurt");
        AudioManager.instance.PlaySoundAtLocation(
            AudioManager.instance.MiscSounds[12],
            PlayerMovement.instance.transform.position
        );
        isChonk = false;
        isBones = true;
    }
    public int MorselCount{
        get {return morselCount;}
    }
}
