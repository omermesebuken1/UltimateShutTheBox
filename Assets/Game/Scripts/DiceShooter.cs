using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceShooter : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField] private float forceAmount;


    private float DiceCorrectionTimer;
    [SerializeField] private LayerMask DiceCheckLayer;
    private RaycastHit hit;

    public int number;

    private int minus1;
    private int minus2;
    private int minus3;

    public bool shootbool;

    [SerializeField] private GameObject Haptic;

    [SerializeField] private AudioClip diceClip1;
    [SerializeField] private AudioClip diceClip2;
    [SerializeField] private AudioClip diceClip3;
    [SerializeField] private AudioClip diceClip4;
    [SerializeField] private AudioClip diceClip5;

    public bool started;





    private void Start()
    {
        started = false;

        rb = GetComponent<Rigidbody>();

        float rand1 = Random.Range(-180, 180);
        float rand2 = Random.Range(-180, 180);
        float rand3 = Random.Range(-180, 180);

        //transform.rotation = Quaternion.Euler(rand1, rand2, rand3);
        number = 0;

        

    }

    private void OnCollisionEnter(Collision other)
    {
        if(PlayerPrefs.HasKey("Vibration"))
        {
            if (PlayerPrefs.GetInt("Vibration") == 1)
        {
            Haptic.GetComponent<iOSHapticController>().TriggerImpactLight();
        }
        }
        


        if(PlayerPrefs.HasKey("Sound"))
        {

            if (PlayerPrefs.GetInt("Sound") == 1)
        {

            int i = Random.Range(1, 6);

            switch (i)
            {
                case 1:
                    GetComponent<AudioSource>().PlayOneShot(diceClip1,0.2f);
                    break;
                case 2:
                    GetComponent<AudioSource>().PlayOneShot(diceClip2,0.2f);
                    break;
                case 3:
                    GetComponent<AudioSource>().PlayOneShot(diceClip3,0.2f);
                    break;
                case 4:
                    GetComponent<AudioSource>().PlayOneShot(diceClip4,0.2f);
                    break;
                case 5:
                    GetComponent<AudioSource>().PlayOneShot(diceClip5,0.2f);
                    break;
            }
        }

        }
        



        if (other.gameObject.CompareTag("Dice"))
        {

            rb.AddForce(new Vector3(
            Random.Range(-2, 2),
            Random.Range(-2, 2),
            Random.Range(-2, 2))
            * forceAmount / 15 * Time.deltaTime);

        }

        if (other.gameObject.CompareTag("Cage"))
        {
            rb.AddForce(new Vector3(
            Random.Range(-2, 2),
            Random.Range(-2, 2),
            Random.Range(-2, 2))
            * forceAmount / 10 * Time.deltaTime);

        }
    }

    private void FixedUpdate()
    {

        RelocateDice();

        WrongStandCorrection();

        if(started == false)
        {


            GetComponent<Rigidbody>().isKinematic = true;
            transform.Rotate(0.5f,0.5f,0.5f);

        }


        if (shootbool)
        {

            started = true;
            GetComponent<Rigidbody>().isKinematic = false;

            minus1 = Mathf.Clamp(Random.Range(-1, 1), -1, 1);
            minus2 = Mathf.Clamp(Random.Range(-1, 1), -1, 1);
            minus3 = Mathf.Clamp(Random.Range(-1, 1), -1, 1);

            rb.AddForceAtPosition(new Vector3(
            Random.Range(5, 6) * minus1,
            Random.Range(7, 10),
            Random.Range(5, 6) * minus3)
            * forceAmount * Time.deltaTime, transform.position, ForceMode.Force);

            transform.rotation *= Quaternion.Euler(Random.Range(-180, 180),
                                                   Random.Range(-180, 180),
                                                   Random.Range(-180, 180));

            shootbool = false;


        }



        DiceNumberChecker();

    }


    private void DiceNumberChecker()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, DiceCheckLayer))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            number = 1;
        }

        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity, DiceCheckLayer))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
            number = 2;
        }

        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, Mathf.Infinity, DiceCheckLayer))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hit.distance, Color.yellow);
            number = 4;
        }
        else if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.forward), out hit, Mathf.Infinity, DiceCheckLayer))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.forward) * hit.distance, Color.yellow);
            number = 6;
        }

        else if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, Mathf.Infinity, DiceCheckLayer))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.up) * hit.distance, Color.yellow);
            number = 5;
        }

        else if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.right), out hit, Mathf.Infinity, DiceCheckLayer))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.right) * hit.distance, Color.yellow);
            number = 3;
        }
        else
        {
            number = 0;

        }




    }

    private void RelocateDice()
    {

        if (transform.localPosition.x > 16f ||
           transform.localPosition.x < -16f ||
           transform.localPosition.y > 16f ||
           transform.localPosition.y < -16f ||
           transform.localPosition.z > 16f ||
           transform.localPosition.z < -16f)
        {

            transform.localPosition = new Vector3(0, 2, 0);

            float rand1 = Random.Range(-180, 180);
            float rand2 = Random.Range(-180, 180);
            float rand3 = Random.Range(-180, 180);

            transform.rotation = Quaternion.Euler(rand1, rand2, rand3);



        }


    }

    private void WrongStandCorrection()
    {

        if (rb.velocity == new Vector3(0, 0, 0) && number == 0)
        {

            rb.AddForce(new Vector3(
            Random.Range(-2, 2),
            Random.Range(-2, 2),
            Random.Range(-2, 2))
            * forceAmount / 20 * Time.deltaTime);


        }

    }


    public void shooter()
    {
        if (PlayerPrefs.HasKey("Vibration"))
                        {
                            if (PlayerPrefs.GetInt("Vibration") == 1)
                            {
                                Haptic.GetComponent<iOSHapticController>().TriggerImpactMedium();
                            }
                        }
        if(FindObjectOfType<GameManagerUSTB>().rewarded == true)
        {
            FindObjectOfType<GameManagerUSTB>().rewardedAlready = true;    

        }
        

        shootbool = true;
    }

}
