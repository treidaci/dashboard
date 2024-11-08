using DashboardApplication.Library.Detection.Rules;
using DashboardCore.Repositories;

namespace DashboardApplication.Library.Detection;

internal class DetectionService(IPlayerActivityRepository playerActivityRepository, IEnumerable<IDetectionRule> rules) : IDetectionService
{
    public async Task AnalysePlayerActivity(string id, string playerId)
    {
        var activity = await playerActivityRepository.GetActivity(id, playerId);
        if (activity == null) return;
        
        // this is equivalent to a basic foreach loop
        // refactored after R# advice - foreach reads easier imho
        var anyRuleApplied = await rules.Aggregate(Task.FromResult(false), 
            async (current, rule) => await current | await rule.Apply(activity));

        if (anyRuleApplied)
        {
            await playerActivityRepository.UpdateActivity(activity);
        }
    }
}