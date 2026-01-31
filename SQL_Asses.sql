 create database Assign;
 
 use Assign;

 create table CustomersData(
	CustomerID INT auto_increment PRIMARY KEY,
    CustomerName VARCHAR(100),
    CustomerPhone VARCHAR(20),
    CustomerCity VARCHAR(50)
 );

 create table  SalesPersonsData(
    SalesPersonID INT AUTO_INCREMENT PRIMARY KEY,
    SalesPersonName VARCHAR(100)
);

create table ProductData(
	ProductID INT AUTO_INCREMENT PRIMARY KEY,
    ProductName VARCHAR(100)
);

create table OrdersData(
	OrderID INT PRIMARY KEY,
    OrderDate DATE,
    CustomerID INT,
    SalesPersonID INT,
    FOREIGN KEY (CustomerID) REFERENCES CustomersData(CustomerID),
    FOREIGN KEY (SalesPersonID) REFERENCES SalesPersonsData(SalesPersonID)
);

create table OrderDetailsData(
	OrderDetailID INT AUTO_INCREMENT PRIMARY KEY,
    OrderID INT,
    ProductID INT,
    Quantity INT,
    UnitPrice DECIMAL(10,2),
    FOREIGN KEY (OrderID) REFERENCES OrdersData(OrderID),
    FOREIGN KEY (ProductID) REFERENCES ProductData(ProductID)
);

CREATE TABLE Sales_Raw(
    OrderID INT,
    OrderDate VARCHAR(20),
    CustomerName VARCHAR(100),
    CustomerPhone VARCHAR(20),
    CustomerCity VARCHAR(50),
    ProductNames VARCHAR(200),
    Quantities VARCHAR(100),
    UnitPrices VARCHAR(100),
    SalesPerson VARCHAR(100)
);

INSERT INTO Sales_Raw VALUES
(101, '2024-01-05', 'Ravi Kumar', '9876543210', 'Chennai', 'Laptop,Mouse', '1,2', '55000,500', 'Anitha'),
(102, '2024-01-06', 'Priya Sharma', '9123456789', 'Bangalore', 'Keyboard,Mouse', '1,1', '1500,500', 'Anitha'),
(103, '2024-01-10', 'Ravi Kumar', '9876543210', 'Chennai', 'Laptop', '1', '54000', 'Suresh'),
(104, '2024-02-01', 'John Peter', '9988776655', 'Hyderabad', 'Monitor,Mouse', '1,1', '12000,500', 'Anitha'),
(105, '2024-02-10', 'Priya Sharma', '9123456789', 'Bangalore', 'Laptop,Keyboard', '1,1', '56000,1500', 'Suresh');

-- QUESTION - 2

SELECT OrderID, TotalSales
FROM (
    SELECT 
        OrderID,
        SUM(qty * price) AS TotalSales,
        DENSE_RANK() OVER (ORDER BY SUM(qty * price) DESC) AS rnk
    FROM (
        SELECT 
            OrderID,
            CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(Quantities, ',', n.n), ',', -1) AS UNSIGNED) AS qty,
            CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(UnitPrices, ',', n.n), ',', -1) AS DECIMAL(10,2)) AS price
        FROM Sales_Raw
        JOIN (
            SELECT 1 AS n UNION ALL
            SELECT 2 UNION ALL
            SELECT 3 UNION ALL
            SELECT 4 UNION ALL
            SELECT 5
        ) n
        ON n.n <= 1 + LENGTH(Quantities) - LENGTH(REPLACE(Quantities, ',', ''))
    ) t
    GROUP BY OrderID
) ranked
WHERE rnk = 3;

-- QUESTION - 3

SELECT SalesPerson, SUM(qty * price) AS TotalSales
FROM (
    SELECT 
        SalesPerson,
        CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(Quantities, ',', n.n), ',', -1) AS UNSIGNED) AS qty,
        CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(UnitPrices, ',', n.n), ',', -1) AS DECIMAL(10,2)) AS price
    FROM Sales_Raw
    JOIN (
        SELECT 1 AS n UNION ALL
        SELECT 2 UNION ALL
        SELECT 3 UNION ALL
        SELECT 4 UNION ALL
        SELECT 5
    ) n
    ON n.n <= 1 + LENGTH(Quantities) - LENGTH(REPLACE(Quantities, ',', ''))
) t
GROUP BY SalesPerson
HAVING SUM(qty * price) > 60000;

-- QUESTION - 4

WITH RECURSIVE nums AS (
    SELECT 1 AS n
    UNION ALL
    SELECT n + 1
    FROM nums
    WHERE n < 5
),
CustomerTotals AS (
    SELECT 
        CustomerName,
        SUM(
            CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(Quantities, ',', n), ',', -1) AS UNSIGNED) *
            CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(UnitPrices, ',', n), ',', -1) AS DECIMAL(10,2))
        ) AS TotalSpent
    FROM Sales_Raw
    JOIN nums
      ON n <= 1 + LENGTH(Quantities) - LENGTH(REPLACE(Quantities, ',', ''))
    GROUP BY CustomerName
)
SELECT CustomerName, TotalSpent
FROM CustomerTotals
WHERE TotalSpent >
(
    SELECT AVG(TotalSpent)
    FROM CustomerTotals
);


-- QUESTION - 5

SELECT 
    UPPER(CustomerName) AS CustomerName,
    MONTH(STR_TO_DATE(OrderDate, '%Y-%m-%d')) AS OrderMonth,
    OrderDate
FROM Sales_Raw
WHERE 
    YEAR(STR_TO_DATE(OrderDate, '%Y-%m-%d')) = 2026
    AND MONTH(STR_TO_DATE(OrderDate, '%Y-%m-%d')) = 1;
