//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;

public class SkillCombatNode : ConfigurableCombatNode
{

	/// <summary>
	/// Creates a new instance that just keeps track of some of the meta data.
	/// The skill itself should generate its own statistics because it can vary from
	/// skill level which isnt put into place.
	/// </summary>
	/// <param name="skillOrigin">Skill origin.</param>
	public SkillCombatNode(CombatRound round) : base() {
		Load (round.combatProperties);
	}
}

