using System;
using UnityEngine;

namespace InventorySystem
{
    public class ItemAssets : MonoBehaviour
    {
        public static ItemAssets Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        [SerializeField] private Transform itemWorld;
        public Transform ItemWorld => itemWorld;
        
        [SerializeField] private Sprite beefSprite;
        public Sprite BeefSprite => beefSprite;
        
        [SerializeField] private Sprite redBookSprite;
        public Sprite RedBookSprite => redBookSprite;      
        
        [SerializeField] private Sprite blueBookSprite;
        public Sprite BlueBookSprite => blueBookSprite;      
        
        [SerializeField] private Sprite candySprite;
        public Sprite CandySprite => candySprite;        
        
        [SerializeField] private Sprite logSprite;
        public Sprite LogSprite => logSprite;
    }
}