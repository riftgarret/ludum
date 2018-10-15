﻿namespace Redninja.Components.Targeting
{
	public interface ISelectedTarget : ITargetResolver
	{
		ITargetingRule Rule { get; }
		IEntityModel Target { get; }
		ITargetPattern Pattern { get; }
		int Team { get; }
		Coordinate Anchor { get; }
	}
}
