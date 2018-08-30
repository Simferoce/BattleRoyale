using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Senses;
using Playmode.Entity.Status;
using Playmode.Movement;
using Playmode.Pickable;
using UnityEngine;

namespace Playmode.Ennemy.Strategies
{
	class Careful : IEnnemyStrategy
	{
		private readonly Mover mover;
		private readonly HandController handController;
		private readonly EnnemySensor enemySensor;
		private readonly PickableSensor pickableSensor;
		private PickableControllerWeapon pickableWeapon;
		private PickableControllerMedKit pickableMedkit;
		private readonly Health health;
		private Vector2? randomSearch = null;
		private float sensibilityProximity = 0.5f;
		private float safeDistance = 5.0f;


		public Careful(Mover mover, HandController handController, EnnemySensor enemySensor, PickableSensor pickableSensor, Health health)
		{
			this.mover = mover;
			this.handController = handController;
			this.health = health;

			this.enemySensor = enemySensor;
			this.pickableSensor = pickableSensor;
		}

		public void Act()
		{
			if (health.HealthPoints < 40)
			{
				PickableControllerMedKit medkit = TargetMethod.TargetMedkit(pickableSensor);

				if (medkit != null)
				{
					mover.MoveToward(medkit.transform.position);
				}
				else
				{
					if (randomSearch == null)
						randomSearch = TargetMethod.Search();
					else if ((randomSearch - mover.transform.position).Value.magnitude < sensibilityProximity)
						randomSearch = TargetMethod.Search();
					mover.MoveToward((Vector2) randomSearch);
				}
			}
			else
			{
				PickableControllerWeapon weapon = TargetMethod.TargetWeapon(pickableSensor);
				if (weapon != null)
				{
					mover.MoveToward(weapon.transform.position);
				}
				else
				{
					EnnemyController enemy = TargetMethod.TargetEnemy(enemySensor);
					if (enemy != null)
					{
						mover.KeepDistance(enemy.transform.position, safeDistance);
						handController.Use();
					}
					else
					{
						if (randomSearch == null)
							randomSearch = TargetMethod.Search();
						else if ((randomSearch - mover.transform.position).Value.magnitude < sensibilityProximity)
							randomSearch = TargetMethod.Search();
						mover.MoveToward((Vector2) randomSearch);
					}
				}
			}
		}

		private void OnPickUp(PickableController controller)
		{
			pickableMedkit.OnPickUp -= OnPickUp;
			pickableMedkit = null;
		}
	}
}
