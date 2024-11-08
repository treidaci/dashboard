using DashboardCore.Entities;

namespace DashboardApplication.Library.Detection.Rules;

public abstract class BaseActionRule : IDetectionRule
{
    public async Task<bool> Apply(PlayerActivity activity)
    {
        var ruleApplied = await ApplyRule(activity);
        if (ruleApplied)
        {
            activity.MarkAsSuspicious(Reason());
        }
        
        return ruleApplied;
    }

    protected abstract string Reason();

    protected abstract Task<bool> ApplyRule(PlayerActivity activity);
}