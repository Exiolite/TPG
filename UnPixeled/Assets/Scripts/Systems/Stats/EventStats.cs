using UnityEngine.Events;

namespace Systems.Stats
{
    public static class EventStats
    {
        public static readonly UpdateStats UpdateStats = new UpdateStats();
        
        public static readonly ChangePlayerHealth ChangePlayerHealth = new ChangePlayerHealth();
        public static RegenerateHealthFlag regenerateHealthFlag = new RegenerateHealthFlag();
        
        public static readonly ChangePlayerEnergy ChangePlayerEnergy = new ChangePlayerEnergy();
        public static RegenerateEnergyFlag regenerateEnergyFlag = new RegenerateEnergyFlag();
    }
    public class UpdateStats : UnityEvent { }
    
    public class ChangePlayerHealth : UnityEvent <float> { }
    public class RegenerateHealthFlag : UnityEvent <bool> { }
    
    public class ChangePlayerEnergy : UnityEvent <float> { }
    public class RegenerateEnergyFlag : UnityEvent <bool> { }
}