namespace Playmode.Event
{
    //BEN_REVIEW : Êtes-vous bien certain d'avoir compris ce qu'est un EventChannel ?
    //             Je dis ça, car le nom de votre EventChannel est plutôt un nom que l'on donenrait
    //             au delegate.
    //
    //             J'aurait proposé EnemyDeathEventChannel.
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

