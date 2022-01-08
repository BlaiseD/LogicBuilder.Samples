﻿using AutoMapper;
using Enrollment.Forms.View;
using Enrollment.Repositories;
using Enrollment.Web.Flow.Cache;
using Enrollment.Web.Flow.Dialogs;
using Enrollment.Web.Flow.Requests;
using Enrollment.Web.Flow.ScreenSettings;
using Enrollment.Web.Flow.ScreenSettings.Views;
using LogicBuilder.RulesDirector;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Enrollment.Web.Flow
{
    public class FlowManager : IFlowManager
    {
        public FlowManager(IMapper mapper,
            ICustomDialogs customDialogs,
            ICustomActions customActions,
            DirectorFactory directorFactory,
            FlowActivityFactory flowActivityFactory,
            IEnrollmentRepository enrollmentRepository,
            ILogger<FlowManager> logger,
            FlowDataCache flowDataCache)
        {
            this.CustomDialogs = customDialogs;
            this.CustomActions = customActions;
            this._logger = logger;
            this.EnrollmentRepository = enrollmentRepository;
            this.Mapper = mapper;
            this.Director = directorFactory.Create(this);
            this.FlowActivity = flowActivityFactory.Create(this);
            this.FlowDataCache = flowDataCache;
        }

        #region Constants
        #endregion Constants

        #region Fields
        private ILogger<FlowManager> _logger;
        #endregion Fields

        #region Properties
        public Progress Progress { get; } = new Progress();
        public FlowDataCache FlowDataCache { get; set; }

        public ICustomActions CustomActions { get; private set; }

        public ICustomDialogs CustomDialogs { get; private set; }
        public DirectorBase Director { get; }
        public IFlowActivity FlowActivity { get; }

        public IEnrollmentRepository EnrollmentRepository { get; }
        public IMapper Mapper { get; }

        private FlowSettings FlowSettings
            => new FlowSettings
            (
                FlowDataCache.UserId,
                ((Director)this.Director).FlowState,
                FlowDataCache.NavigationBar,
                FlowDataCache.ScreenSettings
            );
        #endregion Properties

        public void FlowComplete()
        {
            _logger.LogWarning(0, string.Format("FlowComplete {0}", Newtonsoft.Json.JsonConvert.SerializeObject(this.Progress)));
            FlowDataCache.ScreenSettings = new ScreenSettings<object>(null, (IEnumerable<CommandButtonView>)null, ViewType.FlowComplete);
        }

        public void SetCurrentBusinessBackupData()
        {
        }

        public void Terminate()
        {
        }

        private FlowSettings GetFlowSettings(Exception ex)
            => new FlowSettings
            (
                FlowDataCache.UserId,
                ((Director)this.Director).FlowState,
                FlowDataCache.NavigationBar,
                new ScreenSettings<ExceptionView>
                (
                    new ExceptionView { Message = ex.Message },
                    new CommandButtonView[] { },
                    ViewType.Exception
                )
            );

        public FlowSettings Start(string module, int stage)
        {
            try
            {
                FlowDataCache.RequestedFlowStage = new RequestedFlowStage { InitialModule = module, TargetModule = stage };
                this.Director.StartInitialFlow(module);
                return this.FlowSettings;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(0, string.Format("Progress Start {0}", Newtonsoft.Json.JsonConvert.SerializeObject(this.Progress)));
                this._logger.LogError(ex, ex.Message);
                return this.GetFlowSettings(ex);
            }

        }

        public FlowSettings Next(RequestBase request)
        {
            try
            {
                IDialogHandler handler = BaseDialogHandler.Create(request);
                //temporarily disable server-side validation.
                IEnumerable<ValidationResult> errors = new List<ValidationResult>();//handler.GetErrors(request);
                if (errors.Count() > 0)
                {
                    return new FlowSettings(handler.GetScreenSettings(request, errors));
                }

                handler.Complete(this, request);
                this.Director.ExecuteRulesEngine();

                return this.FlowSettings;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(0, string.Format("Progress Next {0}", Newtonsoft.Json.JsonConvert.SerializeObject(this.Progress)));
                this._logger.LogError(ex, ex.Message);
                return this.GetFlowSettings(ex);
            }
        }

        public FlowSettings NavStart(NavBarRequest navBarRequest)
        {
            try
            {
                FlowDataCache.UserId = navBarRequest.UserId;
                FlowDataCache.RequestedFlowStage = new RequestedFlowStage
                {
                    InitialModule = navBarRequest.InitialModuleName,
                    TargetModule = navBarRequest.TargetModule
                };

                this.Director.StartInitialFlow(FlowDataCache.RequestedFlowStage.InitialModule);

                return this.FlowSettings;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(0, string.Format("Progress NavStart {0}", Newtonsoft.Json.JsonConvert.SerializeObject(this.Progress)));
                this._logger.LogError(ex, ex.Message);
                return this.GetFlowSettings(ex);
            }
        }
    }
}
