using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;




public class DiceManager : MonoBehaviour
{
    [SerializeField] private GameObject Dice1;
    [SerializeField] private GameObject Dice2;

    [SerializeField] private GameObject Cage;



    public bool diceStop;
    private int dice1num;
    private int dice2num;
    public int total;

    private float timer;

    private void Update() 
    {
        
        dice1num = Dice1.GetComponent<DiceShooter>().number;
        dice2num = Dice2.GetComponent<DiceShooter>().number;
        
        

        if(Dice1.GetComponent<DiceShooter>().rb.velocity == new Vector3(0,0,0) && Dice2.GetComponent<DiceShooter>().rb.velocity == new Vector3(0,0,0))
        {

            timer = timer + Time.deltaTime;
            
        }
        else
        {
            diceStop = false;
            timer = 0;
            
        }

        if(timer>0.3f)
        {
            diceStop = true;
            total = dice1num + dice2num;
        }

        if(diceStop == true && FindObjectOfType<GameManagerUSTB>().checkGameEnd == false && FindObjectOfType<GameManagerUSTB>().boxCount != 0 && timer > 0.5f)
        {
            
            Cage.SetActive(false);

        }
        else
        {
            Cage.SetActive(true);
            
        }


    }

    


   



    

}
