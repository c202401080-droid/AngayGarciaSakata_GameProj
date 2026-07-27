using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submarine : MonoBehaviour
{
    public float Health, MaxHealth;

    [SerializeField]
    private HPUI healthBar;

    void Start()
    {
        healthBar.SetMaxHealth(MaxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            SetHealth(-20f);
        }

        if (Input.GetKeyDown("2"))
        {
            SetHealth(20f);
        }
    }

    public void SetHealth(float healthChange)
    {
        Health += healthChange;
        Health = Mathf.Clamp(Health, 0, MaxHealth);

        healthBar.SetHealth(Health);
    }
}
