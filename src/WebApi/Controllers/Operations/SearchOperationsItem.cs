namespace HomeBudgetManager.Bff.WebUI.WebApi.Controllers.Operations;

public class SearchOperationsItem
{
    public SearchOperationsItem(
        string id,
        DateTime date,
        OperationType operationType,
        string sourceAccountId,
        string targetAccountId,
        string title,
        decimal amount,
        string currency,
        string categoryId,
        string budgetId)
    {
        this.Id = id;
        this.Date = date;
        this.OperationType = operationType;
        this.SourceAccountId = sourceAccountId;
        this.TargetAccountId = targetAccountId;
        this.Title = title;
        this.Amount = amount;
        this.Currency = currency;
        this.CategoryId = categoryId;
        this.BudgetId = budgetId;
    }

    public string Id { get; }

    public DateTime Date { get; }

    public OperationType OperationType { get; }

    public string SourceAccountId { get; }

    public string TargetAccountId { get; }

    public string Title { get; }

    public decimal Amount { get; }

    public string Currency { get; }

    public string CategoryId { get; }

    public string BudgetId { get; }
}
