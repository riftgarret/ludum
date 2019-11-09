using System;
using System.Linq;
using Ninject;
using Redninja.Components.Clock;
using Redninja.Components.Combat;
using Redninja.Components.Combat.Events;
using Redninja.Data;
using Redninja.Entities;
using Redninja.System;
using Redninja.View;

namespace Redninja.Presenter
{
	/// <summary>
	/// Main logic that flows through this scene is handled by this presenter in a MVP relationship.
	/// Where this is the presenter, other components generated will represent the views.  
	/// </summary>
	public class BattlePresenter : IBattlePresenter
	{
		private readonly IBattleContext context;		
		private readonly DataManager dataManager;
		private readonly Clock clock;
		private IBattleView view;
		private readonly ICombatExecutor combatExecutor;
		private readonly IBattleEntityManager entityManager;				
		private readonly PriorityProcessingQueue<float, IBattleOperation> battleOpQueue;
		private readonly SystemProvider systemProvider;

		public event Action<ICombatEvent> BattleEventOccurred;

		/// <summary>
		/// Gets the presenter's state.
		/// </summary>
		public GameState State { get; private set; } = GameState.Intro;

		/// <summary>
		/// Check if time should be active. This can be false due to the game state being
		/// over or that the user needs to select a skill.
		/// </summary>
		public bool TimeActive => State == GameState.Active && !RequiresInput;		

		public BattlePresenter(IBattleContext context, IBattleView view)
		{
			this.context = context;
			this.view = view;			

			dataManager = context.Get<DataManager>();
			combatExecutor = context.Get<ICombatExecutor>();
			entityManager = context.Get<IBattleEntityManager>();			
			systemProvider = context.Get<SystemProvider>();			
			clock = context.Get<Clock>();
			battleOpQueue = context.Get<OperationManager>();
					
			entityManager.InitializeBattlePhase();

			BindEvents();
		}

		private void BindEvents()
		{
			combatExecutor.BattleEventOccurred += e => BattleEventOccurred?.Invoke(e);			
			combatExecutor.BattleEventOccurred += view.OnBattleEventOccurred;
		}

		#region Control	
		public void Play()
		{
			State = GameState.Active;
		}

		public void Pause()
		{
			State = GameState.Paused;
		}

		private bool RequiresInput => entityManager.Entities.Any(x => x.Actions.RequiresAction);

		/// <summary>
		/// Update game clock. This drives the presenter.
		/// </summary>
		public void IncrementGameClock(float timeDelta)
		{
			if (TimeActive)
			{
				// This will cause actions to trigger
				clock.IncrementTime(timeDelta);
			}

			// The queue contains operations that already triggered, so we always want to process this
			battleOpQueue.Process();
		}

		public void Dispose()
		{
			entityManager.Dispose();			
		}
		#endregion
	}
}
