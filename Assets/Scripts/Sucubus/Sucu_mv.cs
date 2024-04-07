using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sucu_mv : MonoBehaviour
{
    public float moveSpeed;
    [SerializeField] DirectionZone directionZone;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask InvisibleWall_Layer;
    private Rigidbody2D MyRBody;
    private Animator myAnime;
    private BoxCollider2D MyBoxCollider2D;
    private Vector3 targetPosition; // Store the target position to move towards
    private bool canShoot;
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
    private void Update()
    {
        canShoot = false;
        if (directionZone.GetdectectObjs().Count > 0)
        {
            targetPosition = directionZone.GetdectectObjs()[0].transform.position;
        }
        float Distance = Mathf.Abs(targetPosition.x - transform.position.x);
        Vector2 direction = (targetPosition - transform.position).normalized;

        if (Distance < 1.5)
        {
            Vector2 Force = new Vector2(direction.x * moveSpeed, MyRBody.velocity.y);
            // Apply movement force
            MyRBody.velocity = Force;
            if (Distance < 0.3f)
            {
                MyRBody.velocity = Vector2.zero;
            }
        }

        if(Distance >= 1.5)
        {
            Vector2 Force = new Vector2(direction.x * (moveSpeed/10), MyRBody.velocity.y);
            // Apply movement force
            MyRBody.velocity = Force;
            canShoot = true;
        }
        //print(canShoot); kiểm tra xem có kích hoạt tấn công từ xa

        print(direction);
        if (direction.x != 0)
        {
            // Flip sprite based on movement direction
            if (Mathf.Sign(direction.x) >= 0.01f)
                transform.localScale = Vector3.one;
            if (Mathf.Sign(direction.x) <= 0.01f)
                transform.localScale = new Vector3(-1, 1, 1);
        }


        // Set animator parameter
        myAnime.SetBool("run", directionZone.GetdectectObjs().Count > 0);
    }

    // KnockBack method
    public void isBeingHit(bool hit, float _direction, float knockBack)
    {
        // Calculate knockback velocity
        Vector2 knockbackVelocity = new Vector2(_direction * knockBack, MyRBody.velocity.y);
        GetComponent<Sucu_mv>().enabled = false;
        StartCoroutine(DelayForKnockBack(knockbackVelocity));

    }
    private IEnumerator DelayForKnockBack(Vector2 Force)
    {
        MyRBody.velocity = Force;
        yield return new WaitForSeconds(1);
        print("Coming back for player");
        if (health.currentHealth > 0)
            GetComponent<Sucu_mv>().enabled = true;
    }
    /* Vì Fixupdate liên tục tìm vị trí của người chơi và di chuyển slime đến vị trí đến nên không thể dùng lệnh velocity khác để 
     * di chuyển slime được vì liên tục bị ghi đè. Như vậy bằng cách tạm thời tắt Fixupdate (tắt scpirt di chuyển) và chỉ cho phép 
     * method isbeing hit chạy (về cơ bản thì script khi bị tắt thì chỉ mỗi mình fixupdate và update bị tắt còn các method được tạo ra 
     * vẫn gọi được. Rồi sử dụng như trên để tạo delay bặt lại di chuyển với điều kiện slime có máu lớn hơn 0.*/


    public bool CanShoot()
    {
        return canShoot;
    }
}
