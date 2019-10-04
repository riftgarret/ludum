using System;

namespace Davfalcon.Revelator
{
	[Flags]
	public enum UsableDuringState : short
	{
		OutOfCombat = 0,
		InCombat = 1
	}

	public interface IUsableItem : IItem, IEffectSource
	{
		bool IsConsumable { get; }
		int Remaining { get; set; }
		UsableDuringState UsableDuring { get; }
		void Use();
	}
}
