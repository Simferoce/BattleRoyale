namespace Playmode.Event
{
    public class EventHandlerEnemyDeath : EventChannel<EnemyDeathData>
    {

    }

    public struct EnemyDeathData
    {
        public string name;
        public string killerName;

        public EnemyDeathData(string name, string killerName)
        {
            this.name = name;
            this.killerName = killerName;
        }
    }

}

