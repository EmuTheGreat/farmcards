using System.Collections;
using System.Collections.Generic;
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
        //Debug.Log(parent.transform.childCount);
        //Debug.Log(cardPrefab);
        var newCard = Instantiate(cardPrefab, parent.transform);
        newCard.onClick.AddListener(()=> placementSystem.StartPlacement(0));
    }
}
