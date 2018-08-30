﻿using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Playmode.Entity.Senses;
using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Senses;
using Playmode.Entity.Status;
using Playmode.Movement;
using Playmode.Pickable;
using Playmode.Util.Values;
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
					TargetMethod.SearchEnemyOrPickable(mover, sensibilityProximity,ref randomSearch);
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
						TargetMethod.SearchEnemyOrPickable(mover, sensibilityProximity, ref randomSearch);
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
