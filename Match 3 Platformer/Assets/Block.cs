using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public LayerMask whatIsBlock;
    public float checkThreshold;
    bool hasBeenReleased;
    SpriteRenderer myRenderer;
    [SerializeField]
    private GameObject detonateEffect;

    bool isMoving = false;

    GameObject r, l, u,d;
    Rigidbody2D rb;

    public float distanceDetonationThreshold = 1f;
    public float maxVelocity = 5f;
    bool comboStarted;


    public float comboWaitTime;
    float timeBtwCombo =0f;

    public GameObject popUp;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        timeBtwCombo = comboWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        hasBeenReleased = transform.parent == null;

        if (hasBeenReleased)
        {
            CheckBlocksNearby();
        }

        if (rb.velocity.magnitude> maxVelocity)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
        }

        if (comboStarted)
        {
                timeBtwCombo -= Time.deltaTime;
        }
        //if (isGrounded())
        //{
        //    rb.velocity = new Vector2(rb.velocity.x,0f);
        //}
    }

       


    void CheckBlocksNearby()
    {
        var RightBlock = Physics2D.OverlapPoint(new Vector2(transform.position.x + GetComponent<BoxCollider2D>().size.x + checkThreshold, transform.position.y), whatIsBlock);
        var LeftBlock = Physics2D.OverlapPoint(new Vector2(transform.position.x - (GetComponent<BoxCollider2D>().size.x + checkThreshold), transform.position.y), whatIsBlock);
        var UpBlock = Physics2D.OverlapPoint(new Vector2(transform.position.x , transform.position.y + GetComponent<BoxCollider2D>().size.y + checkThreshold), whatIsBlock);
        var DownBlock = Physics2D.OverlapPoint(new Vector2(transform.position.x , transform.position.y - (GetComponent<BoxCollider2D>().size.y + checkThreshold)), whatIsBlock);
        if (RightBlock&&LeftBlock)
        {
            if (RightBlock.TryGetComponent<SpriteRenderer>(out SpriteRenderer SR1)&&LeftBlock.TryGetComponent<SpriteRenderer>(out SpriteRenderer SR2))
            {
                if (SR1.sprite== myRenderer.sprite&&SR2.sprite==myRenderer.sprite)
                {

                    if (this.GetComponent<Rigidbody2D>().velocity.y==0f&&RightBlock.GetComponent<Rigidbody2D>().velocity.y==0f&&LeftBlock.GetComponent<Rigidbody2D>().velocity.y==0f)
                    {
                        RightBlock.GetComponent<Block>().Detonate();
                        LeftBlock.GetComponent<Block>().Detonate();
                        Detonate();

                        FindObjectOfType<GameManager>().Combo();

                    }

                }
            }
        } if (UpBlock && DownBlock)
        {
            if (UpBlock.TryGetComponent<SpriteRenderer>(out SpriteRenderer SR1) && DownBlock.TryGetComponent<SpriteRenderer>(out SpriteRenderer SR2))
            {
                if (SR1.sprite == myRenderer.sprite && SR2.sprite == myRenderer.sprite)
                {
                    //if (Mathf.Approximately(GetComponent<Rigidbody2D>().velocity.y, 0f)  && Mathf.Approximately(UpBlock.GetComponent<Rigidbody2D>().velocity.y, 0f) && Mathf.Approximately(DownBlock.GetComponent<Rigidbody2D>().velocity.y, 0f))
                    //{
                    //    UpBlock.GetComponent<Block>().Detonate();
                    //    DownBlock.GetComponent<Block>().Detonate();
                    //    Detonate();
                    //}
                        float upBlockDistace = Vector2.Distance((Vector2)this.transform.position + new Vector2(0f, GetComponent<BoxCollider2D>().size.y) , UpBlock.transform.position);
                        float downBlockDistance = Vector2.Distance(this.transform.position + new Vector3(0f,-GetComponent<BoxCollider2D>().size.y,0f), DownBlock.transform.position);
                    if (/*ApproximatelyEqual(GetComponent<Rigidbody2D>().velocity.y, 0f, velocityDetonateThreshold) && */ApproximatelyEqual( upBlockDistace, 0f, distanceDetonationThreshold) && ApproximatelyEqual(downBlockDistance, 0f, distanceDetonationThreshold))
                    {
                        UpBlock.GetComponent<Block>().Detonate();
                        DownBlock.GetComponent<Block>().Detonate();
                        Detonate();
                        FindObjectOfType<GameManager>().Combo();

                    }
                }
            }
        }




    }


    public void Detonate()
    {
        if ((Mathf.Abs(rb.velocity.y) <= 0.2f)==false && transform.position.y>4.75) return;
        Destroy(gameObject);
        Instantiate(detonateEffect, transform.position, Quaternion.identity);
        FindObjectOfType<AudioManager>().Play("Detonate");
        FindObjectOfType<GameManager>().AddScore(10);
        comboStarted = true;
        timeBtwCombo = comboWaitTime;
        Vector2 pos = new Vector2(transform.position.x + Random.Range(0.25f,0.75f),transform.position.y + Random.Range(0.25f,0.75f));
        var _popUp =  Instantiate(popUp, pos, Quaternion.identity);
        _popUp.GetComponent<PopUpText>().SetTextValue(FindObjectOfType<GameManager>().combo * 10);
        //score
    }

    bool ApproximatelyEqual(float a,float b, float threshold)
    {
        return Mathf.Abs(a - b) <= threshold;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector2(transform.position.x + GetComponent<BoxCollider2D>().size.x + checkThreshold, transform.position.y),0.25f);
        Gizmos.DrawSphere(new Vector2(transform.position.x , transform.position.y + GetComponent<BoxCollider2D>().size.y + checkThreshold), 0.05f);
        Gizmos.DrawSphere(new Vector2(transform.position.x , transform.position.y - (GetComponent<BoxCollider2D>().size.y + checkThreshold)), 0.05f);

        Gizmos.DrawLine((Vector2)this.transform.position +new Vector2(0f, this.GetComponent<BoxCollider2D>().size.y) , u.transform.position);
        Vector2 startPos = new Vector2(transform.position.x, transform.position.y - GetComponent<BoxCollider2D>().bounds.extents.y - 0.1f);
        Vector2 endPos = new Vector2(startPos.x, startPos.y - 0.1f);
        Gizmos.DrawLine(startPos,endPos);
    }


    bool ThereIsBlockBellow()
    {
       return Physics2D.OverlapPoint(new Vector2(transform.position.x, transform.position.y - GetComponent<BoxCollider2D>().size.y - checkThreshold), whatIsBlock);

    }


    bool isGrounded()
    {
        Vector2 startPos = new Vector2(transform.position.x, transform.position.y - GetComponent<BoxCollider2D>().bounds.extents.y);
        Vector2 endPos = new Vector2(startPos.x, startPos.y - 0.1f);
        return Physics2D.Linecast(startPos, endPos, whatIsBlock);
    }

   
}
