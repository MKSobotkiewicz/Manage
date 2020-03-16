using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Manage.Characters;
using Manage.Units;

namespace Manage.UI
{
    public class Item:MonoBehaviour
    {
        public ItemType ItemType { get; private set; }
        public RawImage Icon;
        public Canvas InfoPanel;

        private Canvas inventoryCanvas;
        private ItemSlot itemSlot;

        protected bool isDragged = false;

        public void Setup(ItemType itemType, Canvas _inventoryCanvas)
        {
            inventoryCanvas = _inventoryCanvas;
            ItemType = itemType;
            Icon.texture = ItemType.Icon();
        }

        public void DragStart()
        {
            GetComponent<Canvas>().overrideSorting = true;
            GetComponent<Canvas>().sortingOrder = 999;
            if (itemSlot != null)
            {
                itemSlot.RemoveItem();
                itemSlot = null;
            }
            else
            {
                transform.SetParent(inventoryCanvas.transform.parent.parent.parent, true);
            }
            isDragged = true;
            InfoPanel.enabled = false;
        }

        public void DragEnd()
        {
            GetComponent<Canvas>().overrideSorting = false;
            isDragged = false;
            if (inventoryCanvas != null)
            {
                PutInInventoryCanvas();
            }
            CheckForItemSlot();
        }

        public void PutInInventoryCanvas()
        {
            itemSlot = null;
            transform.SetParent(inventoryCanvas.transform, false);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)inventoryCanvas.transform);
        }

        public void PutInItemSlot(ItemSlot _itemSlot)
        {
            if (_itemSlot != null)
            {
                itemSlot = _itemSlot;
                itemSlot.PutItem(this);
            }
        }

        private void CheckForItemSlot()
        {
            var grl = transform.parent.parent.parent.parent.GetComponentsInChildren<GraphicRaycaster>();
            var ped = new PointerEventData(null);
            ped.position = Input.mousePosition;
            var results = new List<RaycastResult>();
            foreach (var gr in grl)
            {
                var localResults = new List<RaycastResult>();
                gr.Raycast(ped, localResults);
                foreach (var item in localResults)
                {
                    UnityEngine.Debug.Log(gr.gameObject.name + " - " + item.gameObject.name);
                }
                results.AddRange(localResults);
            }
            foreach (var item in results)
            {
                var _itemSlot = item.gameObject.GetComponent<ItemSlot>();
                PutInItemSlot(_itemSlot);
            }
            GetComponentInParent<CharacterId>().UpdateInventory();
        }
    }
}
