using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{

    [SerializeField] private float scrollSpeed;
    [SerializeField] private bool x,y;

    Vector2 startPos;

    [SerializeField] private RawImage rawImage;
    [SerializeField]private float xSpeed, ySpeed;
    [SerializeField] private bool uiScrolling;

    


    private void Start()
    {
        if (!uiScrolling)
        {
            startPos = transform.position;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!uiScrolling)
        {
            if (x)
            {
                float newPosition = Mathf.Repeat(Time.time*scrollSpeed,20f);
                transform.position = startPos + Vector2.right * newPosition;
            }
            if (y)
            {
                float newPosition = Mathf.Repeat(Time.time * scrollSpeed, 20f);
                transform.position = startPos + Vector2.up * newPosition;
            }
            if (x&&y)
            {
                float newPosition = Mathf.Repeat(Time.time * scrollSpeed, 50f);
                transform.position = startPos + new Vector2(newPosition,newPosition);
            }
        }
        else
        {
            rawImage.uvRect = new Rect(rawImage.uvRect.position + new Vector2(xSpeed, ySpeed) * Time.deltaTime, rawImage.uvRect.size);
        }


    }
}
