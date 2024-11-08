namespace DashboardApplication.Library.Detection;

public interface IDetectionService
{
    Task AnalysePlayerActivity(string id, string playerId);
}