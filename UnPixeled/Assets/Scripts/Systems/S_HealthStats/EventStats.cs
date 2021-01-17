using UnityEngine.Events;

namespace Systems.S_HealthStats
{
    public class EventStats
    {
        public static UpdatePlayerHealth updatePlayerHealth = new UpdatePlayerHealth();
        public static UpdatePlayerEnergy updatePlayerEnergy = new UpdatePlayerEnergy();
        public static UpdatePlayerExperience updatePlayerExperience = new UpdatePlayerExperience();
        
        
        
        public static RegenerateEnergyFlag regenerateEnergyFlag = new RegenerateEnergyFlag();
        public static ChangePlayerEnergy changePlayerEnergy = new ChangePlayerEnergy();
        
        public static RegenerateHealthFlag regenerateHealthFlag = new RegenerateHealthFlag();
        public static ChangePlayerHealth changePlayerHealth = new ChangePlayerHealth();
        
        public static PlayerDead playerDead = new PlayerDead();
        public static EnemyDead enemyDead = new EnemyDead();
    }

    public class PlayerDead : UnityEvent { }
    public class EnemyDead : UnityEvent { }

    public class UpdatePlayerHealth : UnityEvent <float>  { }
    public class UpdatePlayerEnergy : UnityEvent <float>  { }
    public class UpdatePlayerExperience : UnityEvent <float>  { }
    
    
    
    public class RegenerateEnergyFlag : UnityEvent <bool> { }
    public class ChangePlayerEnergy : UnityEvent <float> { }
    
    public class RegenerateHealthFlag : UnityEvent <bool> { }
    public class ChangePlayerHealth : UnityEvent <float> { }
}