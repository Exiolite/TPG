//Copyright Ex/IO 2020

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Managers
{
    // Менеджер ивентов
    [System.Serializable]
    public static class EventManager
    {
        // GameEvents
        public static PlayerDead playerDead = new PlayerDead();
        public static EnemyDead enemyDead = new EnemyDead();

        // InteractionEvets
        public static PlayerInteraction playerIneraction = new PlayerInteraction();
        
        public static PlayerSpottedByEnemy playerSpottedByEnemy = new PlayerSpottedByEnemy();
    }

    public class PlayerDead : UnityEvent { }
    public class EnemyDead : UnityEvent { }

    public class PlayerInteraction : UnityEvent <GameObject> { }
    

    public class PlayerSpottedByEnemy : UnityEvent<Transform> { }
}