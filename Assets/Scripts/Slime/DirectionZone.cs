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
        if(collision.gameObject.tag == "Player")
            dectectObjs.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        dectectObjs.Remove(collision);
    }
}
