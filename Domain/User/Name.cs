using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.User;

[ComplexType]
public sealed record Name(string FirstName, string LastName);