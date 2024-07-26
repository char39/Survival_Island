using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DataInfo;

public class InventoryDrop : MonoBehaviour, IDropHandler
{

    void Start()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {   
            Item item = InventoryDrag.draggingItem.GetComponent<ItemInfo>().itemData;
            // 드래그 되고 있는 옵젝의 ItemInfo 컴포넌을 불러와 itemData 값을 대입
            // 인벤 UI에 올라간 아이템을 
            InventoryDrag.draggingItem.transform.SetParent(transform, false);
            GameManager.Instance.AddItem(item);
        }
    }
}
