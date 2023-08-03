using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Registration.Lifestyle;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using FX.Core;
using FX.Data;
using log4net;
using NHibernate.Cfg;
using System;
using System.Runtime.CompilerServices;

namespace Parse.Forms
{
	public class Bootstrapper
	{
		private readonly static ILog log;

		private static IWindsorContainer container;

		static Bootstrapper()
		{
			Bootstrapper.log = LogManager.GetLogger(typeof(Bootstrapper));
		}

		public Bootstrapper()
		{
		}

		public static void InitializeContainer()
		{
			try
			{
				Bootstrapper.container = new WindsorContainer(new XmlInterpreter());
				IoC.Initialize(Bootstrapper.container);
				string str = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "dbSqlLite.db");
				Bootstrapper.container.Register(new IRegistration[] { Component.For<IFXContext>().ImplementedBy<FXContext>().Named("FX.context").LifeStyle.Transient });
				NHibernateSessionManager.Instance.SetConnectionString = (Configuration config) => config.SetProperty("connection.connection_string", string.Format("Data Source={0};Version=3;New=True;", str));
			}
			catch (Exception exception)
			{
				Bootstrapper.log.Error("Error initializing application.", exception);
				throw;
			}
		}
	}
}