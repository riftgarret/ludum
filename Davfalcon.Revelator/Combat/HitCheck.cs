using System;

namespace Davfalcon.Revelator.Combat
{
	[Serializable]
	public class HitCheck
	{
		public bool IsSet { get; }
		public bool Crit { get; }
		public double HitChance { get; }
		public bool Hit { get; }
		public double CritChance { get; }

		public HitCheck(double hitChance, bool hit, double critChance = 0, bool crit = false)
		{
			HitChance = hitChance;
			Hit = hit;
			CritChance = critChance;
			Crit = crit;
			IsSet = true;
		}

		private HitCheck(bool success)
			: this(0, success)
			=> IsSet = false;

		public static HitCheck Success = new HitCheck(true);
		public static HitCheck Miss = new HitCheck(false);

		public static implicit operator bool(HitCheck hitCheck)
			=> hitCheck.Hit;
	}
}
