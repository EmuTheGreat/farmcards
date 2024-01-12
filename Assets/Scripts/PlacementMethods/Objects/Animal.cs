using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public Vector2 pos;
    public float minX;
    public float maxX;
    public float movementSpeed = 0.1f;
    public float moveInterval = 0.01f; // Интервал времени между перемещениями
    public float stopTime = 0.5f; // Время, на которое животное останавливается
    public float resumeTime = 0.5f; // Время, через которое животное возобновляет движение

    private bool isMoving = true;

    private void Start()
    {
        pos = transform.position;
        Debug.Log(pos);
        minX = pos.x - 0.5f;
        maxX = pos.x + 0.5f;

        // Вызывать метод ChangePositionWithInterval каждые moveInterval секунд
        InvokeRepeating("ChangePositionWithInterval", 0f, moveInterval);
        // Остановить движение после stopTime секунд
        Invoke("StopMoving", stopTime);
    }

    void ChangePositionWithInterval()
    {
        if (isMoving)
        {
            Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), 0).normalized;
            Vector2 targetPosition = (Vector2)transform.position + randomDirection * Random.Range(1f, 5f);
            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
            StartCoroutine(MoveOverTime(targetPosition));
        }
    }

    IEnumerator MoveOverTime(Vector2 targetPosition)
    {
        float elapsedTime = 0f;
        Vector2 startingPosition = transform.position;

        while (elapsedTime < moveInterval)
        {
            transform.position = Vector2.Lerp(startingPosition, targetPosition, elapsedTime / moveInterval);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Установка точной позиции после завершения интерполяции
        transform.position = targetPosition;

        // Остановить движение после stopTime секунд
        Invoke("StopMoving", stopTime);
    }

    void StopMoving()
    {
        isMoving = false;
        Invoke("ResumeMoving", resumeTime);
    }

    void ResumeMoving()
    {
        isMoving = true;
    }
}

