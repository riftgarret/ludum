using UnityEngine;
using System.Collections;

public class FightTrigger : MonoBehaviour {

	public EnemyPartySO enemyParty;
	public PCPartySO party;

	void OnTriggerEnter(Collider other) {
		// if we enter this colider, lets start the battle

		if(enemyParty != null) {
			SceneLoader.LoadScene(enemyParty, party);
		}
	}


}
