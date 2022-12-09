namespace BlazorComponents.Models;

public record ReadOnlyUser(string? FirstName, string? LastName, string? Email, string? Phone, byte[]? Photo);