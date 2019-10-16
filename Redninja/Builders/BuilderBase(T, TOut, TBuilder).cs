using System;

namespace Redninja
{
	/// <summary>
	/// Base class for implementing extendable builders.
	/// </summary>
	/// <typeparam name="T">The type of the object to be constructed.</typeparam>
	/// <typeparam name="TOut">The type that the object will be returned as when calling <see cref="Build"/>.</typeparam>
	/// <typeparam name="TBuilder">The type of the implemented builder.</typeparam>
	public abstract class BuilderBase<T, TOut, TBuilder> : IBuilder<TOut>
		where T : TOut
		where TBuilder : BuilderBase<T, TOut, TBuilder>
	{
		public delegate IBuilder<TOut> Func(TBuilder builder);

		private T build;

		protected TBuilder Builder => (TBuilder)this;
		
		protected TBuilder Reset(T build)
		{
			this.build = build;
			return Builder;
		}

		protected TBuilder Self(Action<T> func)
		{
			func(build);
			return Builder;
		}

		/// <summary>
		/// Resets the builder to its initial state.
		/// </summary>
		/// <returns></returns>
		public abstract TBuilder Reset();

		/// <summary>
		/// Gets the built object.
		/// </summary>
		/// <returns>The object built with the set parameters.</returns>
		public virtual TOut Build()
			=> build;
	}
}
