using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBehavior : MonoBehaviour
{
    // Variables
    [System.Serializable] class ShopItem
    {
        public Mesh ItemObject;
        public int Price;
        public bool IsPurchased = false;
    }

    [SerializeField] List<ShopItem> ShopItemsList;

    GameObject itemTemplate;
    GameObject g;
    [SerializeField] Transform shopScrollView;

    // Start is called before the first frame update
    void Start()
    {
        itemTemplate = shopScrollView.GetChild(0).gameObject;

        // remember to also change the constraint count in the content scroll
        int len = ShopItemsList.Count;
        for (int i = 0; i < len; i++)
        {
            g = Instantiate(itemTemplate, shopScrollView);
            g.transform.GetChild(0).GetComponent<MeshFilter>().mesh = ShopItemsList[i].ItemObject;
            g.transform.GetChild(2).GetComponent<Text>().text = ShopItemsList[i].Price.ToString();
            g.transform.GetChild(3).GetComponent<Button>().interactable = !ShopItemsList[i].IsPurchased;
        }

        Destroy(itemTemplate);
    }
}
