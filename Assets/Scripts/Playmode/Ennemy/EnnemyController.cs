﻿using System;
using System.Collections;
using Playmode.Ennemy.BodyParts;
using Playmode.Ennemy.Strategies;
using Playmode.Entity.Destruction;
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
        [Header("Behaviour")] [SerializeField] private GameObject startingWeaponPrefab;

        private EventHandlerEnemyDeath enemyDeathChannel;

        private Health health;
        private Mover mover;
        private Destroyer destroyer;
        private EnnemySensor ennemySensor;
        private PickableSensor pickableSensor;
        private HitSensor hitSensor;
        private HandController handController;

        private IEnnemyStrategy strategy;

        private bool invincible = false;
        private string lastEnemyThatHitName = null;

        public string Name { get; private set; }
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
            if (startingWeaponPrefab == null)
                throw new ArgumentException("StartingWeapon prefab must be provided.");
        }

        private void InitializeComponent()
        {
            health = transform.parent.GetComponentInChildren<Health>();
            mover = GetComponent<Mover>();
            destroyer = GetComponent<RootDestroyer>();

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
            Name = name;
            isEarlySearching = false;
            
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
                default:
                    typeSign.GetComponent<SpriteRenderer>().sprite = normalSprite;
                    this.strategy = new Normal(mover, handController, ennemySensor);
                    break;
            }
        }

        private void OnHit(int hitPoints, string enemyThatHitName)
        {
            if (invincible == false)
            {
                lastEnemyThatHitName = enemyThatHitName;
                health.Hit(hitPoints);
            }
        }

        private void OnHealthChange(int newHealth)
        {
            if(newHealth <= 0)
            {
                NotifyOnEnemyDeath();
                enemyDeathChannel.Publish(new EnemyDeathData(Name, lastEnemyThatHitName));
                destroyer.Destroy();
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
        public void EarlyMedpackSearch(int timeForSearch)
        {
            StartCoroutine(EarlyMedpackSearchRoutine(timeForSearch));
        }

        private IEnumerator EarlyMedpackSearchRoutine(int timeForSearch)
        {
            isEarlySearching = true;
            yield return new WaitForSeconds(timeForSearch);
            isEarlySearching = false;
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
    }
}