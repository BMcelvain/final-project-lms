﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System;
using System.Linq;
using Serilog;

namespace Lms.APIErrorHandling
{
    public class ApiBadRequestResponse : ApiResponse
    {
        public object Errors { get; }

        public ApiBadRequestResponse(object errors)
            : base(400)
        {
            Errors = errors;
            Log.Information(errors.ToString());
        }
    }
}
