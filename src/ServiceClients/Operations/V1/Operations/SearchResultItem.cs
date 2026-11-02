namespace HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1.Operations;


public class SearchResultItem
{
    public SearchResultItem(
        Guid id,
        DateTime date,
        string operationType,
        Guid sourceAccountId,
        Guid targetAccountId,
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

    public Guid Id { get; }

    public DateTime Date { get; }

    public string OperationType { get; }

    public Guid SourceAccountId { get; }

    public Guid TargetAccountId { get; }

    public string Title { get; }

    public decimal Amount { get; }

    public string Currency { get; }

    public string CategoryId { get; }

    public string BudgetId { get; }
}
