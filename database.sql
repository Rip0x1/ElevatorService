CREATE DATABASE ElevatorService;
USE ElevatorService;

CREATE TABLE Clients (
    ClientId INT AUTO_INCREMENT PRIMARY KEY,
    CompanyName VARCHAR(100) NOT NULL,
    ContactPerson VARCHAR(100),
    Phone VARCHAR(20),
    Email VARCHAR(100),
    Address VARCHAR(200)
);

CREATE TABLE Elevators (
    ElevatorId INT AUTO_INCREMENT PRIMARY KEY,
    ClientId INT,
    SerialNumber VARCHAR(50) NOT NULL,
    Model VARCHAR(50),
    InstallationDate DATE,
    LastServiceDate DATE,
    Status ENUM('Active', 'Inactive', 'UnderMaintenance') DEFAULT 'Active',
    FOREIGN KEY (ClientId) REFERENCES Clients(ClientId)
);

CREATE TABLE Employees (
    EmployeeId INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Position VARCHAR(50),
    Phone VARCHAR(20),
    Email VARCHAR(100)
);

CREATE TABLE ServiceRequests (
    RequestId INT AUTO_INCREMENT PRIMARY KEY,
    ElevatorId INT,
    ClientId INT,
    EmployeeId INT,
    RequestDate DATETIME NOT NULL,
    Description TEXT,
    Status ENUM('Open', 'InProgress', 'Closed') DEFAULT 'Open',
    Priority ENUM('Low', 'Medium', 'High') DEFAULT 'Medium',
    FOREIGN KEY (ElevatorId) REFERENCES Elevators(ElevatorId),
    FOREIGN KEY (ClientId) REFERENCES Clients(ClientId),
    FOREIGN KEY (EmployeeId) REFERENCES Employees(EmployeeId)
);

CREATE TABLE Parts (
    PartId INT AUTO_INCREMENT PRIMARY KEY,
    PartName VARCHAR(100) NOT NULL,
    PartNumber VARCHAR(50),
    QuantityInStock INT DEFAULT 0,
    Price DECIMAL(10, 2)
);

CREATE TABLE ServiceLogs (
    LogId INT AUTO_INCREMENT PRIMARY KEY,
    RequestId INT,
    EmployeeId INT,
    ServiceDate DATETIME NOT NULL,
    Description TEXT,
    PartsUsed TEXT,
    FOREIGN KEY (RequestId) REFERENCES ServiceRequests(RequestId),
    FOREIGN KEY (EmployeeId) REFERENCES Employees(EmployeeId)
);

INSERT INTO Clients (CompanyName, ContactPerson, Phone, Email, Address) VALUES
('ООО ЛифтТех', 'Иван Петров', '+79991234567', 'ivan@lifttech.ru', 'Москва, ул. Ленина, 10'),
('ЖК Солнечный', 'Мария Иванова', '+79992345678', 'maria@sunny.ru', 'Санкт-Петербург, ул. Мира, 5'),
('ТЦ Гранд', 'Сергей Сидоров', '+79993456789', 'sergey@grand.ru', 'Екатеринбург, ул. Победы, 20');

INSERT INTO Elevators (ClientId, SerialNumber, Model, InstallationDate, LastServiceDate, Status) VALUES
(1, 'LT12345', 'LiftModelX', '2020-05-15', '2025-01-10', 'Active'),
(2, 'LT67890', 'LiftModelY', '2019-08-20', '2024-12-01', 'UnderMaintenance'),
(3, 'LT11223', 'LiftModelZ', '2021-03-10', '2025-02-15', 'Active');

INSERT INTO Employees (FirstName, LastName, Position, Phone, Email) VALUES
('Алексей', 'Смирнов', 'Техник', '+79994567890', 'alexey@elevatorservice.ru'),
('Елена', 'Кузнецова', 'Менеджер', '+79995678901', 'elena@elevatorservice.ru'),
('Дмитрий', 'Васильев', 'Инженер', '+79996789012', 'dmitry@elevatorservice.ru');

INSERT INTO ServiceRequests (ElevatorId, ClientId, EmployeeId, RequestDate, Description, Status, Priority) VALUES
(1, 1, 1, '2025-03-01 10:00:00', 'Не работает кнопка 5 этажа', 'Open', 'High'),
(2, 2, 2, '2025-03-15 14:30:00', 'Шум при движении лифта', 'InProgress', 'Medium'),
(3, 3, 3, '2025-04-01 09:00:00', 'Плановое обслуживание', 'Closed', 'Low');

INSERT INTO Parts (PartName, PartNumber, QuantityInStock, Price) VALUES
('Двигатель лифта', 'ENG123', 5, 15000.00),
('Кабель управления', 'CBL456', 10, 5000.00),
('Кнопка вызова', 'BTN789', 20, 1000.00);

INSERT INTO ServiceLogs (RequestId, EmployeeId, ServiceDate, Description, PartsUsed) VALUES
(1, 1, '2025-03-02 12:00:00', 'Диагностика проведена, требуется замена кнопки', 'BTN789'),
(2, 2, '2025-03-16 16:00:00', 'Проверка механизмов, смазка', NULL),
(3, 3, '2025-04-02 11:00:00', 'Плановое обслуживание завершено', NULL);