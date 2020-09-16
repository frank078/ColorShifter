using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBehavior : MonoBehaviour
{
    // Variables
    GameObject shopItem;
    GameObject g;
    [SerializeField] Transform shopScrollView;

    // Start is called before the first frame update
    void Start()
    {
        shopItem = shopScrollView.GetChild(0).gameObject;

        // change final number depending on how many items are in the shop
        // remember to also change the constraint count in the content scroll
        for (int i = 0; i < 5; i++)
        {
            g = Instantiate(shopItem, shopScrollView);
        }

        Destroy(shopItem);
    }
}
