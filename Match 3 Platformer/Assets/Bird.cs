using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Ease easeType;
    [HideInInspector]
    public Transform goPostion;

 
    bool workDone =false;

    public float angryTime=3f;
    public float timeBeforeDropingBlock=3f;
    float timeBeforeAngry;

    bool isAngry = false;
    Animator anim;

    public void Init(Transform goPos)
    {
        goPostion = goPos;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (goPostion == null) return;
        transform.DOMove(goPostion.position, moveSpeed).SetEase(easeType);

        timeBeforeAngry = angryTime;
        anim = GetComponent<Animator>();
        isAngry = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeBeforeAngry-=Time.deltaTime;    
        if (transform.childCount==0&&workDone==false)
        {
            workDone = true;
            FlyAway();
        }

        if (timeBeforeAngry<=0f&&isAngry==false)
        {
            isAngry = true;
            anim.SetBool("isAngry", true);
        }

        if (isAngry)
        {
            Destroy(transform.GetChild(0).GetComponent<HasBennCarried>());
            transform.GetChild(0).GetComponent<Rigidbody2D>().gravityScale = 1.5f;
            transform.GetChild(0).GetComponent<BoxCollider2D>().isTrigger = false;
            transform.DetachChildren();



        }
    }

    public void FlyAway()
    {
        anim.SetBool("isAngry",false);
        Vector2 randomPlace = new Vector2(transform.position.x + Random.Range(-2f,2f),transform.position.y+Random.Range(4f,6f));
        transform.DOMove(randomPlace,moveSpeed).SetEase(easeType).OnComplete(()=>Destroy(gameObject));
        //FindObjectOfType<AudioManager>().Play("FlyAway");

    }
}
