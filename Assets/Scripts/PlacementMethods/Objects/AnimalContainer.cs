using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimalContainer : MonoBehaviour
{
    public List<Vector2Int> occupiedPosition;
    public TMP_Text capacityText;

    private void Start()
    {
        capacityText.gameObject.SetActive(false);
    }
}