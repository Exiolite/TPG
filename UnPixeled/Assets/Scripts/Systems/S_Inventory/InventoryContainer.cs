using System.Collections.Generic;
using UnityEngine;

namespace Systems.S_Inventory
{
    [CreateAssetMenu(fileName = "New ItemData", menuName = "Inventory System/Container", order = 51)]
    public class InventoryContainer : ScriptableObject
    {
        public List<InventorySlot> container = new List<InventorySlot>();
    }
}