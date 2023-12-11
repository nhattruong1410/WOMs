using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform bar;
    private void Start()
    {
        Transform bar = transform.Find("Bar");
    }

    public void SetHealth(float health)
    {
        if(bar)
            bar.localScale = new Vector3(health, 1f);
    }

    public void SetColor(Color color)
    {
        if(bar)
            bar.Find("BarSprite").GetComponent<SpriteRenderer>().color = color;
    }
}
