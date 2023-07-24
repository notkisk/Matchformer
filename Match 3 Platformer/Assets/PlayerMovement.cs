using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.5f;
    [SerializeField]
    private Ease easeType;
    [SerializeField]
    private float minX=-3.5f, maxX=3.5f;



    Animator anim;
    [HideInInspector]
    public bool isMoving = false;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        anim.SetBool("isMoving", isMoving);

        if (!isMoving)
        {
            
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                Vector2 moveDirection = new Vector2(transform.position.x + 1f, transform.position.y + 0f);
                if (moveDirection.x > maxX) return;
                FindObjectOfType<AudioManager>().Play("Footstep");
                isMoving = true;
                transform.DOMove(moveDirection, moveSpeed).OnComplete(()=>isMoving=false).SetEase(easeType);
                transform.localScale = new Vector3(1f,1f,1f);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                Vector2 moveDirection = new Vector2(transform.position.x - 1f, transform.position.y + 0f);
                if (moveDirection.x < minX) return;
                FindObjectOfType<AudioManager>().Play("Footstep");
                isMoving = true;
                transform.DOMove(moveDirection, moveSpeed).OnComplete(() => isMoving = false).SetEase(easeType);
                transform.localScale = new Vector3(-1f, 1f, 1f);

            }
        }
    
    }
}
