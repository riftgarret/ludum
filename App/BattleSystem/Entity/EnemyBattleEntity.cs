using UnityEngine;
using System.Collections;

public class EnemyBattleEntity : BattleEntity {
		
	// setup variables
	public EnemyBattleEntity(EnemyCharacter character, BattleEntityDelegate listener) : base(character, listener) {
	
	}	
	
	public EnemyCharacter enemyCharacter {
		get { return (EnemyCharacter) character; }
	}
	
	public override bool isPC 
	{
		get {
			return false;
		}
	}

	public override string ToString ()
	{
		return string.Format ("[EnemyBattleEntity: enemyCharacter={0}, hp={1}/{2}]", enemyCharacter, currentHP, maxHP);
	}
	
}
