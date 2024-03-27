using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sucu_Attack : MonoBehaviour
{
    [SerializeField] public float attackFarCoolDown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireBalls;

    [SerializeField] GameObject attackNearBoxCollider2D;
    private Animator MyAnimator;
    private PlayerMovement playerMovement;

    private float attkNearCoolDown;
    private void Start()
    {
        //attackNearBoxCollider2D.GetComponent<BoxCollider2D>().enabled = false; //set GameObject for MeeleColider to off
    }
    private void Awake()
    {
        MyAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    public void Update()
    {
        attkNearCoolDown += Time.deltaTime;
        //coolDownTimer += Time.deltaTime;
        //coolDownTimer = coolDownTimer + Time.deltaTime;

        //print(attkNearCoolDown);
        /*Lấy AttackNearCoolDown làm timer. Khi phương thức AttackNear() được gọi thì attackNearCoolDown = 0
         * Cho đến khi nào attakNearCoolDown lớn hơn 1 thì mới thực hiện việc tắt. Nhược điểm của cách làm này so với
         * IEnumerator là bặt buộc  phải cho điêu kiện vào trong FixUpdate Gây đến việc giảm hiệu quả. Còn nếu
         * Sử dụng IEnumerator trong ngay phương thức AttackNear sẽ hiệu quả hơn.
         */
        //if (attkNearCoolDown > 0.2f)
            //attackNearBoxCollider2D.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void AttackNear(Collider2D collision,float damgeGivenToPlayer, float _direction, float knockBackAmout)
    {

        MyAnimator.SetTrigger("attackNear");
        collision.GetComponent<Health>().TakeDamage(damgeGivenToPlayer);
        collision.GetComponent<PlayerMovement>().isBeingHit(true, _direction, knockBackAmout * 1.5f);
        attkNearCoolDown = 0;
    }

    public void Attack()
    {
        MyAnimator.SetTrigger("attackFar");
        fireBalls[findBall()].transform.position = firePoint.position;
        fireBalls[findBall()].GetComponent<Projectile>().setDirection(Mathf.Sign(transform.localScale.x));
    }

    private int findBall()
    {
        for (int i = 0; i < fireBalls.Length; i++)
        {
            if (!fireBalls[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
