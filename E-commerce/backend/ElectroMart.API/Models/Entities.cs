using System.ComponentModel.DataAnnotations;

namespace ElectroMart.API.Models;

public enum UserRole { Customer, Admin }
public enum OrderStatus { Pending, Confirmed, Shipped, Delivered, Cancelled }
public enum PaymentStatus { Pending, Paid, Failed, Refunded }

public class User
{
    public int Id { get; set; }
    [Required, MaxLength(80)] public string FirstName { get; set; } = "";
    [Required, MaxLength(80)] public string LastName { get; set; } = "";
    [Required, EmailAddress, MaxLength(180)] public string Email { get; set; } = "";
    [Required] public string PasswordHash { get; set; } = "";
    public UserRole Role { get; set; } = UserRole.Customer;
    [MaxLength(30)] public string? PhoneNumber { get; set; }
    [MaxLength(250)] public string? Address { get; set; }
    [MaxLength(80)] public string? City { get; set; }
    [MaxLength(80)] public string? State { get; set; }
    [MaxLength(20)] public string? PostalCode { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Cart? Cart { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}

public class Category
{
    public int Id { get; set; }
    [Required, MaxLength(100)] public string Name { get; set; } = "";
    [MaxLength(500)] public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Product> Products { get; set; } = new List<Product>();
}

public class Product
{
    public int Id { get; set; }
    [Required, MaxLength(180)] public string Name { get; set; } = "";
    [Required, MaxLength(80)] public string Brand { get; set; } = "";
    [Required, MaxLength(2000)] public string Description { get; set; } = "";
    [Range(0, 1000000)] public decimal Price { get; set; }
    public decimal? DiscountPrice { get; set; }
    [Range(0, 1000000)] public int StockQuantity { get; set; }
    public string? ImageUrl { get; set; }
    public int CategoryId { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsActive { get; set; } = true;
    public string? Specifications { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public Category? Category { get; set; }
}

public class Cart
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public User? User { get; set; }
    public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
}
public class CartItem
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public Cart? Cart { get; set; }
    public Product? Product { get; set; }
}
public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    [Required, MaxLength(30)] public string OrderNumber { get; set; } = "";
    public decimal TotalAmount { get; set; }
    [Required, MaxLength(500)] public string ShippingAddress { get; set; } = "";
    [Required, MaxLength(40)] public string PaymentMethod { get; set; } = "";
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public User? User { get; set; }
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    [Required] public string ProductName { get; set; } = "";
    public string? ProductImageUrl { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal Subtotal { get; set; }
    public Order? Order { get; set; }
    public Product? Product { get; set; }
}
