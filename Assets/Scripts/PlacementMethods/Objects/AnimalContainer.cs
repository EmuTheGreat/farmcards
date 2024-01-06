using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalContainer : MonoBehaviour
{
    private Collider2D animalContainerCollider;
    public GameObject parent;
    public List<int> animalsId;
    public ObjectsDatabaseSO dataBase;

    private void Start()
    {
        animalContainerCollider = GetComponent<Collider2D>();
        Debug.Log(parent.transform.position);
    }

    public void AddAnimal(int id)
    {
        animalsId.Add(id);
    }
}