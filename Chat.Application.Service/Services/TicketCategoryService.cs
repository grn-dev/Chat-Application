using FluentValidation.Results;
using Chat.Application.Services.TicketCategory;
using Chat.Application.ViewModels;
using Chat.Domain.Attributes;
using Chat.Domain.Core.SeedWork;
using System.Threading.Tasks;
using Chat.Application.Configuration.Data.BasicQuery;
using Garnet.Standard.Pagination;
using Chat.Domain.Models.Ticket;
using Chat.Application.Configuration.Data.BasicCommand;
using Chat.Domain.Commands.Message;

namespace Chat.Application.Service.Services
{
    [Bean]
    public class TicketCategoryService : ApplicationService, ITicketCategoryService
    {
        private readonly IMyMediatorHandler _mediator;


        public TicketCategoryService(
            IMyMediatorHandler mediatorHandler) : base(mediatorHandler)
        {
            _mediator = mediatorHandler;
        }

        public async Task<TicketCategoryViewModel.TicketCategoryExpose> Get(int id)
        {
            return await _mediator.SendQuery(
                new GetPredicateToDestQuery<TicketCategory, int, TicketCategoryViewModel.TicketCategoryExpose>()
                {
                    Predicate = c => c.Id == id
                });
        }

        public async Task<IPagedElements<TicketCategoryViewModel.TicketCategoryExpose>> GetAllPagination(
            IPagination pagination)
        {
            return await _mediator.SendQuery(
                new PagableQuery<TicketCategory, int, TicketCategoryViewModel.TicketCategoryExpose>()
                {
                    Pagination = pagination
                });
        }


        public async Task<ValidationResult> Register(TicketCategoryViewModel.RegisterTicketCategory ticketCategory)
        {
            var registerTicketCategory = TicketCategory.CreateRegistered(
                ticketCategory.Description,
                ticketCategory.Name); 

            return await SingleCommandExecutor<object, int>(_ =>
                _mediator.SendCommand(new GenericCreateCommand<TicketCategory, int>
                {
                    DomainInitiator = () => Task.FromResult(registerTicketCategory)
                }), null);
        }

        public async Task<ValidationResult> Update(TicketCategoryViewModel.UpdateTicketCategory ticketCategory)
        {
            var updateTicketCategory = await _mediator.SendQuery(
                new GetPredicateQuery<TicketCategory, int>()
                {
                    Predicate = c => c.Id == ticketCategory.Id
                });

            return await SingleCommandExecutor<object, int>(_ =>
                _mediator.SendCommand(new GenericUpdateCommand<TicketCategory, int>
                {
                    DomainInitiator = () => Task.FromResult(updateTicketCategory),
                    DomainFieldUpdater = ac => ac.Update(ticketCategory.Description,
                        ticketCategory.Name)
                }), null);
        }


        public async Task<ValidationResult> Delete(int id)
        {
            var deleteTicketCategory = TicketCategory.DeleteRegistered(id);

            return await SingleCommandExecutor<object, int>(_ =>
                _mediator.SendCommand(new GenericDeleteCommand<TicketCategory, int>
                {
                    DomainInitiator = () => Task.FromResult(deleteTicketCategory)
                }), null);
        }
    }
}