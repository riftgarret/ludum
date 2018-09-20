using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// This is mostly a test script to setup parties, will not be used in game
/// </summary>
[Serializable]
public class PCSkillSetSO : SanitySO {

	[SerializeField]
	private CombatSkillSO [] skills = null;

	/// <summary>
	/// populate the skills
	/// </summary>
	/// <param name="skillset">Skillset.</param>
	public void InitSkills(PCSkillSet skillset) {
		skillset.skills = new ICombatSkill[skills.Length];
		skillset.hotKeys = new HotKey[skills.Length];

		for(int i=0; i < skills.Length; i++) {
			skillset.skills = skills;
			skillset.hotKeys[i] = new HotKey();
			skillset.hotKeys[i].skill = skillset.skills[i];
		}
	}

	protected override void SanityCheck ()
	{
		if(skills == null) {
			LogNull("skills");
		}
	}
}
