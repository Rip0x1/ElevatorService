using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorServiceApp
{
    public class ServiceRequest
    {
        public int RequestId { get; set; }
        public int ElevatorId { get; set; }
        public int ClientId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime RequestDate { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? Priority { get; set; }
    }

    public class Elevator
    {
        public int ElevatorId { get; set; }
        public int ClientId { get; set; }
        public string? SerialNumber { get; set; }
        public string? Model { get; set; }
        public DateTime InstallationDate { get; set; }
        public DateTime? LastServiceDate { get; set; }
        public string? Status { get; set; }
    }

    public class Client
    {
        public int ClientId { get; set; }
        public string? CompanyName { get; set; }
        public string? ContactPerson { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string DisplayName => $"{ClientId}: {CompanyName}";
    }

    public class Employee
    {
        public int EmployeeId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Position { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string DisplayName => $"{EmployeeId}: {FirstName} {LastName}";
    }

    public class Part
    {
        public int PartId { get; set; }
        public string? PartName { get; set; }
        public string? PartNumber { get; set; }
        public int QuantityInStock { get; set; }
        public decimal Price { get; set; }
    }

    public class ServiceLog
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime ServiceDate { get; set; }
        public string? Description { get; set; }
        public string? PartsUsed { get; set; }
    }
}