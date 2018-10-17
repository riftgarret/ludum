﻿using System;
using Redninja.Components.Actions;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;
using Redninja.View;

namespace Redninja.Components.Decisions.Player
{
	internal class PlayerDecisionManager : IActionDecider
	{		
		private readonly IBattleView view;
		private readonly IDecisionHelper decisionHelper;

		private IUnitModel blockingEntity;
		private IMovementComponent currentMovement;
		private ITargetingComponent currentSkill;

		bool IActionDecider.IsPlayer => true;

		public event Action<IUnitModel, IBattleAction> ActionSelected;
		public event Action<IUnitModel> WaitingForDecision;
		public event Action WaitResolved;

		private bool TargetingActive => currentSkill != null || currentMovement != null;

		public PlayerDecisionManager(IBattleView view, IDecisionHelper decisionHelper)
		{
			this.decisionHelper = decisionHelper;			
			this.view = view;

			view.ActionSelected += OnActionSelected;
			view.MovementInitiated += OnMovementInitiated;
			view.MovementPathUpdated += OnMovementPathUpdated;
			view.MovementConfirmed += OnMovementConfirmed;
			view.SkillSelected += OnSkillSelected;
			view.TargetSelected += OnTargetSelected;
			view.TargetingCanceled += OnTargetingCanceled;
		}

		public void ProcessNextAction(IUnitModel entity, IBattleModel battleModel)
		{
			// blockingEntity acts as a mutex to ensure presenter is only waiting on one unit at a time
			if (blockingEntity != null) throw new InvalidOperationException("Another entity is already blocking game execution.");

			WaitForDecision(entity);
		}

		// Considering removing this so view doesn't generate actions
		private void OnActionSelected(IUnitModel entity, IBattleAction action)
		{
			if (TargetingActive) throw new InvalidOperationException("An action should not be selected while a skill is currently being targeted.");

			ActionSelected?.Invoke(entity, action);
			ResumeIfDecided(entity);
		}

		#region Movement
		private void OnMovementInitiated(IUnitModel entity)
		{
			if (TargetingActive) throw new InvalidOperationException("Cannot initiate movement while another targeting state is already active.");

			currentMovement = decisionHelper.GetMovementComponent(entity);
			view.SetViewMode(currentMovement);
		}

		private void OnMovementPathUpdated(Coordinate point)
		{
			if (currentMovement == null) throw new InvalidOperationException("Movement is not currently active.");

			currentMovement.AddPoint(point);
		}

		private void OnMovementConfirmed()
		{
			if (currentMovement == null) throw new InvalidOperationException("Movement is not currently active.");

			ActionSelected?.Invoke(currentMovement.Entity, currentMovement.GetAction());
			ResumeIfDecided(currentMovement.Entity);
			EndTargeting();
		}
		#endregion

		#region Skill targeting
		private void OnSkillSelected(IUnitModel entity, ISkill skill)
		{
			if (TargetingActive) throw new InvalidOperationException("Cannot initiate skill targeting while another targeting state is already active.");

			currentSkill = decisionHelper.GetTargetingComponent(entity, skill);
			view.SetViewMode(currentSkill);
		}

		private void OnTargetSelected(ISelectedTarget target)
		{
			if (currentSkill == null) throw new InvalidOperationException("Skill targeting is not active.");

			currentSkill.SelectTarget(target);

			if (currentSkill.Ready)
			{
				ActionSelected?.Invoke(currentSkill.Entity, currentSkill.GetAction());

				ResumeIfDecided(currentSkill.Entity);
				EndTargeting();
			}
		}
		#endregion

		private void OnTargetingCanceled()
		{
			if (!TargetingActive) throw new InvalidOperationException("Nothing to cancel.");

			if (!currentSkill.Back())
			{
				EndTargeting();
			}
		}

		private void EndTargeting()
		{
			currentSkill = null;
			view.SetViewModeDefault();
		}

		private void WaitForDecision(IUnitModel entity)
		{
			blockingEntity = entity;
			WaitingForDecision?.Invoke(entity);
			view.OnDecisionNeeded(entity);
		}

		private void ResumeIfDecided(IUnitModel entity)
		{
			// do this check in case we allow selecting another unit's actions while one is blocking
			if (entity == blockingEntity)
			{
				blockingEntity = null;
				WaitResolved?.Invoke();
			}
		}
	}
}
