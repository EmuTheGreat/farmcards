using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    private List<int> indexList = new List<int>() { 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 2, 3, 3 };

    private bool flag = true;

    public void CreateCard()
    {
        var r = new System.Random();
        int rndIndex = r.Next(indexList.Count);
        int index = indexList[rndIndex];

        Button newCard = Instantiate(cardPrefab, parent.transform);
        var cardInfo = newCard.GetComponent<CardInfo>();
        cardInfo.objectIndex = index;
        cardInfo.GetComponent<Image>().sprite = dataBase.objectsData[index].Background; //Замена заднего фона карточки
        newCard.onClick.AddListener(() => placementSystem.StartPlacement(cardInfo.objectIndex));
    }

    public void FillHand()
    {
        if (flag)
        {
            flag = false;
            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait()
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            Destroy(parent.transform.GetChild(i).GameObject());
        }
        for (int i = 0; i < 5; i++)
        {
            CreateCard();
            yield return new WaitForSeconds(0.08f);
        }
        flag = true;
    }
}
