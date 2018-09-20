using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class EnemyPartySO : SanitySO {
	
	public EnemyCharacterSO[] characterConfigs;

	void OnEnabled() {

	}

	public EnemyCharacter [] CreateUniqueCharacters() {
		EnemyCharacter[] retValue = new EnemyCharacter[characterConfigs.Length];

		Dictionary<string, int> nameCountMap = new Dictionary<string, int>();
		Dictionary<string, char> namePrefixMap = new Dictionary<string, char>();
		// generate unique enemy characters and populate our duplicate name maps
		for(int i=0; i < characterConfigs.Length; i++) {
			// first copy over the NPC into the same spot so they are unique
			retValue[i] = (EnemyCharacter) Character.CreateFromConfig(characterConfigs[i]);

			EnemyCharacter enemy = retValue[i];
			// store count of name appearance
			// if we've seen it more than once, increment the count and prefix map
			if(nameCountMap.ContainsKey(enemy.displayName)) {
				nameCountMap[enemy.displayName] = nameCountMap[enemy.displayName] + 1;
				namePrefixMap[enemy.displayName] = 'A';	// set the first letter of multiple enmies to be 'A'
			}
			else {
				nameCountMap[enemy.displayName] = 1;
			}
		}

		// clean up any duplicate names
		for(int i=0; i < retValue.Length; i++) {
			EnemyCharacter enemy = retValue[i];
			string origName = enemy.displayName;
			if(namePrefixMap.ContainsKey(origName)) {
				enemy.displayName = origName + " " + namePrefixMap[origName];
				namePrefixMap[origName] = namePrefixMap[origName]++;
			}
		}

		return retValue;
	}

	protected override void SanityCheck ()
	{
		if(characterConfigs == null || characterConfigs.Length == 0) {
			LogNull("characterConfigs");
		}
	}
}
