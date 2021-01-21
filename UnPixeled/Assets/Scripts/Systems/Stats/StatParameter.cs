using UnityEngine;

namespace Systems.Stats
{
    [System.Serializable]
    public class StatParameter
    {
        [SerializeField] private float stat;
        [SerializeField] private float maxStat;

        public float Stat
        {
            get => stat;
            set => stat = value;
        }
        
        public void ChangeStat(float value)
        {
            stat = Mathf.Clamp(stat + value, 0, 100);
        }

        public void RegenerateStat()
        {
            stat = Mathf.Clamp(stat + (0.1f * Time.deltaTime), 0, 100);
        }

        public bool CheckStat()
        {
            return !(stat > 0);
        }
    }
}