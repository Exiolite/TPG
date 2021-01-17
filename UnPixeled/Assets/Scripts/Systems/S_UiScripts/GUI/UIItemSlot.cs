//Copyright Ex/IO 2020
using UnityEngine;
using UnityEngine.UI;

public class UIItemSlot : MonoBehaviour
{
    public ItemData item;
    public GameObject textName;
    public GameObject textCount;



    public void OnClicked()
    {
        if (item != null)
            EventManager.equipmentAction.Invoke(item);
    }

    public void SetSlot(ItemData _item, int _count)
    {
        item = _item;
        textName.GetComponent<Text>().text = item.itemName;

        if (_count > 1)
            textCount.GetComponent<Text>().text = _count.ToString();
        else
            textCount.GetComponent<Text>().text = "";

        GetComponent<Image>().sprite = item.itemIcon;
    }
}
