using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCheck : MonoBehaviour
{
    public Vector2 size;
    public Transform point;
    public LayerMask whatIsBlock;
    [HideInInspector]
    public bool hasLost = false;

    GameObject block;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
            block = Block().gameObject;

        
        //Debug.Log(block.name);
        if (hasLost == false&&block!=null)
        {
                if (block.TryGetComponent(out Rigidbody2D rb))
                {
                    if ((Mathf.Abs(rb.velocity.y) <= 0.1f) && block.GetComponent<HasBennCarried>()==null)
                    {
                            hasLost = true;
                            FindObjectOfType<PlayerDeathHandler>().Kill();
                    }
                
                }
        }
  
      
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            if (Mathf.Abs (collision.gameObject.GetComponent<Rigidbody2D>().velocity.y) <= 0.5f)
            {
            }
        }
    }


    Collider2D Block()
    {

       return Physics2D.OverlapBox(point.position, size, 0f, whatIsBlock);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(point.position, size);
    }
}