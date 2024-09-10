using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushDestroyer : MonoBehaviour
{

    private float timer;
    
    void Update()
    {

        timer += Time.deltaTime;

        
        if(timer > 4.5f)
        {
            Destroy(this.gameObject);
        }
    }
}
