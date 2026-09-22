namespace HomeBudgetManager.Bff.WebUI.WebApi.Controllers.GroupedOperations;

using HomeBudgetManager.Bff.WebUI.WebApi.Controllers.Operations;

public class GetOperationsParameters
{
    public string Period { get; set; } = string.Empty;

    public OperationType OperationType { get; set; }
}
