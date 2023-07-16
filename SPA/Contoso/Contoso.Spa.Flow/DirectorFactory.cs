﻿using LogicBuilder.RulesDirector;

namespace Contoso.Spa.Flow
{
    public class DirectorFactory : IDirectorFactory
    {
        public DirectorFactory(IRulesCache rulesCache)
        {
            this._rulesCache = rulesCache;
        }

        #region Variables
        private readonly IRulesCache _rulesCache;
        #endregion Variables

        public DirectorBase Create(IFlowManager flowManager)
            => new Director(flowManager, _rulesCache);
    }
}
