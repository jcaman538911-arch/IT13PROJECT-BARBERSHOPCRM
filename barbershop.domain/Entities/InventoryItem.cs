namespace barbershop.domain;

public class InventoryItem
{
    public int Id { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public string Category { get; set; } = "General";
    public int Quantity { get; set; }
    public string Unit { get; set; } = "pcs";
    public int MinimumStockLevel { get; set; } = 5;
    public int? SupplierId { get; set; }
    public string SupplierName { get; set; } = "None";
    public decimal Cost { get; set; }
    public string Status { get; set; } = "IN STOCK"; // IN STOCK, LOW STOCK, OUT OF STOCK
    public int? BranchId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}

public class InventoryTransaction
{
    public int Id { get; set; }
    public int InventoryItemId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public string TransactionType { get; set; } = "STOCK IN"; // STOCK IN, STOCK OUT, USED SUPPLY, RESTOCK
    public int Quantity { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public string RecordedBy { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}
