using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCarrier : MonoBehaviour
{
    [HideInInspector]
    public bool isCarrieng;

    [SerializeField]
    private float lineCheckLength;
    [SerializeField]
    private Transform startingPoint;
    [SerializeField]
    private LayerMask whatIsBlock;

    bool isBlockAbove;

    public Transform blockHoldPosition;

    GameObject blockAbove;
    GameObject blockCarried;
    public Transform checkPoint;

    Animator anim;

    public int blocksCarried = 0;
    // Start is called before the first frame update
    void Awake()
    {
        anim=GetComponent<Animator>();
        isCarrieng = false;
    }

    // Update is called once per frame
    void Update()
    {
        isBlockAbove = CheckBlockAbove();
        blockAbove = isCarrieng ? null : GetBlockAbove().gameObject;

        anim.SetBool("isCarrieng", isCarrieng);

        if (Input.GetKeyDown(KeyCode.UpArrow)||Input.GetKeyDown(KeyCode.W))
        {
            if (isCarrieng == false && blockCarried == null)
            {
                if (isBlockAbove && blockAbove.TryGetComponent<HasBennCarried>(out HasBennCarried hbc))
                {
                    if (hbc != null)
                    {
                        blocksCarried++;
                        FindObjectOfType<AudioManager>().Play("PickUp");
                        blockAbove.transform.position = blockHoldPosition.position;
                        blockAbove.transform.parent = blockHoldPosition;
                        blockCarried = blockAbove;
                        isCarrieng = true;
                        blockCarried.GetComponent<BoxCollider2D>().isTrigger = true;
                    }

                }
            }
        }
    
       
            if (Input.GetKeyDown(KeyCode.DownArrow)||Input.GetKeyDown(KeyCode.X)||Input.GetKeyDown(KeyCode.S))
            {
                if (isCarrieng && !GetComponent<PlayerMovement>().isMoving)
                {
                        FindObjectOfType<AudioManager>().Play("Throw");

                        anim.SetTrigger("Throw");
                        isCarrieng = false;
                        Destroy(blockCarried.GetComponent<HasBennCarried>());   
                        blockCarried.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb);
                        blockHoldPosition.GetChild(0).transform.parent = null;    

                        rb.gravityScale = 1.5f;
                        blockCarried.GetComponent<BoxCollider2D>().isTrigger = false;

                        blockCarried = null;
                }


        }
        
        
    }

    bool CheckBlockAbove()
    {
        Vector2 endPoint = new Vector2(startingPoint.position.x, startingPoint.position.y + lineCheckLength);
        return Physics2D.Linecast(startingPoint.position, endPoint, whatIsBlock);

    }

    Collider2D GetBlockAbove()
    {
        return Physics2D.OverlapPoint(checkPoint.position, whatIsBlock);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(startingPoint.position, new Vector2(startingPoint.position.x,startingPoint.position.y+lineCheckLength)) ;
    }
}
