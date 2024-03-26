using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] GameObject player;
    public bool isPicked;
    private Vector2 vel;
    public float smoothTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPicked)
        {
            Vector3 offset = new Vector3(0,-0.5f,0);
            transform.position = Vector2.SmoothDamp(transform.position, player.transform.position+offset, ref vel, smoothTime );
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isPicked)
        {
            isPicked = true;
        }
    }
}
