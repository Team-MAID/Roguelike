using System.Collections;
using System.Collections.Generic;
using Items.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

public class BlueBook : MonoBehaviour
{
    [FormerlySerializedAs("consumableItemData")] [SerializeField] private PotionItemSO potionItemData;
    // Start is called before the first frame update
    private void Start()
    {
        potionItemData.ConsumingItem += Consume;
    }

    public void Consume(GameObject consumer)
    {
        //do smtg
    }
}
