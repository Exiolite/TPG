//Copyright Ex/IO 2020
using System;
using UnityEngine;
using UnityEngine.Events;

// Менеджер ивентов
[System.Serializable]
public static class EventGame
{
    // GameEvents
    public static PlayerDead playerDead = new PlayerDead();
    public static EnemyDead enemyDead = new EnemyDead();

    // InteractionEvets
    public static PlayerInteraction playerIneraction = new PlayerInteraction();

    // InputEvents
    public static PlayerStatAction playerStatAction = new PlayerStatAction();
    public static UpdatePlayerStats updatePlayerStats = new UpdatePlayerStats();

    public static DisableMovement disableMovement = new DisableMovement();
    public static DisableDash disableDash = new DisableDash();
    public static DisableDefence disableDefence = new DisableDefence();

    public static InventoryOpen inventoryOpen = new InventoryOpen();
    public static DisableInventory disableInventory = new DisableInventory();

    public static DisableMouse disableMouse = new DisableMouse();


    // Inventory Events
    public static InventoryAction inventoryAction = new InventoryAction(); // Игрок делает что-то с инвентарем
    public static InventoryUpdate inventoryUpdate = new InventoryUpdate(); // Обновления интерфейса инвентаря

    public static EquipmentAction equipmentAction = new EquipmentAction(); // Игрок делает что-то с экипировкой
    public static EquipmentUpdate equipmentUpdate = new EquipmentUpdate(); // Обновления интерфейса экипировки

    public static UseHealItem useHealItem = new UseHealItem(); // Обновления интерфейса экипировки

    // AI ivents
    public static PlayerSpottedByEnemy playerSpottedByEnemy = new PlayerSpottedByEnemy(); // Игрок был замечен врагами
}

public class PlayerDead : UnityEvent { }
public class EnemyDead : UnityEvent { }

public class PlayerInteraction : UnityEvent <GameObject> { }


public class PlayerStatAction : UnityEvent <String, String, float> { }
public class UpdatePlayerStats : UnityEvent <float, float, float>  { }


public class DisableMovement : UnityEvent <bool> { }
public class DisableDash : UnityEvent <bool> { }
public class DisableDefence : UnityEvent <bool> { }


public class InventoryOpen : UnityEvent { }
public class DisableInventory : UnityEvent { }


public class DisableMouse : UnityEvent { }


public class InventoryAction : UnityEvent<Inventory.InventoryActions, ItemData, int> { }
public class InventoryUpdate : UnityEvent { }


public class EquipmentAction : UnityEvent<ItemData> { }
public class EquipmentUpdate : UnityEvent { }
public class UseHealItem : UnityEvent { }


public class PlayerSpottedByEnemy : UnityEvent<Transform> { }
