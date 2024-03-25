using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

    private Health health;

    // Start is called before the first frame update
    void Start()
    {
        MyRBody = GetComponent<Rigidbody2D>();
        myAnime = GetComponent<Animator>();
        MyBoxCollider2D = GetComponent<BoxCollider2D>();
        health = GetComponent<Health>();

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
        Vector2 Force = new Vector2(direction.x * moveSpeed, MyRBody.velocity.y);

        // Apply movement force
        MyRBody.velocity = Force;

        // Flip sprite based on movement direction
        if (Mathf.Sign(MyRBody.velocity.x) >= 0.01f)
            transform.localScale = Vector3.one;
        if (Mathf.Sign(MyRBody.velocity.x) <= 0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // Set animator parameter
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
        return rayCastHit.collider != null;
    }

    public void isBeingHit(bool hit, float _direction, float knockBack)
    {
        // Calculate knockback velocity
        Vector2 knockbackVelocity = new Vector2(_direction * knockBack, MyRBody.velocity.y);
        GetComponent<SlimeMV>().enabled = false;
        StartCoroutine(DelayForKnockBack(knockbackVelocity));
        
    }
    private IEnumerator DelayForKnockBack(Vector2 Force) 
    {
        MyRBody.velocity = Force;
        yield return new WaitForSeconds(1);
        print("Coming back for player");
        if ( health.currentHealth > 0 )
            GetComponent<SlimeMV>().enabled = true;
    }
    /* Vì Fixupdate liên tục tìm vị trí của người chơi và di chuyển slime đến vị trí đến nên không thể dùng lệnh velocity khác để 
     * di chuyển slime được vì liên tục bị ghi đè. Như vậy bằng cách tạm thời tắt Fixupdate (tắt scpirt di chuyển) và chỉ cho phép 
     * method isbeing hit chạy (về cơ bản thì script khi bị tắt thì chỉ mỗi mình fixupdate và update bị tắt còn các method được tạo ra 
     * vẫn gọi được. Rồi sử dụng như trên để tạo delay bặt lại di chuyển với điều kiện slime có máu lớn hơn 0.*/
}
