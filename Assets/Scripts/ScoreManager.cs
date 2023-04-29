using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI MorselScoreUI;
    int morselCount = 0;


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
        morselCount++;
    }
    public void LoseAllMorsels(){
        morselCount = 0;
    }
    public int MorselCount{
        get {return morselCount;}
    }
}
