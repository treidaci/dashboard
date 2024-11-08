using DashboardCore.Entities;

namespace DashboardApplication.Library.Detection.Rules;

public interface IDetectionRule
{
    Task<bool> Apply(PlayerActivity activity);
}