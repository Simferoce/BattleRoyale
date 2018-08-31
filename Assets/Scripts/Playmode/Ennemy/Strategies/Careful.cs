using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Senses;
using Playmode.Entity.Status.Health;
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
		private PickableControllerMedKit pickableMedkitTargeted;
		private readonly Health health;

		private Vector2? randomSearch = null;
		private float previousHealth;
		private float safeDistance = 5.0f;
		private float minVieCalculDistance = 0.0f;
		private float maxVieCalculdistance = 100.0f;
		private Cowboy cowboy;

		public Careful(Mover mover, HandController handController, EnnemySensor enemySensor, PickableSensor pickableSensor, Health health)
		{
			this.mover = mover;
			this.handController = handController;
			this.health = health;
            previousHealth = health.HealthPoints;
            cowboy = new Cowboy(mover, handController, enemySensor, pickableSensor);

			this.enemySensor = enemySensor;
			this.pickableSensor = pickableSensor;
		}

		public void Act()
		{
			ConfigureDistance();
			if (health.HealthPoints < 70)
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
						TargetMethod.SearchEnemyOrPickable(mover, ref randomSearch);
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
							TargetMethod.SearchEnemyOrPickable(mover, ref randomSearch);
						}
					}
				}
			}
			else
			{
				cowboy.Act();
			}
		}

		private void OnPickUp(PickableController controller)
		{
			pickableMedkitTargeted.OnPickUp -= OnPickUp;
			pickableMedkitTargeted = null;
		}

		private void ConfigureDistance()
		{
			if (health.HealthPoints <= maxVieCalculdistance && health.HealthPoints >= minVieCalculDistance)
			{
				float differenceDistance = previousHealth - health.HealthPoints;
				safeDistance += (differenceDistance * 0.25f);
				previousHealth = health.HealthPoints;
			}
		}
	}
}
