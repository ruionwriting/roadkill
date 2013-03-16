﻿using System.Web.Mvc;
using System.Diagnostics;
using Roadkill.Core.Configuration;
using System;
using StructureMap;

namespace Roadkill.Core.Controllers
{
	/// <summary>
	/// A base controller for all Roadkill controller classes which require services 
	/// (via an IServiceContainer) or authentication.
	/// </summary>
	public class ControllerBase : Controller
	{
		public IConfigurationContainer Configuration { get; private set; }
		public UserManager UserManager { get; private set; }
		public IRoadkillContext Context { get; private set; }

		public ControllerBase(IConfigurationContainer configuration, UserManager userManager, IRoadkillContext context)
		{
			Configuration = configuration;
			UserManager = userManager;
			Context = context;
		}

		protected override void OnException(ExceptionContext filterContext)
		{
			Log.Error("Error caught on controller: {0}\n{1}", filterContext.Exception.Message, filterContext.Exception.ToString());
			base.OnException(filterContext);
		}

		/// <summary>
		/// Called before the action method is invoked. This overides the default behaviour by 
		/// populating RoadkillContext.Current.CurrentUser with the current logged in user after
		/// each action method.
		/// </summary>
		/// <param name="filterContext">Information about the current request and action.</param>
		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (!Configuration.ApplicationSettings.Installed)
			{
				if (!(filterContext.Controller is InstallController))
					filterContext.Result = new RedirectResult(this.Url.Action("Index","Install"));

				return;
			}
			else if (Configuration.ApplicationSettings.UpgradeRequired)
			{
				if (!(filterContext.Controller is UpgradeController))
					filterContext.Result = new RedirectResult(this.Url.Action("Index", "Upgrade"));

				return;
			}

			Context.CurrentUser = UserManager.GetLoggedInUserName(HttpContext);
			ViewBag.Context = Context;
			ViewBag.Config = Configuration;

			// This is a fix for versions before 1.5 storing the username instead of a guid in the login cookie
			if (!Configuration.ApplicationSettings.UseWindowsAuthentication)
			{
				Guid userId;
				if (!Guid.TryParse(Context.CurrentUser, out userId))
				{
					UserManager.Logout();
				}
			}
		}
	}
}
