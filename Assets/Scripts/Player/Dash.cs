using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] private float DashCoolDown;
    [SerializeField] private float DashPower;
    private float DashCoolDownTimer = Mathf.Infinity;
    private Rigidbody2D MyRBody;
    private SpriteRenderer myRenderer;
    // Start is called before the first frame update
    void Start()
    {
        MyRBody = GetComponent<Rigidbody2D>();
        myRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DashCoolDownTimer > DashCoolDown)
        {
            if (0 == 0)
            {
                if (Input.GetKey(KeyCode.J))
                {
                    print("Trigger done");
                    DashMethod();
                }

            }
        }
        else { DashCoolDownTimer += Time.deltaTime; }
    }

    private void DashMethod()
    {
        DashCoolDownTimer = 0; // Set thời gian về lại 0 để so sánh thời gian có thể dash tiếp
        Vector2 Force = new Vector2(transform.localScale.x * DashPower, MyRBody.velocity.y);
        GetComponent<PlayerMovement>().enabled = false;
        StartCoroutine(Dash_Invunerablity(Force));
    }

    private IEnumerator Dash_Invunerablity(Vector2 Force)
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        myRenderer.color = new Color(0,1,0,0.8f);
        MyRBody.velocity = Force;
        yield return new WaitForSeconds(0.2f);
        GetComponent<PlayerMovement>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        myRenderer.color = Color.white;
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }
}
