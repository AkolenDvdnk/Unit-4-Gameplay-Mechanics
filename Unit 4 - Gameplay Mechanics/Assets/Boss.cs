using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static Boss instance;

    public GameObject minionPrefab;

    private void Start()
    {
        instance = this;

        StartCoroutine(SpawnMinions());
    }
    private IEnumerator SpawnMinions()
    {
        while (this != null)
        {
            yield return new WaitForSeconds(1.5f);

            Instantiate(minionPrefab, SpawnManager.instance.GenerateSpawnPosition(), transform.rotation);

            yield return new WaitForSeconds(4f);
        }
    }
}
