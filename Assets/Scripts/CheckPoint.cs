using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Respawn respawn;

    void Awake()
    {
        respawn = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Respawn>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            respawn.bonfire = this.gameObject;
        }

    }
}
