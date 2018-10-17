﻿using System;
using System.Reflection;
using Redninja.Data;

namespace Redninja.ConsoleDriver.Objects
{
	public class ObjectLoader<T> : IDataLoader
	{
		private readonly Type type;

		public ObjectLoader(Type fromType)
			=> type = fromType;

		public void Load(IEditableDataManager manager)
		{
			foreach (PropertyInfo p in type.GetProperties())
			{
				if (p.PropertyType == typeof(T))
				{
					manager.GetDataStore<T>()[p.Name] = (T)p.GetValue(null);
				}
			}
		}
	}
}