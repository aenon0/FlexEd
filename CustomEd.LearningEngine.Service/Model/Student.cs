using CustomEd.Shared.Model;

namespace CustomEd.LearningEngine.Service.Model;

public class Student: BaseEntity
{
    public string? StudentId {get; set;} 
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public Department Department { get; set; }
    public string? PhoneNumber {get; set;}
    public DateOnly? JoinDate { get; set; }
    public int? Year { get; set; }
    public string? Section { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public Role Role { get; set; }

}