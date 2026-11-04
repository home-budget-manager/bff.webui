namespace HomeBudgetManager.Bff.WebUI.WebApi.Controllers.V2.GroupedOperations;

using HomeBudgetManager.Bff.WebUI.WebApi.Controllers.V2.Operations;

public class GetOperationsParameters
{
    public string Period { get; set; } = string.Empty;

    public OperationType OperationType { get; set; }
}
