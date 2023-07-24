using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{

    public float flashDuration = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FlashCourotine());   
    }

    

    IEnumerator FlashCourotine()
    {
        yield return new WaitForSeconds(flashDuration);
        Destroy(gameObject);
    }
}
