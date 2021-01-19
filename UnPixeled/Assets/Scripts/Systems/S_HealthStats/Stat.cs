using UnityEngine;

namespace Systems.S_HealthStats
{
    public class StatParameter
    {
        private float _stat;
        public float Stat
        {
            get => _stat;
            set => _stat = value;
        }
        
        private float _maxStat;
        public float MaxStat
        {
            get => _maxStat;
            set => _maxStat = value;
        }
        
        private float _statRegenerationValue;
        public float StatRegenerationValue
        {
            get => _statRegenerationValue;
            set => _statRegenerationValue = value;
        }
        
        
        
        public void ChangeStat(float value)
        {
            _stat += Mathf.Clamp(value, 0, _maxStat);
        }

        public void RegenerateStat()
        {
            _stat += Mathf.Clamp(_statRegenerationValue, 0, _maxStat);
        }
    }
}