using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Managers;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    private Text costText;
    private Image itemImage;
    private int shopID;
    private ShopItemData itemData;
    
   //Creating a Static method for adding a component to a GameObject with parameters following the Factory Pattern
    public static ShopItem CreateComponent (GameObject target, int shopID, ShopItemData newItemData) {
       ShopItem newComponent = target.AddComponent<ShopItem>();
       newComponent.itemData = newItemData;
       newComponent.shopID = shopID;
       newComponent.costText = target.GetComponentInChildren<Text>();
       newComponent.itemImage = target.GetComponentInChildren<Image>();
       newComponent.Initialize();
       target.GetComponent<Button>().onClick.AddListener(newComponent.OnClicked);
       return newComponent;
    }

    //Causes Physical Pain
    //Hard Coded But will be changed at a later data... deadlines
    //TODO
    //Make StatsAndAchievements.cs Generic 
    //Deep Clean this monstrosity
    private void Initialize()
    {
        if (itemData.isUnlockedFromStart)
        {
            Unlock();
            return;
        }
    }

    public void Unlock()
    {
        costText.text = "Unlocked";
        itemImage.sprite = itemData.unLockedTexture2D;
    }

    private void OnClicked()
    {
        
        
    }
}


