using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBlock : MonoBehaviour
{

    public Sprite[] blocks; 
    SpriteRenderer myRenderer;
    Rigidbody2D rb;

    Sprite startingSprite;
    public SpriteRenderer extraRenderer;
    [SerializeField]
    private float animationSampleRate = 0.1f;

    bool hasStarted = false;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myRenderer = GetComponent<SpriteRenderer>();
        startingSprite = myRenderer.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (myRenderer.sprite==startingSprite)
        {
            if (GetComponent<HasBennCarried>() == null && Mathf.Abs(rb.velocity.y) <= 0.25f && transform.position.y<4.75f)
            {
                if (hasStarted) return;
                hasStarted = true;
                StartCoroutine(RandromSpriteAnimatio());
                //myRenderer.sprite = PickRandomSprite();
            }
        }
    
    }


    Sprite PickRandomSprite()
    {
        int randomIndex = Random.Range(0,blocks.Length);
        return blocks[randomIndex];
    }

    IEnumerator RandromSpriteAnimatio()
    {
        for (int i = 0; i < 20; i++)
        {
            extraRenderer.sprite = PickRandomSprite();
            FindObjectOfType<AudioManager>().Play("Random");
            yield return new WaitForSeconds(animationSampleRate);
        }
        Destroy(extraRenderer);
        myRenderer.sprite = PickRandomSprite();

    }
}
