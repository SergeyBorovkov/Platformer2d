using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinSpawnGenerator : MonoBehaviour
{
    [SerializeField] private Coin _template;
    [SerializeField] private List<CoinSpawnPoint> _points;
    [SerializeField] private int _generationInterval = 2;
    [SerializeField] private int _generationRounds = 3;

    private void Start()
    {
        StartCoroutine(GenerateCoin(_generationInterval, _generationRounds));
    }

    private IEnumerator GenerateCoin(int interval, int rounds)
    {
        var waitForSeconds = new WaitForSeconds(interval);

        for (int j = 0; j < rounds; j++)
        {
            for (int i = 0; i < _points.Count; i++)
            {
                Instantiate(_template.transform, new Vector3(_points[i].transform.position.x, _points[i].transform.position.y, 0), Quaternion.identity);
                yield return waitForSeconds;
            }
        }
    }
}
