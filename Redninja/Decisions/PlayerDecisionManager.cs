using System;
using Redninja.Skills;
using Redninja.Targeting;

namespace Redninja.Decisions
{
	public class PlayerDecisionManager : IActionDecider, IBattleViewCallbacks
	{
		private readonly IBattleEntityManager entityManager;
		private IBattleEntity blockingEntity;
		private ISkillTargetingManager currentSkill;

		bool IActionDecider.IsPlayer => true;

		public event Action<IBattleEntity, IBattleAction> ActionSelected;
		public event Action<ISkillTargetingInfo> TargetingSkill;
		public event Action TargetingEnded;
		public event Action<IBattleEntity> WaitingForDecision;
		public event Action WaitResolved;

		public PlayerDecisionManager(IBattleEntityManager entityManager)
		{
			this.entityManager = entityManager;
		}

		public void ProcessNextAction(IBattleEntity entity, IBattleEntityManager entityManager)
		{
			if (blockingEntity != null) throw new InvalidOperationException("Another entity is already blocking game execution.");

			blockingEntity = entity;
			WaitingForDecision?.Invoke(entity);
		}

		public void OnActionSelected(IBattleEntity entity, IBattleAction action)
		{
			if (currentSkill != null) throw new InvalidOperationException("An action should not be selected while a skill is currently being targeted.");

			ActionSelected?.Invoke(entity, action);

			ResumeIfDecided(entity);
		}

		public void OnSkillSelected(IBattleEntity entity, ICombatSkill skill)
		{
			if (currentSkill != null) throw new InvalidOperationException("Targeting should be canceled before another skill can be selected.");

			currentSkill = DecisionHelper.GetTargetingManager(entity, entityManager, skill);
			TargetingSkill?.Invoke(currentSkill);
		}

		public void OnTargetSelected(ISelectedTarget target)
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

		public void OnTargetingCanceled()
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
			TargetingEnded?.Invoke();
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
