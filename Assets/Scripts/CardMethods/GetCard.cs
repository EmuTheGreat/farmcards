using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GetCard : MonoBehaviour, ISaveState
{
    [SerializeField]
    GameObject parent;

    [SerializeField]
    private ObjectsDatabaseSO dataBase;

    [SerializeField]
    private Button cardPrefab;

    [SerializeField]
    private PlacementSystem placementSystem;

    private List<int> cardsInHand;

    private List<int> indexList = new List<int>() { 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 2, 3, 3, 4, 4, 4 };

    private bool flag = true;

    public void CreateRandomCard()
    {
        var r = new System.Random();
        int rndIndex = r.Next(indexList.Count);
        int index = indexList[rndIndex];

        CreateCard(index);
    }

    public void CreateCard(int index)
    {
        Button newCard = Instantiate(cardPrefab, parent.transform);
        var cardInfo = newCard.GetComponent<CardInfo>();
        cardInfo.objectIndex = index;
        cardInfo.GetComponent<Image>().sprite = dataBase.objectsData[index].Background;
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
            CreateRandomCard();
            yield return new WaitForSeconds(0.08f);
        }
        flag = true;
    }

    private void AddCardInRotaition(int wight, int index)
    {
        for (int i = 0; i < wight; i++)
        {
            indexList.Add(index);
        }
    }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("CardsInHand"))
        {
            cardsInHand = new();
        }
        else
        {
            Load();
            cardsInHand = new();
        }
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("CardsInHand"))
        {
            CreateCard(0);
            CreateCard(0);
            CreateCard(1);
            CreateCard(2);
            CreateCard(3);
        }
    }

    public void Save()
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            var cardInfo = parent.transform.GetChild(i).GetComponent<CardInfo>();
            cardsInHand.Add(cardInfo.objectIndex);
        }
        string cardsInHandString = JsonUtility.ToJson(new ListContainer<int>(cardsInHand));
        Debug.Log(cardsInHandString);
        PlayerPrefs.SetString("CardsInHand", cardsInHandString);
    }

    public void Load()
    {
        string cardsInHandString = PlayerPrefs.GetString("CardsInHand");
        ListContainer<int> cards = JsonUtility.FromJson<ListContainer<int>>(cardsInHandString);
        foreach (var index in cards.list)
        {
            CreateCard(index);
        }
    }
}
