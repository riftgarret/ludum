using UnityEngine;
using System.Collections;

public class HotKey {
	public ICombatSkill skill {
		get;
		set;
	}

	public HotKey() {}
	public HotKey(ICombatSkill skill) {
		this.skill = skill;
	}

}
