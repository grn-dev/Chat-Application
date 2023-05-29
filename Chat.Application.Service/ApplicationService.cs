using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Chat.Application.Configuration.Exceptions;
using Chat.Domain.Commands;
using Chat.Domain.Core.SeedWork;

namespace Chat.Application.Service
{
    public abstract class ApplicationService
    {
        protected readonly IMyMediatorHandler MediatorHandler;

        protected ApplicationService(IMyMediatorHandler mediatorHandler)
        {
            MediatorHandler = mediatorHandler;
        }


        protected async Task<ValidationResult> CommandExecutor<T>(
            Func<T, Task<IList<ValidationResult>>> commandProcedure, T input)
        {
            try
            {
                var results = await commandProcedure(input);

                if (results.All(result => result.IsValid))
                {
                    await MediatorHandler.SendCommand(new CommitTransActionCommand());

                    return results.First();
                }

                await MediatorHandler.SendCommand(new RollBackTransActionCommand());

                return new ValidationResult(results.SelectMany(result => result.Errors));
            }
            catch (Exception ex)
            {
                await MediatorHandler.SendCommand(new RollBackTransActionCommand());
                throw;
            }
        }

        protected async Task<ValidationResult> SingleCommandExecutor<T, R>(
            Func<T, Task<CommandResponse<R>>> commandProcedure, T input)
        {
            try
            {
                var result = (await commandProcedure(input)).ValidationResult;

                if (result.IsValid)
                {
                    await MediatorHandler.SendCommand(new CommitTransActionCommand());

                    return result;
                }

                await MediatorHandler.SendCommand(new RollBackTransActionCommand());

                return result;
            }
            catch (Exception ex)
            {
                await MediatorHandler.SendCommand(new RollBackTransActionCommand());

                if (ex.InnerException != null && ex.InnerException.HResult == -2146232060)
                    throw new MyApplicationException(ApplicationErrorCode.CANT_DELETE_UPDATE);

                throw;
            }
        }

        protected async Task<R> SingleCommandExecutorWithOutput<T, R>(
            Func<T, Task<CommandResponse<R>>> commandProcedure, T input)
        {
            try
            {
                var result = (await commandProcedure(input));
                //var result = (await commandProcedure(input)).ValidationResult;

                if (result.ValidationResult.IsValid)
                {
                    await MediatorHandler.SendCommand(new CommitTransActionCommand());

                    return result.Response;
                }

                await MediatorHandler.SendCommand(new RollBackTransActionCommand());

                return result.Response;
            }
            catch (Exception ex)
            {
                await MediatorHandler.SendCommand(new RollBackTransActionCommand());

                if (ex.InnerException != null && ex.InnerException.HResult == -2146232060)
                    throw new MyApplicationException(ApplicationErrorCode.CANT_DELETE_UPDATE);

                throw;
            }
        }

        protected async Task<R> SingleCommandExecutorWithOutput<T, T1, R>(
            Func<T, T1, Task<CommandResponse<R>>> commandProcedure, T input, T1 input1)
        {
            try
            {
                var result = (await commandProcedure(input, input1));
                //var result = (await commandProcedure(input)).ValidationResult;

                if (result.ValidationResult.IsValid)
                {
                    await MediatorHandler.SendCommand(new CommitTransActionCommand());

                    return result.Response;
                }

                await MediatorHandler.SendCommand(new RollBackTransActionCommand());

                return result.Response;
            }
            catch (Exception ex)
            {
                await MediatorHandler.SendCommand(new RollBackTransActionCommand());

                if (ex.InnerException != null && ex.InnerException.HResult == -2146232060)
                    throw new MyApplicationException(ApplicationErrorCode.CANT_DELETE_UPDATE);

                throw;
            }
        }

        protected async Task<CommandResponse<R>> SingleCommandExecutorIncludeCommandReponse<T, R>(
            Func<T, Task<CommandResponse<R>>> commandProcedure, T input)
        {
            try
            {
                var result = (await commandProcedure(input));

                if (result.ValidationResult.IsValid)
                {
                    await MediatorHandler.SendCommand(new CommitTransActionCommand());

                    return result;
                }

                await MediatorHandler.SendCommand(new RollBackTransActionCommand());

                return result;
            }
            catch (Exception ex)
            {
                await MediatorHandler.SendCommand(new RollBackTransActionCommand());

                if (ex.InnerException.HResult == -2146232060)
                    throw new MyApplicationException(ApplicationErrorCode.CANT_DELETE_UPDATE);

                throw;
            }
        } 

        protected async Task<CommandResponseBatchValidationResult<R>> CommandExecutorIncludeCommandReponse<T, T2, R>(
            Func<T, T2, Task<CommandResponseBatchValidationResult<R>>> commandProcedure, T input, T2 input2)
        {
            try
            {
                var results = (await commandProcedure(input, input2));

                if (results.ValidationResults.All((result => result.IsValid)))
                {
                    await MediatorHandler.SendCommand(new CommitTransActionCommand());

                    return results;
                }

                await MediatorHandler.SendCommand(new RollBackTransActionCommand());

                return results;
            }
            catch (Exception ex)
            {
                await MediatorHandler.SendCommand(new RollBackTransActionCommand());

                if (ex.InnerException.HResult == -2146232060)
                    throw new MyApplicationException(ApplicationErrorCode.CANT_DELETE_UPDATE);

                throw;
            }
        }

        protected async Task<ValidationResult> SingleCommandExecutor<T, T2, R>(
            Func<T, T2, Task<CommandResponse<R>>> commandProcedure, T input, T2 input2)
        {
            try
            {
                var result = (await commandProcedure(input, input2)).ValidationResult;

                if (result.IsValid)
                {
                    await MediatorHandler.SendCommand(new CommitTransActionCommand());

                    return result;
                }

                await MediatorHandler.SendCommand(new RollBackTransActionCommand());

                return result;
            }
            catch (Exception ex)
            {
                await MediatorHandler.SendCommand(new RollBackTransActionCommand());
                throw;
            }
        }

        protected async Task<ValidationResult> CommandExecutor<T>(Func<T, Task<ValidationResult>> commandProcedure,
            T input)
        {
            try
            {
                var result = await commandProcedure(input);

                await MediatorHandler.SendCommand(new CommitTransActionCommand());

                return result;
            }
            catch (Exception)
            {
                await MediatorHandler.SendCommand(new RollBackTransActionCommand());
                throw;
            }
        }
    }
}