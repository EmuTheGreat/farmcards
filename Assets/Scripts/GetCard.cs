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

    private List<int> indexList = new List<int>() { 1, 2 };

    public void CreateCard()
    {
        var r = new System.Random();
        int rndIndex = r.Next(indexList.Count);
        int index = indexList[rndIndex];
        //CardInfo cardInfo = cardPrefab.GetComponent<CardInfo>();

        if (parent.transform.childCount < 5)
        {
            Button newCard = Instantiate(cardPrefab, parent.transform);
            var cardInfo = newCard.GetComponent<CardInfo>();
            cardInfo.objectIndex = index;
            cardInfo.GetComponent<Image>().sprite = dataBase.objectsData[index].Background; //Замена заднего фона карточки
            newCard.onClick.AddListener(() => placementSystem.StartPlacement(cardInfo.objectIndex));
        }
    }
}
