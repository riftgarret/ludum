using System;
using Redninja.Skills;
using Redninja.Targeting;

namespace Redninja.Decisions
{
	public class PlayerDecisionManager : IActionDecider
	{
		private readonly IBattleEntityManager entityManager;
		private readonly IBattleView view;

		private IBattleEntity blockingEntity;
		private ISkillTargetingManager currentSkill;

		bool IActionDecider.IsPlayer => true;

		public event Action<IBattleEntity, IBattleAction> ActionSelected;
		public event Action<IBattleEntity> WaitingForDecision;
		public event Action WaitResolved;

		public PlayerDecisionManager(IBattleEntityManager entityManager, IBattleView view)
		{
			this.entityManager = entityManager;
			this.view = view;

			view.ActionSelected += OnActionSelected;
			view.SkillSelected += OnSkillSelected;
			view.TargetSelected += OnTargetSelected;
			view.TargetingCanceled += OnTargetingCanceled;
		}

		public void ProcessNextAction(IBattleEntity entity, IBattleEntityManager entityManager)
		{
			// blockingEntity acts as a mutex to ensure presenter is only waiting on one unit at a time
			if (blockingEntity != null) throw new InvalidOperationException("Another entity is already blocking game execution.");

			WaitForDecision(entity);
		}

		private void OnActionSelected(IBattleEntity entity, IBattleAction action)
		{
			if (currentSkill != null) throw new InvalidOperationException("An action should not be selected while a skill is currently being targeted.");

			ActionSelected?.Invoke(entity, action);

			ResumeIfDecided(entity);
		}

		private void OnSkillSelected(IBattleEntity entity, ISkill skill)
		{
			if (currentSkill != null) throw new InvalidOperationException("Targeting should be canceled before another skill can be selected.");

			currentSkill = DecisionHelper.GetTargetingManager(entity, entityManager, skill);
			view.SetViewModeTargeting(currentSkill);
		}

		private void OnTargetSelected(ISelectedTarget target)
		{
			if (currentSkill == null) throw new InvalidOperationException("A target should not be selected while a skill has not been selected.");

			currentSkill.SelectTarget(target);

			if (currentSkill.Ready)
			{
				IBattleAction battleAction = currentSkill.GetAction();
				ActionSelected?.Invoke(currentSkill.Entity, battleAction);

				ResumeIfDecided(currentSkill.Entity);
				EndTargeting();
			}
		}

		private void OnTargetingCanceled()
		{
			if (currentSkill == null) throw new InvalidOperationException("Targeting should not be canceled while a skill has not been selected.");

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

		private void WaitForDecision(IBattleEntity entity)
		{
			blockingEntity = entity;
			WaitingForDecision?.Invoke(entity);
			view.OnDecisionNeeded(entity);
		}

		private void ResumeIfDecided(IBattleEntity entity)
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
