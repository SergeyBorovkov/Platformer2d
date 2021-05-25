using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    [SerializeField] private int _coinAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Coin>(out Coin thief))
        {
            Debug.Log("МОНЕТКА!!!");
            
            _coinAmount++;

            Destroy(collision.gameObject);
        }
    }
}
