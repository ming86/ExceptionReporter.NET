using System.Reflection;
using ExceptionReporting.Core;
using NUnit.Framework;

namespace ExceptionReporting.Tests
{
	[TestFixture]
	public class ViewFactory_Tests
	{
		private readonly Assembly _winformsAssembly;
		private readonly ViewResolver _winformsViewResolver;
		private readonly Assembly _wpfAssembly;
		private readonly ViewResolver _wpfViewResolver;

		public ViewFactory_Tests()
		{
			_winformsAssembly = Assembly.Load(new AssemblyName("ExceptionReporter.WinForms"));
			_winformsViewResolver = new ViewResolver(_winformsAssembly);

			_wpfAssembly = Assembly.Load(new AssemblyName("ExceptionReporter.Wpf"));
			_wpfViewResolver = new ViewResolver(_wpfAssembly);
		}

		[Test]
		public void CanResolve_WinForms_IInternalExceptionView_Interface()
		{
			var viewType = _winformsViewResolver.Resolve<IInternalExceptionView>();
			Assert.That(viewType.ToString(), Text.StartsWith("ExceptionReporting.WinForms.Views.InternalExceptionView"));
			Assert.That(viewType.Assembly.FullName, Text.StartsWith("ExceptionReporter.WinForms"));
		}

		[Test]
		public void CanCreate_WinForms_InternalExceptionView()
		{
			var view = ViewFactory.Create<IInternalExceptionView>(_winformsViewResolver);
			Assert.That(view.ToString(), Text.StartsWith("ExceptionReporting.WinForms.Views.InternalExceptionView"));
		}

		[Test]
		public void CanResolve_WinForms_IExceptionReportView_Interface()
		{
			var viewType = _winformsViewResolver.Resolve<IExceptionReportView>();

			Assert.That(viewType.ToString(), Text.StartsWith("ExceptionReporting.WinForms.Views.ExceptionReportView"));
			Assert.That(viewType.Assembly.FullName, Text.StartsWith("ExceptionReporter.WinForms"));
		}

		[Test]
		[Ignore("Looks like the IE WebControl thingy on the form, prevents us from instantiating the class here")]
		public void CanCreate_WinForms_ExceptionReportView()
		{
			var view = ViewFactory.Create<IExceptionReportView>(_winformsViewResolver, new ExceptionReportInfo());

			Assert.That(view.ToString(), Text.StartsWith("ExceptionReporting.WinForms.Views.ExceptionReportView"));
		}

		[Test]
		public void CanResolve_Wpf_IInternalExceptionView_Interface()
		{
			var viewType = _wpfViewResolver.Resolve<IInternalExceptionView>();

			Assert.That(viewType.ToString(), Is.EqualTo("ExceptionReporting.Wpf.Views.InternalExceptionView"));
			Assert.That(viewType.Assembly.FullName, Text.StartsWith("ExceptionReporter.Wpf"));
		}

		[Test]
		public void CanResolve_Wpf_IExceptionReportView_Interface()
		{
			var viewType = _wpfViewResolver.Resolve<IExceptionReportView>();

			Assert.That(viewType.ToString(), Text.StartsWith("ExceptionReporting.Wpf.Views.ExceptionReportView"));
			Assert.That(viewType.Assembly.FullName, Text.StartsWith("ExceptionReporter.Wpf"));
		}
	}
}