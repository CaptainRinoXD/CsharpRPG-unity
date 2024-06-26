﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Mathematics;
using UnityEngine;

public class Meele : MonoBehaviour
{
    [SerializeField] float knockBackAmout;
    
    [Header("Player")]
    [SerializeField] GameObject PlayerObject;
    [SerializeField] float damgeGivenToEnemy;
    [SerializeField] int criticalPercentage;
    private float DmgIncrase;

    [Header("Enemy")]
    [SerializeField] GameObject EnemyObject;
    [SerializeField] float damgeGivenToPlayer;
    [SerializeField] public float attackCoolDown;

    private BoxCollider2D MyBoxCollider2D;
    private float direction;

    private void Awake()
    {
        MyBoxCollider2D = GetComponent<BoxCollider2D>();

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Health>().takeCriticalHit(isCriticalHit()); // set the floating text
            if (isCriticalHit() == true) 
            {
                collision.GetComponent<Health>().TakeDamage(DmgIncrase);
            } //set critial hit
            else
            {
                collision.GetComponent<Health>().TakeDamage(damgeGivenToEnemy); //Damge enemy health
            }

            direction = PlayerObject.transform.localScale.x; //Get the direction where the player hit to gave knockback
            

            //Slime
            if (collision.GetComponent<SlimeMV>() != null)
            {
                collision.GetComponent<SlimeMV>().enabled = false;
                collision.GetComponent<SlimeMV>().isBeingHit(true, direction, knockBackAmout);
            }

            //Sucubus
            if (collision.GetComponent<Sucu_mv>() != null)
            {
                collision.GetComponent<Sucu_mv>().enabled = false;
                collision.GetComponent<Sucu_mv>().isBeingHit(true, direction, knockBackAmout);
            }
            
            // lý do vì sao lấy được GetCompoent<Health> vì được lấy từ đối tượng va vào colider của gameObject này.
            MyBoxCollider2D.enabled = false;
        }
        if (collision.tag == "Player")
        {
            StartCoroutine(meeleAttackCoolDown(collision));
        }
        else if (collision.tag == "DetectionZone" || collision.tag == "InvisibleWall") //ingore out the collsion for dectionZone
        {
            //print("Collsion dectect"); //https://forum.unity.com/threads/ignore-collisions-by-tag-solved.60387/
            if (collision.GetComponent<CircleCollider2D>() != null)
                Physics2D.IgnoreCollision(collision.GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>(), true);
        }
    }

    private IEnumerator meeleAttackCoolDown(Collider2D collision)
    {
        direction = EnemyObject.transform.localScale.x;
        EnemyObject.GetComponent<Sucu_Attack>().AttackNear(collision, damgeGivenToPlayer, direction, knockBackAmout);
        yield return new WaitForSeconds(0.2f);
        MyBoxCollider2D.enabled = false;
        yield return new WaitForSeconds(attackCoolDown);
        MyBoxCollider2D.enabled = true;
    }

    private bool isCriticalHit()
    {
        System.Random rnd = new System.Random();
        int Number = rnd.Next(0,100);
        if (Number < criticalPercentage)
        {
            DmgIncrase = damgeGivenToEnemy + rnd.Next(1,3);
            return true;
        } else { return false; }
    }
}
