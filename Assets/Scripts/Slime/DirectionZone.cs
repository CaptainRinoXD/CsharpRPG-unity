using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionZone : MonoBehaviour
{
    [SerializeField] List<Collider2D> dectectObjs = new List<Collider2D>();
    private CircleCollider2D circleCollider;
    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Collider2D> GetdectectObjs()
    {
        return dectectObjs;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            dectectObjs.Add(collision);
            if ( collision.tag == "buff") //ingore out the collsion for dectionZone
            {
                //print("Collsion dectect"); //https://forum.unity.com/threads/ignore-collisions-by-tag-solved.60387/
                if (collision.GetComponent<Collider2D>() != null)
                {
                    Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), GetComponent<BoxCollider2D>(), true);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GetComponentInParent<Sucu_Attack>() != null && GetComponentInParent<Sucu_mv>() != null)
            {
                if (GetComponentInParent<Sucu_mv>().CanShoot())
                    StartCoroutine(attackFarSpeed());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        dectectObjs.Remove(collision);
    }

    private IEnumerator attackFarSpeed()
    {
        circleCollider.enabled = false;
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.2f);
            GetComponentInParent<Sucu_Attack>().Attack();
        }
        yield return new WaitForSeconds(GetComponentInParent<Sucu_Attack>().attackFarCoolDown);
        circleCollider.enabled = true;
    }
}
