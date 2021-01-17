using UnityEngine.Events;

namespace Core.Managers.Input
{
    [System.Serializable]
    public static class EventInput
    {
        public static DisableMovement disableMovement = new DisableMovement();
        public static DisableDash disableDash = new DisableDash();
        public static DisableDefence disableDefence = new DisableDefence();
        public static DisableInventory disableInventory = new DisableInventory();
        public static DisableMouse disableMouse = new DisableMouse();
        public static InventoryOpen inventoryOpen = new InventoryOpen();
    }
    
    public class DisableMovement : UnityEvent <bool> { }
    public class DisableDash : UnityEvent <bool> { }
    public class DisableDefence : UnityEvent <bool> { }
    public class DisableInventory : UnityEvent { }
    public class DisableMouse : UnityEvent { }
    public class InventoryOpen : UnityEvent { }
}
