using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Playmode.Entity.Senses
{
    public delegate void WallSensorEventHandler(Environement.WallController wallControl);
    
    public class WallSensor : MonoBehaviour
    {
        private ICollection<Environement.WallController> wallsInSight;

        public event WallSensorEventHandler OnWallSeen;
        public event WallSensorEventHandler OnWallSightLost;

        public IEnumerable<Environement.WallController> WallsInSight => wallsInSight;

        private void Awake()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            wallsInSight = new HashSet<Environement.WallController>();
        }

        public void See(Environement.WallController wall)
        {
            wallsInSight.Add(wall);

            NotifyWallSeen(wall);
        }

        public void LooseSightOf(Environement.WallController wall)
        {
            wallsInSight.Remove(wall);

            NotifyWallSightLost(wall);
        }

        private void NotifyWallSeen(Environement.WallController wall)
        {
            OnWallSeen?.Invoke(wall);
        }

        private void NotifyWallSightLost(Environement.WallController wall)
        {
            OnWallSightLost?.Invoke(wall);
        }
    }
}

