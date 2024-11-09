using DashboardCore.Entities;
using DashboardCore.Repositories;

namespace DashboardApplication.Library.Detection.Rules;

internal class IdenticalActionRule(IPlayerActivityRepository playerActivityRepository) : BaseActionRule
{
    protected override string Reason()
    {
        // can be a reason code in future iterations 
       return "Identical action";
    }

    protected override async Task<bool> ApplyRule(PlayerActivity activity)
    {
        var activities = await playerActivityRepository.GetActivitiesByFilter(a=> a.PlayerId == activity.PlayerId && 
                                                           a.Action == activity.Action &&
                                                           a.Timestamp == activity.Timestamp && 
                                                           a.Id != activity.Id // Exclude the current activity itself
            );
        return activities.Any();
    }
}