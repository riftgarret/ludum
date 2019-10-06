using System;
using System.Collections;
using System.Collections.Generic;
using Redninja;
using Redninja.Components.Combat.Events;
using Redninja.Components.Decisions;
using Redninja.View;
using UnityEngine;

public class BattleView : MonoBehaviour, IBattleView
{
	private IBattleModel model;
	private IUnitModel waiting;

	private Action checkActionNeeded;
	private Action drawUnitActions;
	private Action drawTargeting;

	public void OnBattleEventOccurred(ICombatEvent battleEvent)
	{
		throw new System.NotImplementedException();
	}

	public void OnDecisionNeeded(IUnitModel entity)
	{
		throw new System.NotImplementedException();
	}

	public void Resume()
	{
		throw new System.NotImplementedException();
	}

	public void SetBattleModel(IBattleModel model)
	{
		this.model = model;
	}

	public void SetViewMode(IBaseCallbacks callbacks)
	{
		throw new System.NotImplementedException();
	}

	public void SetViewMode(IActionsView actionsContext, ISkillsCallbacks callbacks)
	{
		throw new System.NotImplementedException();
	}

	public void SetViewMode(IMovementView movementContext, IMovementCallbacks callbacks)
	{
		throw new System.NotImplementedException();
	}

	public void SetViewMode(ITargetingView targetingContext, ITargetingCallbacks callbacks)
	{
		throw new System.NotImplementedException();
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
