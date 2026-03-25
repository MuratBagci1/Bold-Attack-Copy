using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperiencePoint : MonoBehaviour, IPoolable
{
    [SerializeField] private SpriteRenderer shineSR;
    [SerializeField] private float xpDropRadius;
    [SerializeField] private float moveToPointSpeed;
    private Color originalColor;
    private float colorAlpha = .5f;
    private bool isDecreasing = true;

    private void Awake()
    {
        originalColor = shineSR.color;
    }

    private void Update()
    {
        if (colorAlpha < 0 && isDecreasing)
        {
            isDecreasing = false;
            colorAlpha = 0;
            colorAlpha += Time.deltaTime;
        }
        else if (isDecreasing)
        {
            colorAlpha -= Time.deltaTime;
        }
        else if (colorAlpha > 0.5f && !isDecreasing)
        {
            isDecreasing = true;
            colorAlpha = .5f;
            colorAlpha -= Time.deltaTime;
        }
        else if (!isDecreasing)
        {
            colorAlpha += Time.deltaTime;
        }
        shineSR.color = new Color(originalColor.r, originalColor.g, originalColor.b, colorAlpha);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ActionManager.OnXPCollected?.Invoke();

        Debug.Log("Inside XP OnTriggerEnter");

        PoolManager.Instance.Return<ExperiencePoint>(this);
    }

    public void SetXPPosition(Transform creationPointTransform)
    {
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        Vector3 offset = new Vector3(randomDir.x, randomDir.y, 0) * xpDropRadius;

        transform.position = creationPointTransform.position;

        Coroutine positionCoroutine = StartCoroutine(MoveToPosition(creationPointTransform.position + offset));
    }

    private IEnumerator MoveToPosition(Vector3 targetPos)
    {
        while (transform.position != targetPos)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, moveToPointSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void OnDespawn()
    {
        shineSR.color = originalColor;
    }

    public void OnSpawn()
    {
    }
}