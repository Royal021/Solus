using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healTextPrefab;
    
    public Canvas canvas;

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    private void OnEnable()
    {
        CharacterEvents.charDamaged += characterTakeDamage;
        CharacterEvents.charHealed += characterHeal;
    }

    private void OnDisable()
    {
        CharacterEvents.charDamaged -= characterTakeDamage;
        CharacterEvents.charHealed -= characterHeal;
    }
    public void characterTakeDamage( GameObject character, int damage )
    {
        Vector3 spawnPos = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPos, Quaternion.identity, canvas.transform).GetComponent<TMP_Text>();

        tmpText.text = damage.ToString();
    }

    public void characterHeal( GameObject character, int heal )
    {
        Vector3 spawnPos = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(healTextPrefab, spawnPos, Quaternion.identity, canvas.transform).GetComponent<TMP_Text>();

        tmpText.text = heal.ToString();
    }
}
