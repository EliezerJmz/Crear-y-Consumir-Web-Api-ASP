
--	ADJUNTAR LA BASE DE DATOS EMPRESA
-- se debe dar permisos de control total al usuario para usar los .mdf y .ldf archivo en el sistema operativo
				--click derecho sobre el EMPRESA.mdf----propiedades---seguridad--usuario--editar--control total
	
--NOTA 
--Si no tenemos el archivo .ldf lo podemos omitir
		USE master
		GO
		CREATE DATABASE UsuariosDB
		ON  (FILENAME = 'C:\Users\Ego\Documents\ARCHIVOS HP SYNC\CURSO ASP.NET C#\CONSUMIR API\Crear y Consumir Web Api ASP\UsuariosDB.mdf')
			--(FILENAME = 'C:\Users\Ego\Documents\ARCHIVOS HP SYNC\CURSO ASP.NET C#\CONSUMIR API\Crear y Consumir Web Api ASP\UsuariosDB_log.ldf')
		FOR ATTACH