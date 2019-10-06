using UnityEngine;
using UnityEngine.UI;

public class BarUI : MonoBehaviour
{
	[SerializeField]
	private Image foregroundBar;

	public float PercentFill => foregroundBar.fillAmount;
}
