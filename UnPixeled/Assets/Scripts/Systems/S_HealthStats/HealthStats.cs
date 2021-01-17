//Copyright Ex/IO 2020

using Systems.S_HealthStats.Stats;

namespace Systems.S_HealthStats
{
    public class HealthStats
    {
        private StatParameter _health;
        public StatParameter Health
        {
            get => _health;
            set => _health = value;
        }
        
        private StatParameter _energy;
        public StatParameter Energy
        {
            get => _energy;
            set => _energy = value;
        }


        /*if(energy < 1)
        {
            EventManager.disableDash.Invoke(true);
            EventManager.disableDefence.Invoke(true);
        }*/



        /*if (!GameManager.instance.inputManager.Dash() && energy <= energyMax)
            energy += energyRegeneration * Time.deltaTime;

        if (energy > minimumEnergyForDash)
            EventManager.disableDash.Invoke(false);
        if (energy > minimumEnergyForDefence)
            EventManager.disableDefence.Invoke(false);*/
    }
}