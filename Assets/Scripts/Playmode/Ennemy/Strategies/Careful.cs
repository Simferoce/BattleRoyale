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
		private const float MIN_VIE_CALCUL_DISTANCE = 0.0f;
		private const float MAX_VIE_CALCUL_DISTANCE = 100.0f;
		private const int HEALTH_LIMIT_TO_BE_COWBOY = 70;
		private const int HEALTH_LIMIT_TO_SEARCH_MEDKIT = 40;
		
		private readonly Mover mover;
		private readonly HandController handController;
		private readonly EnnemySensor enemySensor;
		private readonly PickableSensor pickableSensor;
		private PickableControllerMedKit pickableMedkitTargeted;
		private readonly Health health;

		private Vector2? randomSearch = null;
		private float previousHealth;
		private float safeDistance = 5.0f;
		
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
			if (health.HealthPoints < HEALTH_LIMIT_TO_BE_COWBOY)
			{
				if (health.HealthPoints < HEALTH_LIMIT_TO_SEARCH_MEDKIT)
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
			if (health.HealthPoints <= MAX_VIE_CALCUL_DISTANCE && health.HealthPoints >= MIN_VIE_CALCUL_DISTANCE)
			{
				float differenceDistance = previousHealth - health.HealthPoints;
				safeDistance += (differenceDistance * 0.25f);
				previousHealth = health.HealthPoints;
			}
		}
	}
}
