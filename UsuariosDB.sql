
	USE master ;  
	GO  
	CREATE DATABASE UsuariosDB
	ON   
	( NAME = UsuariosDB_dat,  
	    FILENAME = 'D:\Curso ASP.NET C#\API REST\Crear Y consumir Api Asp C#\UsuariosDB.mdf',
		SIZE = 10,  
		MAXSIZE = 50,  
		FILEGROWTH = 5 )  
	LOG ON  
	( NAME = UsuariosDB_log,  
		 FILENAME = 'D:\Curso ASP.NET C#\API REST\Crear Y consumir Api Asp C#\UsuariosDB_log.ldf',
		SIZE = 5MB,  
		MAXSIZE = 25MB,  
		FILEGROWTH = 5MB ) ;  
	GO 

	USE UsuariosDB
	GO
	CREATE TABLE usuario (
	int_id int primary key identity, 
	str_status varchar(50) NOT NULL,
	dat_fecha date,
	str_nombre varchar(50),
	str_direccion varchar(50),
	str_departamento varchar(50),
	int_telefono int
	);

	INSERT INTO usuario(str_status,
						dat_fecha,
						str_nombre,
						str_direccion,
						str_departamento,
						int_telefono)
	VALUES('Activo', '03/11/2020','Giovany','Zona 12 Guatemala', 'Guatemala', 11223344),
			('Activo', '01/11/2020','Eliezer','Zona 12 Guatemala', 'Guatemala', 22334455),
			('Activo', '02/11/2020','Matteo','Zona 12 Guatemala', 'Guatemala', 33223355),
			('Activo', '03/11/2020','Emiliano','Zona 21 Mixco', 'Guatemala', 99889977),
			('Activo', '20/10/2020','Luna','Zona 1 Guatemala', 'Mixco', 15523344),
			('Activo', '20/10/2020','Sol','Barrio 1', 'Quetzaltenago', 15523344),
			('Activo', '13/09/2020','Mar','Calle principal', 'Escuintla', 99223344),
			('Activo', '06/09/2020','Yova','Zona 12 Guatemala', 'Guatemala', 11223344),
			('Activo', '03/09/2020','Juanes','Zona 21 Guatemala', 'Guatemala', 77223344),
			('Activo', '21/08/2020','Martes','Zona 4 Guatemala', 'Guatemala', 19923344),
			('Activo', '04/08/2020','Lunes','Zona 3 Guatemala', 'Guatemala', 33883344),
			('Activo', '12/07/2020','Mercurio','Zona 2 Guatemala', 'Guatemala', 56893344),

--Impedir guardar cambios
	--Para cambiar la opción Impedir guardar cambios que requieren la creación de la tabla, siga estos pasos:

	--1.	Abra SQL Server Management Studio (SSMS).
	--2.	En el menú Herramientas, haga clic en Opciones.
	--3.	En el panel de navegación de la ventana de Opciones, haga clic en Diseñadores.
	--4.	Seleccione o desactive la casilla de verificación Impedir guardar cambios que requieren la recreación de la tabla y, a continuación, haga clic en Aceptar.
