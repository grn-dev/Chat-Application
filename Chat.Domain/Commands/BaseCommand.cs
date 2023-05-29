using System;
using FluentValidation.Results;
using Chat.Domain.Attributes;
using Chat.Domain.Core.SeedWork;
using MediatR;

namespace Chat.Domain.Commands
{
    [Transactional]
    public abstract class BaseCommand<R> : NetDevPack.Messaging.Message, IRequest<CommandResponse<R>>, IBaseRequest
    {
        public DateTime Timestamp { get; private set; }
        public R Id { get; set; }
        public ValidationResult ValidationResult { get; set; }

        protected BaseCommand()
        {
            Timestamp = DateTime.Now;
            ValidationResult = new ValidationResult();
        }

        public virtual bool IsValid()
        {
            return ValidationResult.IsValid;
        }
    }
    [Transactional]
    public abstract class BaseCommandWithDomain<TDomin> : NetDevPack.Messaging.Message, IRequest<CommandResponse<TDomin>>, IBaseRequest
    {
        public DateTime Timestamp { get; private set; }
        public TDomin Domin { get; set; }
        public ValidationResult ValidationResult { get; set; }

        protected BaseCommandWithDomain()
        {
            Timestamp = DateTime.Now;
            ValidationResult = new ValidationResult();
        }

        public virtual bool IsValid()
        {
            return ValidationResult.IsValid;
        }
    }

    [Transactional]
    public abstract class BaseBatchCommand<R> : NetDevPack.Messaging.Message, IRequest<CommandResponseBatch<R>>, IBaseRequest
    {
        public DateTime Timestamp { get; private set; }
        public R Id { get; set; }
        public ValidationResult ValidationResult { get; set; }

        protected BaseBatchCommand()
        {
            Timestamp = DateTime.Now;
            ValidationResult = new ValidationResult();
        }

        public virtual bool IsValid()
        {
            return ValidationResult.IsValid;
        }
    }
}
