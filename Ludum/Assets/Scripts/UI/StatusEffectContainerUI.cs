using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HorizontalLayoutGroup))]
public class StatusEffectContainerUI : MonoBehaviour
{
	private HorizontalLayoutGroup layoutContainer;

    void Awake()
	{
		// TODO should this add a layout group or just grab the current one?
		layoutContainer = GetComponent<HorizontalLayoutGroup>();
	}

	// TODO add function to add/remvoe or sync status effects
}
