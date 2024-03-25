using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] public float attackCoolDown;
    [SerializeField] private Transform flamePoint;
    [SerializeField] private GameObject[] flameBalls;

    [SerializeField] GameObject attackNearBoxCollider2D;
    private float coolDownTimer = Mathf.Infinity;
    private Animator MyAnimator;
    private PlayerMovement playerMovement;

    private float attkNearCoolDown;
    private void Start()
    {
        attackNearBoxCollider2D.GetComponent<BoxCollider2D>().enabled = false;
    }
    private void Awake()
    {
        MyAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    public void Update()
    {
        if ((Input.GetMouseButton(0)||Input.GetButton("Fire1") || Input.GetKeyDown(KeyCode.L)) && coolDownTimer > attackCoolDown && playerMovement.canAttack())
        {
            Attack();
            print("Key flameBall is being press");
            
        }

        if (Input.GetKeyDown(KeyCode.K) && coolDownTimer > attackCoolDown && playerMovement.canAttackNear())
        {
            AttackNear();
            print("Key attacknear is being press");
        }
        attkNearCoolDown += Time.deltaTime;
        coolDownTimer += Time.deltaTime;
        //coolDownTimer = coolDownTimer + Time.deltaTime;

        //print(attkNearCoolDown);
        /*Lấy AttackNearCoolDown làm timer. Khi phương thức AttackNear() được gọi thì attackNearCoolDown = 0
         * Cho đến khi nào attakNearCoolDown lớn hơn 1 thì mới thực hiện việc tắt. Nhược điểm của cách làm này so với
         * IEnumerator là bặt buộc  phải cho điêu kiện vào trong FixUpdate Gây đến việc giảm hiệu quả. Còn nếu
         * Sử dụng IEnumerator trong ngay phương thức AttackNear sẽ hiệu quả hơn.
         */
        if (attkNearCoolDown > 0.5f) 
            attackNearBoxCollider2D.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void AttackNear()
    {
        MyAnimator.SetTrigger("attackNear");
        attkNearCoolDown = 0;
        attackNearBoxCollider2D.GetComponent<BoxCollider2D>().enabled = true;
        
    }

    public void Attack()
    {
        MyAnimator.SetTrigger("attackFar");
        coolDownTimer = 0;
        flameBalls[findBall()].transform.position = flamePoint.position;
        flameBalls[findBall()].GetComponent<Projectile>().setDirection(Mathf.Sign(transform.localScale.x));
    }

    private int findBall()
    {
        for (int i = 0; i< flameBalls.Length; i++)
        {
            if (!flameBalls[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}



