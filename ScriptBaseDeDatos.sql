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
-- Insertar  impuestos
INSERT INTO Impuestos (Nombre, Porcentaje)
VALUES
    ('Impuesto 15%', 0.15),
    ('Impuesto 12%', 0.12),
	('Impuesto 18%', 0.18)

-- Insertar productos
INSERT INTO Productos (Codigo, Descripcion, Precio, ImpuestoId)
VALUES
    ('PROD001', 'Arroz blanco', 45.99, 1),
    ('PROD002', 'Aceite de oliva', 120.50, 1),
    ('PROD003', 'Leche descremada', 28.99, 1),
    ('PROD004', 'Café molido', 85.75, 1),
    ('PROD005', 'Pasta de trigo', 25.99, 1)
GO

-- Insertar clientes
INSERT INTO Clientes (Codigo, Nombre, Sexo, RTN, Direccion) 
VALUES 	
	('C001', 'Juan Pérez', 'M', '123456789', 'Calle A, Ciudad'),
	('C002', 'María López', 'F', '987654321', 'Avenida B, Ciudad'),
	('C003', 'Pedro Gómez', 'M', '456789123', 'Calle C, Ciudad'),
	('C004', 'Ana Torres', 'F', '321654987', 'Avenida D, Ciudad'),
	('C005', 'Luisa Martínez', 'F', '654789321', 'Calle E, Ciudad');
GO

-- Insertar facturas
-- Insertar facturas
INSERT INTO Facturas (ClienteId, FechaFactura, Total, Estado)
VALUES
    (1, '2023-07-01', 0, 'Pendiente');

-- Insertar detalles de factura para la primera factura
DECLARE @FacturaId1 INT;
SET @FacturaId1 = SCOPE_IDENTITY();

INSERT INTO DetalleFactura (FacturaId, ProductoId, Cantidad, PrecioUnitario, Total)
VALUES
    (@FacturaId1, 1, 2, (SELECT Precio * (1 + Impuestos.Porcentaje) FROM Productos INNER JOIN Impuestos ON Productos.ImpuestoId = Impuestos.Id WHERE Productos.Id = 1), 0),
    (@FacturaId1, 2, 1, (SELECT Precio * (1 + Impuestos.Porcentaje) FROM Productos INNER JOIN Impuestos ON Productos.ImpuestoId = Impuestos.Id WHERE Productos.Id = 2), 0);

-- Insertar la segunda factura
INSERT INTO Facturas (ClienteId, FechaFactura, Total, Estado)
VALUES
    (2, '2023-07-01', 0, 'Pendiente');

-- Insertar detalles de factura para la segunda factura
DECLARE @FacturaId2 INT;
SET @FacturaId2 = SCOPE_IDENTITY();

INSERT INTO DetalleFactura (FacturaId, ProductoId, Cantidad, PrecioUnitario, Total)
VALUES
    (@FacturaId2, 3, 3, (SELECT Precio * (1 + Impuestos.Porcentaje) FROM Productos INNER JOIN Impuestos ON Productos.ImpuestoId = Impuestos.Id WHERE Productos.Id = 3), 0),
    (@FacturaId2, 4, 2, (SELECT Precio * (1 + Impuestos.Porcentaje) FROM Productos INNER JOIN Impuestos ON Productos.ImpuestoId = Impuestos.Id WHERE Productos.Id = 4), 0);
GO



-- Procedimientos almacenados

-- Procedimientos almacenados para productos
-- Crear un producto
CREATE PROCEDURE CrearProducto
    @Codigo VARCHAR(50),
    @Descripcion VARCHAR(100),
    @Precio DECIMAL(10, 2),
    @Impuesto VARCHAR(50)
AS
BEGIN
    INSERT INTO Productos (Codigo, Descripcion, Precio, ImpuestoId)
    VALUES (@Codigo, @Descripcion, @Precio, (SELECT Id FROM Impuestos WHERE Nombre = @Impuesto))
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

-- Actualizar Producto
CREATE PROCEDURE ActualizarProducto
    @Id INT,
    @Codigo VARCHAR(50),
    @Descripcion VARCHAR(100),
    @Precio DECIMAL(10, 2),
    @Impuesto VARCHAR(50)
AS
BEGIN
    UPDATE Productos
    SET Codigo = @Codigo,
        Descripcion = @Descripcion,
        Precio = @Precio,
        ImpuestoId = (SELECT Id FROM Impuestos WHERE Nombre = @Impuesto)
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

-- Cargar impuestos
CREATE PROCEDURE CargarImpuestos
AS
BEGIN
    SELECT Nombre FROM Impuestos
END
GO


-- Procedimientos almacenados para clientes
-- Insertar un cliente
CREATE PROCEDURE CrearCliente
    @Codigo VARCHAR(50),
    @Nombre VARCHAR(100),
    @Sexo VARCHAR(10),
    @RTN VARCHAR(14),
    @Direccion VARCHAR(200)
AS
BEGIN
    INSERT INTO Clientes (Codigo, Nombre, Sexo, RTN, Direccion)
    VALUES (@Codigo, @Nombre, @Sexo, @RTN, @Direccion)
END
GO

-- Actualizar un cliente
CREATE PROCEDURE ActualizarCliente
    @Id INT,
    @Codigo VARCHAR(50),
    @Nombre VARCHAR(100),
    @Sexo VARCHAR(10),
    @RTN VARCHAR(14),
    @Direccion VARCHAR(200)
AS
BEGIN
    UPDATE Clientes
    SET Codigo = @Codigo, Nombre = @Nombre, Sexo = @Sexo, RTN = @RTN, Direccion = @Direccion
    WHERE Id = @Id
END
GO

-- eliminar un cliente
CREATE PROCEDURE EliminarCliente
    @Id INT
AS
BEGIN
    DELETE FROM Clientes
    WHERE Id = @Id
END
GO

-- obtener todos los clientes
CREATE PROCEDURE ObtenerClientes
AS
BEGIN
    SELECT * FROM Clientes
END
GO

-- Procedimientos almacenados para Facturas
-- Procedimiento almacenado para obtener todas las facturas
CREATE PROCEDURE ObtenerFacturas
AS
BEGIN
    SELECT * FROM Facturas
END
GO

-- Obtener una factura por su ID
CREATE PROCEDURE ObtenerFacturaPorId
    @FacturaId INT
AS
BEGIN
    SELECT * FROM Facturas WHERE Id = @FacturaId
END
GO

-- Insertar una nueva factura
CREATE PROCEDURE InsertarFactura
    @ClienteId INT,
    @FechaFactura DATE,
    @Total DECIMAL(10, 2),
    @Estado VARCHAR(20)
AS
BEGIN
    INSERT INTO Facturas (ClienteId, FechaFactura, Total, Estado)
    VALUES (@ClienteId, @FechaFactura, @Total, @Estado)
END
GO

-- Actualizar una factura existente
CREATE PROCEDURE ActualizarFactura
    @FacturaId INT,
    @ClienteId INT,
    @FechaFactura DATE,
    @Total DECIMAL(10, 2),
    @Estado VARCHAR(20)
AS
BEGIN
    UPDATE Facturas
    SET ClienteId = @ClienteId,
        FechaFactura = @FechaFactura,
        Total = @Total,
        Estado = @Estado
    WHERE Id = @FacturaId
END
GO

-- Eliminar una factura
CREATE PROCEDURE EliminarFactura
    @FacturaId INT
AS
BEGIN
    
    -- Eliminar registros de DetalleFactura relacionados con la factura
    DELETE FROM DetalleFactura WHERE FacturaId = @FacturaId;

    -- Eliminar la factura
    DELETE FROM Facturas WHERE Id = @FacturaId;
END
GO

-- Procedimientos almacenados para el detalle de la factura
-- Insertar un detalle de factura
CREATE PROCEDURE InsertarDetalleFactura
    @FacturaId INT,
    @ProductoId INT,
    @Cantidad INT,
    @Total DECIMAL(10, 2)
AS
BEGIN
    DECLARE @PrecioUnitario DECIMAL(10, 2);
    SET @PrecioUnitario = (SELECT Precio * (1 + Impuestos.Porcentaje) FROM Productos INNER JOIN Impuestos ON Productos.ImpuestoId = Impuestos.Id WHERE Productos.Id = @ProductoId);

    INSERT INTO DetalleFactura (FacturaId, ProductoId, Cantidad, PrecioUnitario, Total)
    VALUES (@FacturaId, @ProductoId, @Cantidad, @PrecioUnitario, @Total)
END
GO

-- Actualizar un detalle de factura
CREATE PROCEDURE ActualizarDetalleFactura
    @Id INT,
    @FacturaId INT,
    @ProductoId INT,
    @Cantidad INT,
    @Total DECIMAL(10, 2)
AS
BEGIN
    DECLARE @PrecioUnitario DECIMAL(10, 2);
    SET @PrecioUnitario = (SELECT Precio * (1 + Impuestos.Porcentaje) FROM Productos INNER JOIN Impuestos ON Productos.ImpuestoId = Impuestos.Id WHERE Productos.Id = @ProductoId);

    UPDATE DetalleFactura
    SET FacturaId = @FacturaId,
        ProductoId = @ProductoId,
        Cantidad = @Cantidad,
        PrecioUnitario = @PrecioUnitario,
        Total = @Total
    WHERE Id = @Id
END
GO


-- Eliminar un detalle de factura
CREATE PROCEDURE EliminarDetalleFactura
    @Id INT
AS
BEGIN
    DELETE FROM DetalleFactura
    WHERE Id = @Id
END
GO

-- Obtener todos los detalles de una factura
CREATE PROCEDURE ObtenerDetallesFactura
    @FacturaId INT
AS
BEGIN
    SELECT *
    FROM DetalleFactura
    WHERE FacturaId = @FacturaId
END
GO
