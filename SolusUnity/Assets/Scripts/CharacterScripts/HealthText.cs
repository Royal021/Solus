using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public Vector3 moveSpeed = new Vector3(0,75,0);
    public float timeToFade = 1f;

    private Color iColor;

    private float eTime = 0f;


    RectTransform textTransform;
    TextMeshProUGUI textMesh;

    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMesh = GetComponent<TextMeshProUGUI>();
        iColor = textMesh.color;
    }

    private void Update()
    {
        textTransform.position += moveSpeed * Time.deltaTime;

        eTime += Time.deltaTime;

        if(eTime<timeToFade)
        {
            float fadeAlpha = iColor.a * (1f - (eTime / timeToFade));
            textMesh.color = new Color(iColor.r, iColor.g, iColor.b, fadeAlpha);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
