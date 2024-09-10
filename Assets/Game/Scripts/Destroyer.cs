using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{

    private Touch touch;
    [SerializeField] private new Camera camera;

    [SerializeField] private LayerMask BoxLayer;

    public int BoxNumber;

    public new Renderer renderer;
    public Material[] mats;

    [SerializeField] private Material BoxMetarial;
    [SerializeField] private Material NumberMetarial;
    [SerializeField] private Material ShineMetarial;

    [SerializeField] private Material ErrorMetarial;

    private GameObject hitObject;

    [SerializeField] private GameObject Haptic;

    [SerializeField] private AudioClip[] Boxes;
    [SerializeField] private AudioClip Error;

    [SerializeField] private GameObject BoxesAudioSource;

    private bool destroyed;

    [SerializeField] private ParticleSystem Crush;

    public bool mouseOn;





    private void Start()
    {

        renderer = GetComponent<Renderer>();
        mats = renderer.materials;
        mats[0] = BoxMetarial;
        mats[1] = NumberMetarial;
        renderer.materials = mats;
        destroyed = false;

    }



    private void Update()
    {

        if (FindObjectOfType<DiceManager>().diceStop != true)
        {

            mats = renderer.materials;
            mats[0] = BoxMetarial;
            mats[1] = NumberMetarial;
            renderer.materials = mats;


        }


        //mouse

        if (Input.GetMouseButtonUp(0) && mouseOn)
        {

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, BoxLayer))
            {

                if (hitInfo.collider.gameObject.GetComponent<Destroyer>() != null)
                {

                    if (FindObjectOfType<GameManagerUSTB>().RoundTotal - hitInfo.transform.gameObject.GetComponent<Destroyer>().BoxNumber >= 0 && !destroyed)
                    {
                        destroyed = true;
                        FindObjectOfType<GameManagerUSTB>().RoundTotal = FindObjectOfType<GameManagerUSTB>().RoundTotal -
                        hitInfo.transform.gameObject.GetComponent<Destroyer>().BoxNumber;
                        NumberSound();
                        Instantiate(Crush,hitInfo.transform.position,hitInfo.transform.rotation);
                        DestroyIt.Destructible destObj = hitInfo.transform.gameObject.GetComponent<DestroyIt.Destructible>();
                        destObj.Destroy();


                        if (PlayerPrefs.HasKey("Vibration"))
                        {
                            if (PlayerPrefs.GetInt("Vibration") == 1)
                            {
                                Haptic.GetComponent<iOSHapticController>().TriggerImpactLight();
                            }
                        }

                    }

                    else

                    {
                        hitInfo.transform.gameObject.GetComponent<Destroyer>().mats[0] = BoxMetarial;
                        hitInfo.transform.gameObject.GetComponent<Destroyer>().mats[1] = ErrorMetarial;
                        hitInfo.transform.gameObject.GetComponent<Destroyer>().renderer.materials = hitInfo.transform.gameObject.GetComponent<Destroyer>().mats;
                        if (PlayerPrefs.HasKey("Sound"))
                        {
                            if (PlayerPrefs.GetInt("Sound") == 1)
                            {
                                GetComponent<AudioSource>().PlayOneShot(Error, 0.05f);
                            }
                        }
                    }

                }

            }
        }

        //touch

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = camera.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out RaycastHit hitInfo, BoxLayer))
                {
                    if (hitInfo.collider.gameObject.GetComponent<Destroyer>() != null)
                    {

                        hitObject = hitInfo.transform.gameObject;

                        hitObject.GetComponent<Destroyer>().mats[0] = BoxMetarial;
                        hitObject.GetComponent<Destroyer>().mats[1] = ShineMetarial;
                        hitObject.GetComponent<Destroyer>().renderer.materials = hitObject.GetComponent<Destroyer>().mats;
                    }
                }
            }

            if (touch.phase == TouchPhase.Moved)
            {
                if(hitObject != null)
                {

                
                Ray ray = camera.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out RaycastHit hitInfo, BoxLayer))
                {
                    if (hitInfo.collider.gameObject.GetComponent<Destroyer>() != null)
                    {
                        hitObject = hitInfo.transform.gameObject;
                    }
                    else
                    {
                        hitObject.GetComponent<Destroyer>().mats[0] = BoxMetarial;
                        hitObject.GetComponent<Destroyer>().mats[1] = NumberMetarial;
                        hitObject.GetComponent<Destroyer>().renderer.materials = hitObject.GetComponent<Destroyer>().mats;
                        hitObject = null;
                    }
                }

                }
            }

            if (touch.phase == TouchPhase.Ended)
            {

                if (hitObject != null)
                {

                    Ray ray = camera.ScreenPointToRay(touch.position);

                    if (Physics.Raycast(ray, out RaycastHit hitInfo, BoxLayer))
                    {
                        if (hitInfo.collider.gameObject.GetComponent<Destroyer>() != null)
                        {
                            if (hitObject == this.gameObject)
                            {

                                if (FindObjectOfType<GameManagerUSTB>().RoundTotal - hitObject.GetComponent<Destroyer>().BoxNumber >= 0 && !destroyed)
                                {
                                    if (PlayerPrefs.HasKey("Vibration"))
                                    {
                                        if (PlayerPrefs.GetInt("Vibration") == 1)
                                        {
                                            Haptic.GetComponent<iOSHapticController>().TriggerImpactMedium();
                                        }
                                    }

                                    destroyed = true;
                                    FindObjectOfType<GameManagerUSTB>().RoundTotal = FindObjectOfType<GameManagerUSTB>().RoundTotal -
                                    hitObject.GetComponent<Destroyer>().BoxNumber;
                                    NumberSound();
                                    DestroyIt.Destructible destObj = hitObject.GetComponent<DestroyIt.Destructible>();
                                    Instantiate(Crush,hitInfo.transform.position,hitInfo.transform.rotation);
                                    destObj.Destroy();
                                    hitObject = null;

                                }

                                else

                                {

                                    
                                    hitObject.GetComponent<Destroyer>().mats[0] = BoxMetarial;
                                    hitObject.GetComponent<Destroyer>().mats[1] = ErrorMetarial;
                                    hitObject.GetComponent<Destroyer>().renderer.materials = hitObject.GetComponent<Destroyer>().mats;


                                    if (PlayerPrefs.HasKey("Vibration"))
                                    {
                                        if (PlayerPrefs.GetInt("Vibration") == 1)
                                        {
                                            Haptic.GetComponent<iOSHapticController>().TriggerImpactMedium();
                                        }
                                    }
                                    if (PlayerPrefs.HasKey("Sound"))
                                    {
                                        if (PlayerPrefs.GetInt("Sound") == 1)
                                        {
                                            
                                            GetComponent<AudioSource>().PlayOneShot(Error, 0.9f);
                                        }
                                    }




                                    


                                }

                            }

                        }

                    }

                }

            }





        }






    }

    public void NumberSound()
    {

        if (PlayerPrefs.HasKey("Sound"))
        {

            if (PlayerPrefs.GetInt("Sound") == 1)
            {
                if (PlayerPrefs.HasKey("SoundCounter"))
                {

                    int soundCounter = PlayerPrefs.GetInt("SoundCounter");

                    BoxesAudioSource.GetComponent<AudioSource>().PlayOneShot(Boxes[soundCounter - 1], 0.6f);

                    PlayerPrefs.SetInt("SoundCounter", soundCounter + 1);
                }
            }
        }
    }





}














