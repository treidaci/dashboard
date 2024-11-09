using DashboardCore.Entities;
using DashboardCore.Repositories;

namespace DashboardApplication.Library.Detection.Rules;

internal class InhumanSpeedActionRule(IPlayerActivityRepository playerActivityRepository) : BaseActionRule
{
    // this can be read from config in future iterations
    private readonly TimeSpan _inhumanSpeedThreshold = TimeSpan.FromMilliseconds(100);

    protected override string Reason()
    {
        return "Inhuman speed";
    }

    protected override async Task<bool> ApplyRule(PlayerActivity activity)
    {
        // Get activities that fall within the time range for the same player
        var activities = await playerActivityRepository.GetActivitiesWithinThreshold(activity, _inhumanSpeedThreshold);
        
        // Return true if any activities are found within the time range
        return activities.Any();
    }
}