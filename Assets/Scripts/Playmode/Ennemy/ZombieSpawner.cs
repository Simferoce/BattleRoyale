using Playmode.Ennemy;
using Playmode.Ennemy.Strategies;
using Playmode.Entity.Senses;
using Playmode.Movement;
using Playmode.Util.Values;
using System;
using System.Collections;
using UnityEngine;

//BEN_CORRECTION : Pas de namespace ?

public class ZombieSpawner : MonoBehaviour {

    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private int numbOfZombies;
    [SerializeField] private float timeBeforeZombieSpawn;

    private void Awake()
    {
        ValidateField();
    }

    private void ValidateField()
    {
        if (zombiePrefab == null)
            throw new ArgumentException("Zombie prefab not present");
    }

    private void OnEnable()
    {
        StartCoroutine("SpawnZombies");
    }

    private void SpawnZombie(Vector2 position)
    {
        GameObject zombie = Instantiate(zombiePrefab, position, Quaternion.identity);
        EnnemyController controller = zombie.GetComponentInChildren<EnnemyController>();
        GameObject control = controller.gameObject;
        control.GetComponent<Mover>().Speed = 2; //BEN_CORRECTION : Valeur magique. Constante.
        GameObject body = controller.transform.parent.GetComponentInChildren<HitSensor>().gameObject;
        HitStimulusZombie hitStimulus = body.AddComponent<HitStimulusZombie>(); //BEN_CORRECTION : Pourquoi créer ce composant là ? Pourquoi pas avoir vraiment fait un prefab de Zombie ?
        controller.Configure(EnnemyStrategy.Zombie, Color.black, "Zombie");
        hitStimulus.ShooterName = "Zombie"; //BEN_CORRECTION : Valeur magique. Constante.
    }

    private IEnumerator SpawnZombies()
    {
        yield return new WaitForSeconds(timeBeforeZombieSpawn);
        for (int i = 0; i < numbOfZombies; ++i)
            SpawnZombie(new Vector2(UnityEngine.Random.Range(-CameraInfo.Width / 2, CameraInfo.Width / 2), -CameraInfo.Height / 2));
    }
}
