using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] Image healthImage;
    [SerializeField] float maxHealth = 100.0f;
    float currentHealth;

    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
        Initialize();
    }

    void Initialize()
    {
        healthImage.fillAmount = currentHealth / maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.C))
            TakeDamage();
    }

    void TakeDamage()
    {
        currentHealth -= 1;
        if(currentHealth > 0f)
            healthImage.transform.localScale = new Vector3(currentHealth / maxHealth, 1f, 1f);
    }
}
