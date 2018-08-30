using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Playmode.Ennemy;
namespace Playmode.Event
{
    public class EventHandlerEnemyDeath : EventChannel<EnemyDeathData>
    {

    }

    public struct EnemyDeathData
    {
        public string name;

        public EnemyDeathData(string name)
        {
            this.name = name;
        }
    }

}

