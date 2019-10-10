using UnityEngine;
using UnityEngine.UI;

public class BarUI : MonoBehaviour
{
	[SerializeField]
	private Image foregroundBar = default;

	public float PercentFill
	{
		get => foregroundBar.fillAmount;
		set => foregroundBar.fillAmount = value;
	}
}
