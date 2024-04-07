
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider HealthBar;
    [SerializeField] private Slider EaseHealthBar;
    [SerializeField] private Health EnemyHealth;
    // Start is called before the first frame update
    void Start()
    {
        //mySlider = GetComponent<Slider>();
        HealthBar.maxValue = EnemyHealth.StartingtHealth;
        EaseHealthBar.maxValue = EnemyHealth.StartingtHealth;
    }

    // Update is called once per frame
    void Update()
    {

        HealthBar.value = EnemyHealth.currentHealth;

        if (HealthBar.value != EaseHealthBar.value)
        {
            EaseHealthBar.value = Mathf.Lerp(EaseHealthBar.value, EnemyHealth.currentHealth, 0.005f);
        }
    }
}
