using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleCharacterOverlayUI : MonoBehaviour
{
	[SerializeField] private BarUI hpBar;
	[SerializeField] private Text hpText;
	[SerializeField] private BarUI resBar;
	[SerializeField] private Text resText;
	[SerializeField] private Text nameText;
	[SerializeField] private BarUI actionBar;
	[SerializeField] private RawImage actionIcon;
	[SerializeField] private StatusEffectContainerUI statusEffectContainer;

	private Image hpImage;

    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
