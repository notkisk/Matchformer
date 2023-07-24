using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpText : MonoBehaviour
{
    public float animationSpeed;
    public Ease easeType;
    TextMeshPro txt;

    private void Awake()
    {
        txt = GetComponent<TextMeshPro>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Vector2 randomDirection = new Vector2(transform.position.x + Random.Range(0.25f, 0.5f), transform.position.y + Random.Range(0.35f, 0.75f));
        transform.DOMove(randomDirection, animationSpeed).SetEase(easeType);
        transform.DORotate(new Vector3(0f, 0f, UnityEngine.Random.Range(-15f, 15f)), animationSpeed).SetEase(easeType);
        transform.DOScale(Vector3.zero, animationSpeed/2f).OnComplete(() => Destroy(gameObject)).SetEase(easeType).SetDelay(0.5f);
    }

    public void SetTextValue(int value)
    {
        txt.text = value.ToString();
    }
}
