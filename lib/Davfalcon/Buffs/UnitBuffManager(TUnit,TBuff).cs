using System.Linq;

namespace Davfalcon.Buffs
{
	public class UnitBuffManager<TUnit, TBuff> : Modifier<TUnit>,
		IUnitBuffManager<TUnit, TBuff>,
		IUnitComponent<TUnit>
		where TUnit : class, IUnitTemplate<TUnit>
		where TBuff : class, IBuff<TUnit>
	{
		private readonly ModifierStack<TUnit> stack = new ModifierStack<TUnit>();

		public TBuff[] GetAllBuffs() => stack.Cast<TBuff>().ToArray();

		public void Add(TBuff buff) => stack.Add(buff);

		public void Remove(TBuff buff) => stack.Remove(buff);

		public void RemoveAt(int index) => stack.RemoveAt(index);

		public virtual void Initialize(TUnit unit) => unit.Modifiers.Add(this);

		public override TUnit AsModified() => stack.LastOrDefault()?.AsModified() ?? Target;

		public override void Bind(TUnit target)
		{
			base.Bind(target);
			stack.Bind(Target);
		}
	}
}
