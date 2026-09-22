namespace HomeBudgetManager.Bff.WebUI.WebApi.Controllers.GroupedOperations;

public class OperationsInGroup
{
    public OperationsInGroup(
        string groupType,
        string groupId,
        string groupName,
        int operationsCount,
        decimal operationsTotalAmount,
        string currency,
        string period)
    {
        this.GroupType = groupType;
        this.GroupId = groupId;
        this.GroupName = groupName;
        this.OperationsCount = operationsCount;
        this.OperationsTotalAmount = operationsTotalAmount;
        this.Currency = currency;
        this.Period = period;
    }

    public string GroupType { get; }

    public string GroupId { get; }

    public string GroupName { get; }

    public int OperationsCount { get; }

    public decimal OperationsTotalAmount { get; }

    public string Currency { get; }

    public string Period { get; }
}
