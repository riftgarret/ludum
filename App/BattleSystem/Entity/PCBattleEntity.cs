using UnityEngine;
using System.Collections;

public class PCBattleEntity : BattleEntity {

	// setup variables
	public PCBattleEntity(PCCharacter character, BattleEntity.BattleEntityDelegate listener) : base(character, listener) {		
	}

	public PCCharacter pcCharacter {
		get { return (PCCharacter) character; }
	}

	public PCSkillSet SkillSet {
		get { return pcCharacter.skillSet; }
	}

	public override bool isPC 
	{
		get { return true; }
	}

	public override string ToString ()
	{
		return string.Format ("[PCBattleEntity: pcCharacter={0}, hp={1}/{2}]", pcCharacter, currentHP, maxHP);
	}
}
