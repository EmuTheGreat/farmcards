using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private List<int> cardIndexToAdd = new();
    private List<int> indexList = new List<int>() { 0, 0, 0, 1, 1, 2, 3, 3, 4, 4 };

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
        var hashIndex = new HashSet<int>(indexList);
        if (!PlayerPrefs.HasKey("CardsInHand"))
        {
            CreateCard(0);
            CreateCard(0);
            CreateCard(1);
            CreateCard(2);
            CreateCard(3);

            for (int i = 0; i < dataBase.objectsData.Count; i++)
            {
                if (!hashIndex.Contains(i))
                {
                    cardIndexToAdd.Add(i);
                }
            }
        }
    }

    public void Save()
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            var cardInfo = parent.transform.GetChild(i).GetComponent<CardInfo>();
            cardsInHand.Add(cardInfo.objectIndex);
        }
        string indexInRotation = JsonUtility.ToJson(new ListContainer<int>(indexList));
        string cardsInHandString = JsonUtility.ToJson(new ListContainer<int>(cardsInHand));
        Debug.Log(cardsInHandString);
        PlayerPrefs.SetString("CardsInHand", cardsInHandString);
        PlayerPrefs.SetString("IndexInRotation", indexInRotation);
    }

    public void Load()
    {
        string indexInRotation = PlayerPrefs.GetString("IndexInRotation");
        string cardsInHandString = PlayerPrefs.GetString("CardsInHand");
        ListContainer<int> cards = JsonUtility.FromJson<ListContainer<int>>(cardsInHandString);
        ListContainer<int> indexInRot = JsonUtility.FromJson<ListContainer<int>>(indexInRotation);
        indexList = indexInRot.list;
        foreach (var e in indexList)
        {
            Debug.Log(e);
        }
        foreach (var index in cards.list)
        {
            CreateCard(index);
        }
    }

    public void MakeChoice()
    {
        var hashIndex = new HashSet<int>(indexList);

        var e = cardIndexToAdd.Except(hashIndex).ToList();
        if (e.Count != 0)
        {
            var r = new System.Random();
            int rndIndex = r.Next(e.Count);
            int index = e[rndIndex];

            AddCardInRotaition(dataBase.objectsData[index].Weight, index);
        }
    }
}
