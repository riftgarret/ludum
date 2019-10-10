using System;
using Ninject;
using Redninja.Components.Clock;
using Redninja.Components.Combat;
using Redninja.Data;
using Redninja.Entities;
using Redninja.System;

namespace Redninja
{
	public interface IBattleContext : IDisposable
	{
		IClock Clock { get; }
		ISystemProvider SystemProvider { get; }
		IBattleModel BattleModel { get; }
		IDataManager DataManager { get; }			
		ICombatExecutor CombatExecutor { get; }
		T Get<T>() where T : class;	
	}

	public class BattleContext : IBattleContext
	{
		private IKernel kernel;

		/// TODO: Lets see if  we cant have the executor created from resources
		public BattleContext(IDataManager dataManager, ICombatExecutor executor)
		{
			kernel = new StandardKernel();
			kernel.Bind<IBattleContext>().ToConstant(this);
			kernel.Bind<ICombatExecutor>().ToConstant(executor);
			kernel.Bind<IDataManager>().ToConstant(dataManager);
			kernel.Bind<ISystemProvider, SystemProvider>().To<SystemProvider>().InSingletonScope();
			kernel.Bind<IClock, Clock>().To<Clock>().InSingletonScope();
			kernel.Bind<IBattleEntityManager, IBattleModel, BattleEntityManager>().To<BattleEntityManager>().InSingletonScope();
			kernel.Bind<IBattleEventProcessor>().To<EntityBattleEventProcessor>().InSingletonScope();									
		}

		

		public IClock Clock => Get<Clock>();

		public ISystemProvider SystemProvider => Get<ISystemProvider>();

		public IBattleModel BattleModel => Get<IBattleModel>();

		public IDataManager DataManager => Get<IDataManager>();

		public ICombatExecutor CombatExecutor => Get<ICombatExecutor>();

		// TODO remove as unncessary
		public void Bind<T>(T obj) where T : class => kernel.Bind<T>().ToConstant(obj);

		public void Dispose() => kernel.Dispose();

		public T Get<T>() where T : class => kernel.Get<T>();		
	}
}
