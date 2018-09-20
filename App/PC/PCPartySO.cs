using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public class PCPartySO : ScriptableObject {
	
	public PCCharacterSO[] characterConfigs;

	void OnEnabled() {

	}

	public PCCharacter [] CreateUniqueCharacters() {
		PCCharacter[] retValue = new PCCharacter[characterConfigs.Length];
		
		Dictionary<string, int> nameCountMap = new Dictionary<string, int>();
		Dictionary<string, char> namePrefixMap = new Dictionary<string, char>();
		// generate unique enemy characters and populate our duplicate name maps
		for(int i=0; i < characterConfigs.Length; i++) {
			// first copy over the NPC into the same spot so they are unique
			retValue[i] = (PCCharacter) Character.CreateFromConfig(characterConfigs[i]);
			
			Character enemy = retValue[i];
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
			Character enemy = retValue[i];
			if(namePrefixMap.ContainsKey(enemy.displayName)) {
				enemy.displayName = enemy.displayName + " " + namePrefixMap[enemy.displayName];
				namePrefixMap[enemy.displayName] = namePrefixMap[enemy.displayName]++;
			}
		}
		
		return retValue;
	}
}
