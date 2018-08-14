using Core;
using StructureMap;
using System;
using UI.RunnerManagement.ViewModels;

namespace UI.RunnerManagement.Registries
{
    internal class ViewRegistry : Registry
    {
        public ViewRegistry()
        {
            ForConcreteType<RunnersViewModel>()
                .Configure
                .Ctor<Func<IUnitOfWork>>().Is(c => () => c.GetInstance<IUnitOfWork>())
                .AlwaysUnique();
        }
    }
}
