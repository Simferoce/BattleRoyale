using Playmode.Ennemy.BodyParts;
using Playmode.Movement;

namespace Playmode.Ennemy.Strategies
{
    //BEN_CORRECTION : Code mort. Devrait être supprimé.
    
    public class TurnAndShootStragegy : IEnnemyStrategy
    {
        private readonly Mover mover;
        private readonly HandController handController;

        public TurnAndShootStragegy(Mover mover, HandController handController)
        {
            this.mover = mover;
            this.handController = handController;
        }

        public void Act()
        {
            mover.Rotate(Mover.CLOCKWISE);

            handController.Use();
        }
    }
}