using System;
using System.Collections;
using System.Collections.Generic;
using Redninja;
using Redninja.Components.Combat.Events;
using Redninja.Components.Decisions;
using Redninja.Presenter;
using Redninja.View;
using UnityEngine;

public class BattleView : MonoBehaviour, IBattleView
{

	[SerializeField] private CharacterOverlayContainer characterContainer = default;
	[SerializeField] private CharacterOverlayContainer enemyContainer = default;

	private IBattleContext context;
	private IBattlePresenter presenter;

	public void OnBattleEventOccurred(ICombatEvent battleEvent)
	{
		Debug.Log(battleEvent);
	}

	public void Initialize(IBattleContext context)
	{
		this.context = context;
		this.presenter = new BattlePresenter(context, this);

		foreach (var unit in context.BattleModel.Entities)
		{
			var targetContainer = (unit.Team == 1) ? enemyContainer : characterContainer;
			targetContainer.AddCharacter(unit);
		}

		presenter.Play();
	}

	void Update()
	{
		if (presenter == null) return;
		presenter.IncrementGameClock(Time.deltaTime);
	}
}
