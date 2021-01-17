//Copyright Ex/IO 2020

using UnityEngine;
using UnityEngine.AI;

public class HealthStatsActor : MonoBehaviour
{
    FloatingText damageText;

    public float health = 100;
    public bool dead = false;
    private float destroyDelay = 1;
    private float deadTime;
    private GameObject dieParticles;
    private GameObject hitParticles;
    [SerializeField] private InventorySlot[] dropFromMob;


    private void Awake()
    {
        hitParticles = Instantiate(GameManager.instance.vfxManager.soulHitted, transform);
        hitParticles.GetComponent<ParticleSystemRenderer>().material = GetComponent<MeshRenderer>().material;
        hitParticles.GetComponent<ParticleSystemRenderer>().trailMaterial = GetComponent<MeshRenderer>().material;
        damageText = GameManager.instance.floatingText.GetComponent<FloatingText>();
    }


    void Update()
    {
        if (dead)
        {
            if (Time.time > destroyDelay + deadTime)
            {
                Destroy(gameObject);
                Destroy(dieParticles.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        float damage = 0;

        if (GameManager.instance.playerBehaviour.playerEquipment.weaponSlot != null)
            if (UnityEngine.Random.Range(0, 100) <=
                GameManager.instance.playerBehaviour.playerEquipment.weaponSlot.CritChance)
                damage = GameManager.instance.playerBehaviour.playerEquipment.weaponSlot.WeaponDamage * 2;
            else
                damage = GameManager.instance.playerBehaviour.playerEquipment.weaponSlot.WeaponDamage;

        if (other.tag == "Weapon" && health >= 0)
            HealthDamage(damage);
    }


    public void EnemyDead()
    {
        for (int i = 0; i < dropFromMob.Length; i++)
        {
            GameObject deadDrop = Instantiate(GameManager.instance.soulDrop, transform);
            deadDrop.transform.parent = null;
            deadDrop.GetComponent<Item>().item.count = UnityEngine.Random.Range(1, dropFromMob[i].count);
            if (dropFromMob[i].item.itemName == "Pixels")
                deadDrop.GetComponentInChildren<MeshRenderer>().material = GetComponent<MeshRenderer>().material;
        }

        dead = true;
        deadTime = Time.time;
        GetComponent<NavMeshAgent>().isStopped = true;
        GetComponent<MeshRenderer>().enabled = false;
        dieParticles = Instantiate(GameManager.instance.vfxManager.soulDead, transform);
        dieParticles.GetComponent<ParticleSystemRenderer>().material = this.GetComponent<MeshRenderer>().material;
    }

    public void HealthDamage(float damage)
    {
        if (health > 0)
        {
            hitParticles.GetComponent<ParticleSystem>().Play();
            damageText.showText(transform, damage, 1);

            health -= damage;

            EventAudio.damageEnemySoul.Invoke(GetComponent<AudioSource>());

            switch (GetComponent<EnemyAI_Soul>().ai_Type)
            {
                case EnemyAI_Soul.AiType.melee:
                    GetComponent<NavMeshAgent>().Move(transform.forward * -2);
                    break;
                case EnemyAI_Soul.AiType.range:
                    break;
            }

            if (health <= 0)
            {
                EnemyDead();
            }
        }
    }
}