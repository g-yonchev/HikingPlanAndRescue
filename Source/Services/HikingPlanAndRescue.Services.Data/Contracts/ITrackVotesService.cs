namespace HikingPlanAndRescue.Services.Data.Contracts
{
    using Common;
    using HikingPlanAndRescue.Data.Models;

    public interface ITrackVotesService : IBaseDataWithCreatorService<TrackVote>
    {
        void AddVote(TrackVote vote);

        int GetTrackVotesSum(int trackId);
    }
}