using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMV : MonoBehaviour
{
    public float moveSpeed;
    [SerializeField] DirectionZone directionZone;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask InvisibleWall_Layer;
    private Rigidbody2D MyRBody;
    private Animator myAnime;
    private BoxCollider2D MyBoxCollider2D;
    private Vector3 targetPosition; // Store the target position to move towards

    // Start is called before the first frame update
    void Start()
    {
        MyRBody = GetComponent<Rigidbody2D>();
        myAnime = GetComponent<Animator>();
        MyBoxCollider2D = GetComponent<BoxCollider2D>();

        // Set initial target position
        if (directionZone.GetdectectObjs().Count > 0)
        {
            targetPosition = directionZone.GetdectectObjs()[0].transform.position;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (directionZone.GetdectectObjs().Count > 0)
        {
            targetPosition = directionZone.GetdectectObjs()[0].transform.position;
        }

        Vector2 direction = (targetPosition - transform.position).normalized;
        float Speed = moveSpeed;

        if (isGrounded())
        {
            moveSpeed = Speed;
            MyRBody.velocity = new Vector2(direction.x * moveSpeed, MyRBody.velocity.y);
        }
        else if (isOnInvisibleWall()  )
        {
            moveSpeed = -moveSpeed;
            MyRBody.velocity = new Vector2(moveSpeed, MyRBody.velocity.y);
        }

        if (Mathf.Sign(MyRBody.velocity.x) >= 0.01f)
            transform.localScale = Vector3.one;
        if (Mathf.Sign(MyRBody.velocity.x) <= 0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        myAnime.SetBool("run", direction.x != 0);
    }

    private bool isGrounded()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(MyBoxCollider2D.bounds.center, MyBoxCollider2D.bounds.size, 0, Vector2.down, 0.5f, groundLayer);
        return rayCastHit.collider != null;
    }


    private bool isOnInvisibleWall()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(MyBoxCollider2D.bounds.center, MyBoxCollider2D.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.035f, InvisibleWall_Layer);
        // Need more information Vector2(transform.localScale.x,0)
        return rayCastHit.collider != null; // if rayCasetHit is not collieded with ground layer then return false, vice versa

    }
}
