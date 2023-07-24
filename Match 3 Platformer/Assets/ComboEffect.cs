using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboEffect : MonoBehaviour
{
    public float animationSpeed = 0.5f;
    public Ease easeType;
    SpriteRenderer myRenderer;
    public Sprite[] combosSprites;

    private void Awake()
    {
        myRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Vector2 randomDirection = new Vector2(transform.position.x + Random.Range(0.25f, 0.5f), transform.position.y + Random.Range(0.5f, 1.5f));
        transform.DOMove(randomDirection, animationSpeed).SetEase(easeType);
        transform.DORotate(new Vector3(0f, 0f, UnityEngine.Random.Range(-15f, 15f)), animationSpeed).SetEase(easeType);
        transform.DOScale(Vector3.zero,animationSpeed*2f).OnComplete(()=>Destroy(gameObject)).SetEase(Ease.InElastic);
    }

    public void Initialize(int combo)
    {
        switch (combo)
        {
            case 2:
                myRenderer.sprite = combosSprites[0];
                break;
            case 3:
                myRenderer.sprite = combosSprites[1];
                break;
            case 4:
                myRenderer.sprite = combosSprites[2];
                break;
            case 5:
                myRenderer.sprite = combosSprites[3];
                break;
            case 6:
                myRenderer.sprite = combosSprites[4];
                break;
            default:
                break;
        }
    }


}
