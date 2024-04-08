using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Health : MonoBehaviour
{
    [Header ("Health")]
    public float StartingtHealth;
    public float currentHealth;
    private Animator myAnim;
    private bool PlayerHasDie;
    private SpriteRenderer myRenderer;

    [Header("iFrame")]
    [SerializeField] private float iFrameDuration;
    [SerializeField] private float numberOfFlash;

    [Header("Floating damage")]
    [SerializeField] private FloatingText Fl_text;
    private bool isCritialHit = false;


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
        if(Fl_text != null) //set Floating dmg text
        {
            Fl_text.DisplayFL_text(_damage, isCritialHit);
        }

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

                //SceneManager.LoadScene
            }
        }
    }

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

    public void takeCriticalHit(bool value)
    {
        isCritialHit = value;   
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
