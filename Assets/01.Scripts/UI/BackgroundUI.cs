using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BackgroundUI : MonoSingleton<BackgroundUI>
{
    TextMeshProUGUI goldText;
    TextMeshProUGUI cashText;
    TextMeshProUGUI nameText;

    User user;
    public void Init()
    {
        user = GameManager.Instance.user; 
        goldText = transform.Find("Gold").GetChild(0).GetComponent<TextMeshProUGUI>();
        cashText = transform.Find("Cash").GetChild(0).GetComponent<TextMeshProUGUI>();
        nameText = transform.Find("Name").GetComponent<TextMeshProUGUI>();

        goldText.text = user.Inventory.GetGold().ToString();
        cashText.text = user.Inventory.GetCash().ToString();
        nameText.text = user.Nickname.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        goldText.text = user.Inventory.GetGold().ToString();
        cashText.text = user.Inventory.GetCash().ToString();
        nameText.text = user.Nickname.ToString();
    }
}
