using System.Collections.Generic;
using Playmode.Ennemy;
using UnityEngine;

namespace Playmode.Entity.Senses
{
    public delegate void EnnemySensorEventHandler(EnnemyController ennemy);

    public class EnnemySensor : MonoBehaviour
    {
        private ICollection<EnnemyController> ennemiesInSight;

        public event EnnemySensorEventHandler OnEnnemySeen;
        public event EnnemySensorEventHandler OnEnnemySightLost;

        public IEnumerable<EnnemyController> EnnemiesInSight => ennemiesInSight;

        private void Awake()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            ennemiesInSight = new List<EnnemyController>();
        }

        public void See(EnnemyController ennemy)
        {
            ennemiesInSight.Add(ennemy);
            ennemy.OnDeathEnemy += Ennemy_OnDeathEnemy;

            NotifyEnnemySeen(ennemy);
        }

        private void Ennemy_OnDeathEnemy(EnnemyController ennemyController)
        {
            ennemiesInSight.Remove(ennemyController);
        }

        public void LooseSightOf(EnnemyController ennemy)
        {
            ennemiesInSight.Remove(ennemy);
            ennemy.OnDeathEnemy -= Ennemy_OnDeathEnemy;

            NotifyEnnemySightLost(ennemy);
        }

        private void NotifyEnnemySeen(EnnemyController ennemy)
        {
            OnEnnemySeen?.Invoke(ennemy);
        }

        private void NotifyEnnemySightLost(EnnemyController ennemy)
        {
            OnEnnemySightLost?.Invoke(ennemy);
        }
    }
}