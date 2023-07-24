using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Vector2 offset;
    public LayerMask whatIsBird;
    public bool isOccupied;
    // Start is called before the first frame update
    void Awake()
    {
        isOccupied = false;
    }

    // Update is called once per frame
    void Update()
    {
        isOccupied = IsOccupied();
    }


    bool IsOccupied()
    {
        return Physics2D.OverlapPoint((Vector2)transform.position + offset, whatIsBird);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position+offset,0.15f);
    }
}
