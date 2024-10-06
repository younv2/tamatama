using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum DamageType {NORMAL, CRITICAL }
public class DamageTextUI : MonoBehaviour
{
    RectTransform rect;
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void OnEnable()
    {
        rect = gameObject.GetComponent<RectTransform>();
        text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(OnAnimation());
    }
    private void OnDisable()
    {
        StopCoroutine(OnAnimation());
    }

    IEnumerator OnAnimation()
    {
        float time = 0;
        while (true)
        {
            rect.position = rect.position + new Vector3 (0f, 0.005f, 0f);
            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;
            if (time > 1f)
                break;
        }
        gameObject.SetActive(false);
        yield return null;
    }
    public void ShowDamage(int damage, DamageType type)
    {
        text.text = damage.ToString();
        switch(type)
        {
            case DamageType.NORMAL:
                text.color = Color.white;
                break;
            case DamageType.CRITICAL:
                text.color = Color.yellow;
                break;
        }
        
    }
}
