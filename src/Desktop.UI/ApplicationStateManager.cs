using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Departments
{
	public interface IApplicationStateManager
	{
		void ChangeStateRequest(string newState, NavigationParameters? navigationParameters = null);
	}

	internal class ApplicationStateManager : IApplicationStateManager
	{
		private readonly IRegionManager _regionManager;
		private string CurrentState = "Started";

		private readonly Dictionary<string, string[]> State = new Dictionary<string, string[]>
		{
			["Started"] = new[] { "Main" },
			["Main"] = new[] { "DepartmentDetails" },
			["DepartmentDetails"] = new[] { "Main", "AddEmployee", "EditEmployee", "DepartmentDetails" },
			["AddEmployee"] = new[] { "DepartmentDetails" },
			["EditEmployee"] = new[] { "DepartmentDetails" }
		};

		public ApplicationStateManager(IRegionManager regionManager)
		{
			_regionManager = regionManager;
		}

		public void ChangeStateRequest(string newState, NavigationParameters? navigationParameters = null)
		{
			if (!State[CurrentState].Any(x => x.Contains(newState)))
			{
				throw new Exception("Невозможен переход в новое состояние.");
			}

			CurrentState = newState;

			_regionManager.RequestNavigate("ShellRegion", newState + "View", NavigationResultHandler, navigationParameters);
		}

		private void NavigationResultHandler(NavigationResult navigationResult)
		{
			if (navigationResult.Result.HasValue && navigationResult.Result.Value == false)
			{
				if (navigationResult.Error != null)
				{
					throw navigationResult.Error;
				}

				throw new Exception("При навигации произошла ошибка");
			}
		}
	}
}
