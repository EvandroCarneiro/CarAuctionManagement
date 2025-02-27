namespace BCA.CarAuctionManagement.Infrastructure.Dependencies.Mediator;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

internal class CustomMediator(IServiceProvider serviceProvider) : Mediator(serviceProvider)
{
    protected override async Task PublishCore(
        IEnumerable<NotificationHandlerExecutor> handlerExecutors, 
        INotification notification, 
        CancellationToken cancellationToken)
    {
        foreach (var handler in handlerExecutors)
        {
            _ = Task.Run(() =>
            {

            });
        }

        await Task.CompletedTask;
    }
}
