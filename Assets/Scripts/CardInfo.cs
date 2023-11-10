using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardInfo : MonoBehaviour
{
    public int objectIndex;
    public ObjectsDatabaseSO dataBase;
    public Image Logo;
    public TextMeshProUGUI Name;

    public void ShowCardInfo()
    {
        var objectData = dataBase.objectsData[objectIndex];
        Logo.sprite = objectData.Sprite;
        Name.text = objectData.Name;
    }

    private void Start()
    {
        ShowCardInfo();
    }
}
