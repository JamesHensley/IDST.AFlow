using IDST.AFlow.Browser.UI.Forms;
using IDST.AFlow.Browser.UI.Mapper;
using IDST.AFlow.Browser.UI.Workflow;
using IDST.AFlow.Browser.UI.Workflow.Models;
using IDST.AFlow.Browser.UI.Workflow.Steps;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Models.LifeCycleEvents;

namespace IDST.AFlow.Browser.UI.Services
{
    public static class ServiceManager
    {
        public delegate void WorkflowLifeCycleEventDelegate(LifeCycleEvent e);
        public static event WorkflowLifeCycleEventDelegate WorkflowLifeCycleEvent;

        public delegate void WorkflowStepErrorDelegate(WorkflowStepException e);
        public static event WorkflowStepErrorDelegate WorkflowStepError;

        public static IServiceProvider ConfigureServices()
        {
            //setup dependency injection
            IServiceCollection services = new ServiceCollection();
            services.AddLogging();
            services.AddWorkflow();
            services.AddTransient<HtmlStepNavigate>();
            services.AddScoped<BrowserForm>();

            //var config = new TypeAdapterConfig();
            //services.AddSingleton(config);
            //services.AddScoped<IMapper, ServiceMapper>();

            //services.AddMapster(options =>
            //{
            //    options.
            //});

            //services.AddWorkflow(x => x.UseSqlServer(@"Server=.\SQLEXPRESS;Database=WorkflowCore;Trusted_Connection=True;", true, true));
            var serviceProvider = services.BuildServiceProvider();

            RegisterWorkflowService(serviceProvider);
            return serviceProvider;
        }

        private static void RegisterWorkflowService(IServiceProvider serviceProvider) {
            var host = serviceProvider.GetService<IWorkflowHost>();
            host.RegisterWorkflow<IDSTWorkFlow, WorkflowData>();
            host.OnStepError += Host_OnStepError;
            host.OnLifeCycleEvent += Host_OnLifeCycleEvent;
            host.Start();
        }

        private static void Host_OnLifeCycleEvent(LifeCycleEvent evt)
        {
            WorkflowLifeCycleEvent?.Invoke(evt);
        }

        private static void Host_OnStepError(WorkflowInstance workflow, WorkflowStep step, Exception exception)
        {
            WorkflowStepError?.Invoke(new WorkflowStepException() {
                ExtraDetail = "No Extra Detail",
                workflow = workflow,
                step = step,
                exception = exception
            });
        }
    }
}
