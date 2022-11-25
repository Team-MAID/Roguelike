using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBook : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private ConsumableItemSO consumableItemData;
    // Start is called before the first frame update
    private void Start()
    {
        consumableItemData.ConsumingItem += Consume;
    }

    public void Consume(GameObject consumer)
    {
        //do smtg
    }
}
