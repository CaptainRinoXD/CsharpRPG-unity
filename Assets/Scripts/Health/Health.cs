using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    public float StartingtHealth = 5;
    public float currentHealth;
    private Animator myAnim;
    public bool PlayerHasDie;
    private SpriteRenderer myRenderer;
    //public GameObject player;
    //public GameObject bonfire;
    //public bool Reload;


    [Header("iFrame")]
    [SerializeField] private float iFrameDuration;
    [SerializeField] private float numberOfFlash;

    // Start is called before the first frame update
    private void Start() // This method is called every frame
    {
        myAnim = GetComponent<Animator>();
        myRenderer = GetComponent<SpriteRenderer>();


    }

    private void Awake() // This method is only called once when the this script being call
    {
        currentHealth = StartingtHealth;
        myRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, StartingtHealth);
        
        if (currentHealth > 0 )
        {
            //Player is being hurt
            myAnim.SetTrigger("Hurt");
            StartCoroutine(Invunerablity());
        }
        else 
        {
            //Player died
            if(!PlayerHasDie) 
            {
                
                myAnim.SetTrigger("Died");
                
                PlayerHasDie = true;

                //Player 
                if(GetComponent<PlayerMovement>() != null)
                {
                    GetComponent<PlayerMovement>().enabled = false;
                    GetComponent<PlayerAttack>().enabled = false;
                }

                //Enemy
                if(GetComponent<SlimeMV>() != null)
                {
                    GetComponent<SlimeMV>().enabled = false;
                    GetComponent<DamgePlayer>().setActive(false);
                    //GetComponent<SlimeMV>().GetComponent<BoxCollider2D>().enabled = false;
                }

                //Sucubus
                if(GetComponent<Sucu_mv>() != null)
                {
                    GetComponent<Sucu_mv>().enabled = false;
                }

                StartCoroutine(DeactivateAfterDelay(2f)); // delaying setActive false
            }

            //Respawn();
            //player.transform.position = bonfire.transform.position;
        }
    }

    //public void Respawn()
    //{
    //    if (PlayerHasDie == true)
    //    {
    //        Reload = true;
    //        Scene currentScene = SceneManager.GetActiveScene();
    //        SceneManager.LoadScene(currentScene.name);           
    //    }
    //    if (Reload == true)
    //    {
            
    //    }
    //}

    private IEnumerator Invunerablity() //use IEnumerator not IEnumerable
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        //Invunerablity duration delay
        for (int i = 0; i < numberOfFlash; i++)
        {
            myRenderer.color = new Color(1,0,0,0.8f);
            yield return new WaitForSeconds(iFrameDuration/(numberOfFlash*2));
            myRenderer.color = Color.white;
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlash * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);

    }
    private IEnumerator DeactivateAfterDelay(float delay)
    {
        
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
        }
    }
}
