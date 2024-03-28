using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{   
    public bool locked;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {   
        animator = GetComponent<Animator>();
        locked = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            animator.SetTrigger("Open");
            locked = false;
            SceneManager.LoadScene(1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            animator.SetTrigger("Close");
            locked = true;
        }
    }


}
