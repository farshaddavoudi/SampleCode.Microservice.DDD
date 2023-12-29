using Hangfire;
using MediatR;
using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;

namespace SampleDDDMicroserviceApp.TransportPeople.Infrastructure.BackgroundJobs;

public class HangfireRecurringJobsService : IAppBackgroundJobsService
{
    private readonly IRecurringJobManager _recurringJobManager;
    private readonly IMediator _mediator;

    #region Constructor
    public HangfireRecurringJobsService(IRecurringJobManager recurringJobManager, IMediator mediator)
    {
        _recurringJobManager = recurringJobManager;
        _mediator = mediator;
    }
    #endregion

    public void ScheduleJob_SyncInsuredsTable()
    {
        _recurringJobManager.AddOrUpdate(HangfireConst.JobId.SyncUsersWithRahkaran,
            HangfireConst.Queue.DefaultQueue,
            () => SyncInsuredsTableAsync(CancellationToken.None),
            Cron.Daily);
    }

    public void RemoveJob_SyncInsuredsTable()
    {
        _recurringJobManager.RemoveIfExists(HangfireConst.JobId.SyncUsersWithRahkaran);
    }

    #region JOB METHODS

    public async Task SyncInsuredsTableAsync(CancellationToken cancellationToken)
    {
        //await _mediator.Send(new SyncInsuredsTableCommand(), cancellationToken);

        //TODO: Put it in Try/Catch and SMS developer on error

        await Task.CompletedTask;
    }

    #endregion
}