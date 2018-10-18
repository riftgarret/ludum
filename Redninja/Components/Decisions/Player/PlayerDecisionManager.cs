using System;
using System.Collections;
using Redninja.Components.Actions;
using Redninja.Components.Skills;

namespace Redninja.Components.Decisions.Player
{
	internal class PlayerDecisionManager : IActionDecider
	{
		private readonly Stack contextStack = new Stack();
		private readonly IDecisionHelper decisionHelper;

		private IUnitModel blockingEntity;
		public IActionsContext ActionsContext => contextStack.Peek() as IActionsContext;
		public IMovementContext MovementContext => contextStack.Peek() as IMovementContext;
		public ITargetingContext TargetingContext => contextStack.Peek() as ITargetingContext;

		public event Action<IUnitModel, IBattleAction> ActionSelected;
		public event Action<IUnitModel> WaitingForDecision;
		public event Action WaitResolved;

		private T GetComponent<T>() where T : class => contextStack.Peek() as T;

		public PlayerDecisionManager(IDecisionHelper decisionHelper)
		{
			this.decisionHelper = decisionHelper;
		}

		private InvalidOperationException StateException()
			=> new InvalidOperationException($"Invalid action for the current context of {contextStack.Peek().GetType()},");

		public void ProcessNextAction(IUnitModel entity, IBattleModel battleModel)
		{
			// blockingEntity acts as a mutex to ensure presenter is only waiting on one unit at a time
			if (blockingEntity != null) throw new InvalidOperationException("Another entity is already blocking game execution.");

			WaitForDecision(entity);
		}

		public void SetEntityContext(IUnitModel entity)
		{
			if (contextStack.Count > 0) throw StateException();

			contextStack.Push(decisionHelper.GetActionsContext(entity));
		}

		public void SetMovementContext(IUnitModel entity)
		{
			if (contextStack.Count > 1) throw StateException();

			contextStack.Push(decisionHelper.GetMovementContext(entity));
		}

		public void SetTargetingContext(IUnitModel entity, ISkill skill)
		{
			if (contextStack.Count > 1) throw StateException();

			contextStack.Push(decisionHelper.GetTargetingContext(entity, skill));
		}

		public void Resolve(IUnitModel entity, IBattleAction action)
		{
			ActionSelected?.Invoke(entity, action);
			ResumeIfDecided(entity);
			contextStack.Clear();
		}

		public void Resolve()
		{
			if (!(contextStack.Peek() is IActionProvider)) throw StateException();

			IActionProvider actionProvider = contextStack.Pop() as IActionProvider;
			Resolve(actionProvider.Entity, actionProvider.GetAction());
		}

		public void ExitContext()
		{
			if (contextStack.Count == 0) throw new InvalidOperationException("No context to exit.");

			contextStack.Pop();
		}

		private void WaitForDecision(IUnitModel entity)
		{
			blockingEntity = entity;
			WaitingForDecision?.Invoke(entity);
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
