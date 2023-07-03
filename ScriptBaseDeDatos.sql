-- Usar la base de datos 'master'
USE master
GO

-- Crear la base de datos para el proyecto
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'puntoVenta')
BEGIN
	DROP DATABASE puntoVenta
END
GO
CREATE DATABASE puntoVenta
GO



-- Usar la base de datos creada
USE puntoVenta
GO

-- Crear las tablas sin relaciones
CREATE TABLE Impuestos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Porcentaje DECIMAL(5, 2) NOT NULL
)

CREATE TABLE Productos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Codigo VARCHAR(50) NOT NULL,
    Descripcion VARCHAR(100) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL,
    ImpuestoId INT NOT NULL
)

CREATE TABLE Clientes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Codigo VARCHAR(50) NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    Sexo VARCHAR(10) NOT NULL,
    RTN VARCHAR(14) NOT NULL,
    Direccion VARCHAR(200) NOT NULL
)

CREATE TABLE Facturas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ClienteId INT NOT NULL,
    FechaFactura DATE NOT NULL,
    Total DECIMAL(10, 2) NOT NULL,
    Estado VARCHAR(20) NOT NULL
)

CREATE TABLE DetalleFactura (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FacturaId INT NOT NULL,
    ProductoId INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10, 2) NOT NULL,
    Total DECIMAL(10, 2) NOT NULL
)

-- Crear las relaciones de las tablas
ALTER TABLE Productos ADD CONSTRAINT FK_Productos_Impuesto FOREIGN KEY (ImpuestoId) REFERENCES Impuestos(Id)
ALTER TABLE Facturas ADD CONSTRAINT FK_Facturas_Clientes FOREIGN KEY (ClienteId) REFERENCES Clientes(Id)
ALTER TABLE DetalleFactura ADD CONSTRAINT FK_DetalleFactura_Facturas FOREIGN KEY (FacturaId) REFERENCES Facturas(Id)
ALTER TABLE DetalleFactura ADD CONSTRAINT FK_DetalleFactura_Productos FOREIGN KEY (ProductoId) REFERENCES Productos(Id)
GO

-- Inserción de datos de ejemplo
-- Insert de impuestos
INSERT INTO Impuestos (Nombre, Porcentaje)
VALUES
    ('Impuesto 15%', 0.15),
    ('Impuesto 12%', 0.12),
	('Impuesto 18%', 0.18)

-- Insertar productos de supermercado con el impuesto del 15%
INSERT INTO Productos (Codigo, Descripcion, Precio, ImpuestoId)
VALUES
    ('PROD001', 'Arroz blanco', 45.99, 1),
    ('PROD002', 'Aceite de oliva', 120.50, 1),
    ('PROD003', 'Leche descremada', 28.99, 1),
    ('PROD004', 'Café molido', 85.75, 1),
    ('PROD005', 'Pasta de trigo', 25.99, 1)
GO
-- Procedimientos almacenados

-- Procedimientos almacenadoos para productos
-- Crear un producto
CREATE PROCEDURE CrearProducto
    @Codigo VARCHAR(50),
    @Descripcion VARCHAR(100),
    @Precio DECIMAL(10, 2),
    @ImpuestoId INT
AS
BEGIN
    INSERT INTO Productos (Codigo, Descripcion, Precio, ImpuestoId)
    VALUES (@Codigo, @Descripcion, @Precio, @ImpuestoId)
END
GO

-- Obtener todos los productos
CREATE PROCEDURE ObtenerProductos
AS
BEGIN
    SELECT Prod.Id, Prod.Codigo, Prod.Descripcion, Prod.Precio, Imp.Nombre FROM Productos Prod
	INNER JOIN Impuestos Imp ON Prod.ImpuestoId = Imp.Id
END
GO

-- Obtener producto por id
CREATE PROCEDURE ObtenerProductoPorId
    @Id INT
AS
BEGIN
    SELECT * FROM Productos WHERE Id = @Id
END
GO

-- Actualizar Producto
CREATE PROCEDURE ActualizarProducto
    @Id INT,
    @Codigo VARCHAR(50),
    @Descripcion VARCHAR(100),
    @Precio DECIMAL(10, 2),
    @ImpuestoId INT
AS
BEGIN
    UPDATE Productos
    SET Codigo = @Codigo,
        Descripcion = @Descripcion,
        Precio = @Precio,
        ImpuestoId = @ImpuestoId
    WHERE Id = @Id
END
GO

-- Eliminar Producto
CREATE PROCEDURE EliminarProducto
    @Id INT
AS
BEGIN
    DELETE FROM Productos WHERE Id = @Id
END
GO