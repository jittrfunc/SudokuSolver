using SudokuSolver.Core.Implementation;
using SudokuSolver.Core.Interfaces;
using SudokuSolver.Service.Implementation;
using SudokuSolver.Service.Interfaces;
using System;

using Unity;

namespace SudokuSolver.Web
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ISudokuSolver, SudokuSolverCore>();
            container.RegisterType<ISudokuSolverService, SudokuSolverService>();
        }
    }
}