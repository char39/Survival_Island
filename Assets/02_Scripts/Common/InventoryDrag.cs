using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Transform inventoryTr;
    private Transform itemListTr;
    private Transform itemTr;
    private CanvasGroup canvasGroup;
    public static GameObject draggingItem = null;
    

    void Start()
    {
        inventoryTr = GameObject.Find("Inventory").transform;
        itemListTr = GameObject.Find("Inventory_Item").transform;
        itemTr = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnDrag(PointerEventData eventData)  // 드래그 이벤트 발생 시 호출
    {
        itemTr.position = Input.mousePosition;      // 아이템 위치를 마우스 위치로 이동
    }

    public void OnBeginDrag(PointerEventData eventData)     // 드래그 시작 시 호출
    {
        this.transform.SetParent(inventoryTr);      // 드래그 시작 시 인벤토리의 자식으로 이동
        draggingItem = this.gameObject;             // 드래그 중인 아이템을 저장
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)       // 드래그 종료 시 호출
    {
        draggingItem = null;                        // 드래그 중인 아이템을 초기화
        canvasGroup.blocksRaycasts = true;
        if (itemTr.parent == inventoryTr)
            itemTr.SetParent(itemListTr);
        this.transform.localPosition = Vector3.zero;
    }
}
