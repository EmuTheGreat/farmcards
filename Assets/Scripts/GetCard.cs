using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GetCard : MonoBehaviour
{
    [SerializeField]
    GameObject parent;

    [SerializeField]
    private ObjectsDatabaseSO dataBase;

    [SerializeField]
    private Button cardPrefab;

    [SerializeField]
    private PlacementSystem placementSystem;

    public void CreateCard()
    {
        int index = 1;
        CardInfo cardInfo = cardPrefab.GetComponent<CardInfo>(); 
        
        if (cardInfo != null)
        {
            cardInfo.objectIndex = index;
        }

        Button newCard = Instantiate(cardPrefab, parent.transform);
        newCard.onClick.AddListener(()=> placementSystem.StartPlacement(cardInfo.objectIndex));
    }
}
