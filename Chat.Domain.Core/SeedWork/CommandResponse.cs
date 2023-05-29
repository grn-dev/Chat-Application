using FluentValidation.Results;
using System.Collections.Generic;

namespace Chat.Domain.Core.SeedWork
{
    public class CommandResponse<R>
    {
        public R Response { get; set; }
        public ValidationResult ValidationResult { get; set; }
    }

    public class CommandResponseBatchValidationResult<R>
    {
        public R Response { get; set; }
        public IList<ValidationResult> ValidationResults { get; set; }
    }

    public class CommandResponseBatch<R>
    {
        public IList<R> Response { get; set; }
        public ValidationResult ValidationResult { get; set; }
    }
}