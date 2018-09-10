using System;
using System.Collections;
using Playmode.Ennemy.BodyParts;
using Playmode.Ennemy.Strategies;
using Playmode.Entity.Senses;
using Playmode.Entity.Status.Health;
using Playmode.Event;
using Playmode.Movement;
using Playmode.Util.Values;
using UnityEngine;

namespace Playmode.Ennemy
{
    public delegate void DeathEventHandler(EnnemyController ennemyController);

    public class EnnemyController : MonoBehaviour
    {
        public event DeathEventHandler OnDeathEnemy;

        [Header("Body Parts")] [SerializeField] private GameObject body;
        [SerializeField] private GameObject hand;
        [SerializeField] private GameObject sight;
        [SerializeField] private GameObject typeSign;
        [Header("Type Images")] [SerializeField] private Sprite normalSprite;
        [SerializeField] private Sprite carefulSprite;
        [SerializeField] private Sprite cowboySprite;
        [SerializeField] private Sprite camperSprite;
        [SerializeField] private Sprite zombieSprite;
        [Header("Behaviour")] [SerializeField] private GameObject startingWeaponPrefab;

        private EventHandlerEnemyDeath enemyDeathChannel;

        private Health health;
        private Mover mover;
        private EnnemySensor ennemySensor;
        private PickableSensor pickableSensor;
        private HitSensor hitSensor;
        private HandController handController;

        private IEnnemyStrategy strategy;

        private bool invincible = false;
        private string lastEnemyThatHitName = null;

        //BEN_CORRECTION : Nommage pas standard C#. Doit débuter par une majuscule.
        //                 Aussi, semble inutilisé.
        public bool isEarlySearching { get; private set; }

        private void Awake()
        {
            ValidateSerialisedFields();
            enemyDeathChannel = GameObject.FindWithTag(Tags.GameController).GetComponent<EventHandlerEnemyDeath>();

            InitializeComponent();
            CreateStartingWeapon();
        }

        private void ValidateSerialisedFields()
        {
            if (body == null)
                throw new ArgumentException("Body parts must be provided. Body is missing.");
            if (hand == null)
                throw new ArgumentException("Body parts must be provided. Hand is missing.");
            if (sight == null)
                throw new ArgumentException("Body parts must be provided. Sight is missing.");
            if (typeSign == null)
                throw new ArgumentException("Body parts must be provided. TypeSign is missing.");
            if (normalSprite == null)
                throw new ArgumentException("Type sprites must be provided. Normal is missing.");
            if (carefulSprite == null)
                throw new ArgumentException("Type sprites must be provided. Careful is missing.");
            if (cowboySprite == null)
                throw new ArgumentException("Type sprites must be provided. Cowboy is missing.");
            if (camperSprite == null)
                throw new ArgumentException("Type sprites must be provided. Camper is missing.");
            if (zombieSprite == null)
                throw new ArgumentException("Type sprites must be provided. Zombie is missing.");
            if (startingWeaponPrefab == null)
                throw new ArgumentException("StartingWeapon prefab must be provided.");
        }

        private void InitializeComponent()
        {
            health = transform.parent.GetComponentInChildren<Health>();
            mover = GetComponent<Mover>();
            var rootTransform = transform.root;
            ennemySensor = rootTransform.GetComponentInChildren<EnnemySensor>();
            pickableSensor = rootTransform.GetComponentInChildren<PickableSensor>();
            hitSensor = rootTransform.GetComponentInChildren<HitSensor>();
            handController = hand.GetComponent<HandController>();
        }

        private void CreateStartingWeapon()
        {
            handController.Take(Instantiate(
                startingWeaponPrefab,
                Vector3.zero,
                Quaternion.identity
            ));
        }

        private void OnEnable()
        {
            hitSensor.OnHit += OnHit;
            health.OnHealthChange += OnHealthChange;
        }

        private void Update()
        {
            //BEN_REVIEW : J'aurais mis ça dans un bloc #if UNITY_EDITOR pour éviter que cela se ramasse dans la release
            //             finale du jeu.
            if(strategy == null)
            {
                Configure(EnnemyStrategy.Normal, Color.black, "Test");
                Debug.Log("Created test enemy");
            }
            strategy.Act();
        }

        private void OnDisable()
        {
            hitSensor.OnHit -= OnHit;
            health.OnHealthChange -= OnHealthChange;
        }

        public void Configure(EnnemyStrategy strategy, Color color, string name)
        {
            body.GetComponent<SpriteRenderer>().color = color;
            sight.GetComponent<SpriteRenderer>().color = color;
            gameObject.transform.root.name = name;
            isEarlySearching = false; //BEN_CORRECTION : Inutilisé au final...
            
            switch (strategy)
            {
                case EnnemyStrategy.Careful:
                    typeSign.GetComponent<SpriteRenderer>().sprite = carefulSprite;
                    this.strategy = new Careful(mover, handController, ennemySensor, pickableSensor, health);
                    break;
                case EnnemyStrategy.Cowboy:
                    typeSign.GetComponent<SpriteRenderer>().sprite = cowboySprite;
                    this.strategy = new Cowboy(mover, handController, ennemySensor, pickableSensor);
                    break;
                case EnnemyStrategy.Camper:
                    typeSign.GetComponent<SpriteRenderer>().sprite = camperSprite;
                    this.strategy = new Camper(mover, handController, ennemySensor, pickableSensor, health);
                    break;
                case EnnemyStrategy.Zombie:
                    typeSign.GetComponent<SpriteRenderer>().sprite = zombieSprite;
                    this.strategy = new ZombieStrategy(mover, ennemySensor);
                    break;
                default:
                    typeSign.GetComponent<SpriteRenderer>().sprite = normalSprite;
                    this.strategy = new Normal(mover, handController, ennemySensor);
                    break;
            }
        }

        private void OnHit(int hitPoints, string enemyThatHitName)
        {
            //BEN_CORRECTION : Il aurait été préférable de gérer l'invincibilité dans le composant "Health". Ce dernier
            //                 s'occupe déjà de tout ce qui concerne les points de vie après tout.
            //
            //                 Voila un peu pourquoi je dis à chaque fois de lire le code de base afin de savoir ce qui
            //                 existe déjà.
            if (invincible == false)
            {
                lastEnemyThatHitName = enemyThatHitName;
                health.Hit(hitPoints);
            }
        }

        //BEN_CORRECTION : En lien avec le commentaire du haut, il aurait aussi été préférable de publier sur le
        //                 EnemyDeathChannel à partir du composant Health.
        //
        //                 Maintenant que j'y pense, j'aurais vraiment pas du appeler cette classe "EnnemyController"...
        //                 vous avez tendance à vouloir tout faire la dedans.
        private void OnHealthChange(int newHealth)
        {
            if(newHealth <= 0)
            {
                if(!(strategy is ZombieStrategy))
                {
                    enemyDeathChannel.Publish(new EnemyDeathData(gameObject.transform.root.name, lastEnemyThatHitName));
                }
                NotifyOnEnemyDeath();
                Destroy(this.transform.parent.gameObject); //BEN_CORRECTION : Bogue potentiel. Utilisez "transform.root".
            }
        }

        public void Take(GameObject gameObject)
        {
            handController.Take(gameObject);
        }

        public void Heal(int healthPoints)
        {
            health.Heal(healthPoints);
        }

        private void NotifyOnEnemyDeath()
        {
            OnDeathEnemy?.Invoke(this);
        }
        
        public void ActivateInvincibility(int timeInvincible)
        {
            StartCoroutine(ActivateInvincibilityRoutine(timeInvincible));
        }
        private IEnumerator ActivateInvincibilityRoutine(int timeInvincible)
        {
            invincible = true;
            yield return new WaitForSeconds(timeInvincible);
            invincible = false;
        }

        //BEN_CORRECTION : Nom indique pas ce que cela fait vraiment.
        public IEnnemyStrategy GetStrategyType()
        {
            return strategy;
        }
    }
}