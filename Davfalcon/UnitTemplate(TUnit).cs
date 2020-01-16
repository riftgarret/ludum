using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Davfalcon.Stats;

namespace Davfalcon
{
	/// <summary>
	/// Implements basic unit functionality.
	/// </summary>
	/// <typeparam name="TUnit">The interface used by the unit's implementation.</typeparam>
	[Serializable]
	public abstract class UnitTemplate<TUnit> : StatsProviderTemplate, IUnitTemplate<TUnit> where TUnit : class, IUnitTemplate<TUnit>
	{									
		private readonly Dictionary<Enum, IUnitComponent<TUnit>> components = new Dictionary<Enum, IUnitComponent<TUnit>>();

		public string Name { get; set; }
		
		protected abstract TUnit Self { get; }
		

		public void AddComponent(Enum id, IUnitComponent<TUnit> component)
		{
			components.Add(id, component);
			component.Initialize(Self);
		}

		public TComponent GetComponent<TComponent>(Enum id) where TComponent : class, IUnitComponent<TUnit>
			=> components[id] as TComponent;		
	}
}
