namespace SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;

public interface IAppBackgroundJobsService
{
    void ScheduleJob_SyncInsuredsTable();
    void RemoveJob_SyncInsuredsTable();
}