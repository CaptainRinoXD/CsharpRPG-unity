using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    private bool isCall;
    private TMPro.TextMeshProUGUI textMesh;
    private Animator myAnim;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TMPro.TextMeshProUGUI>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCall == false) { return; }
    }

    public void DisplayFL_text(float dmgGiven, bool isCrital)
    {
        gameObject.SetActive(true);
        isCall = true; // display the floating text
        if (textMesh.text != null)
        {
            textMesh.text = dmgGiven.ToString();
        }
        if (isCrital == true) 
        {
            myAnim.SetTrigger("IsCritical");
        } else if(isCrital == false)
        {
            myAnim.SetTrigger("IsCall");
        }
    }

    private void DeactiveText()
    {
        gameObject.SetActive(false);
        isCall = false; //return the floating text
    }
}
