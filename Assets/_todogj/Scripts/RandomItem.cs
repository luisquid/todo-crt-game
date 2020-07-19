using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItem : MonoBehaviour
{
    public GameObject item;
    public GameObject enemy;
    public float randomSpeed = 0.5f;

    private void OnEnable()
    {
        item.SetActive(false);
        enemy.SetActive(true);

        StartCoroutine(ChangeItem());
    }

    IEnumerator ChangeItem()
    {
        item.SetActive(!item.activeSelf);
        enemy.SetActive(!item.activeSelf);

        yield return new WaitForSeconds(randomSpeed);

        StartCoroutine(ChangeItem());
    }
}
