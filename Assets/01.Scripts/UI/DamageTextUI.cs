using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextUI : MonoBehaviour
{
    RectTransform rect;
    // Start is called before the first frame update
    void OnEnable()
    {
        rect = gameObject.GetComponent<RectTransform>();
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
}
