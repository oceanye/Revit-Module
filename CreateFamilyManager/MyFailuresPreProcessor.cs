﻿using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateFamilyManager
{
  public   class MyFailuresPreProcessor : IFailuresPreprocessor
    {
        private string _failureMessage;
        private bool _hasError;
        public string FailureMessage
        {
            get { return _failureMessage; }
            set { _failureMessage = value; }
        }
        public bool HasError
        {
            get { return _hasError; }
            set { _hasError = value; }
        }
        public FailureProcessingResult PreprocessFailures(FailuresAccessor failuresAccessor)
        {
            var failures = failuresAccessor.GetFailureMessages();
            if (failures.Count == 0)
            {
                return FailureProcessingResult.Continue;
            }
            foreach (var failure in failures)
            {
                if (failure.GetSeverity() == FailureSeverity.Error)
                {
                    _failureMessage = failure.GetDescriptionText();
                    _hasError = true;
                    return FailureProcessingResult.ProceedWithRollBack;
                }
                if (failure.GetSeverity() == FailureSeverity.Warning)
                {
                    failuresAccessor.DeleteWarning(failure);

                }
            }
            return FailureProcessingResult.Continue;
        }
    }
}
