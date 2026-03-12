Objetivo del sistema
Un sistema que permita a usuarios (brokers y clientes) gestionar pólizas de seguros,
desde cotización hasta consulta, con funcionalidades de autenticación.
Con un esquema desacoplado, en Back End donde la lógica de login y gestión de
pólizas será desarrollada en este Back End.
En Fron End, solo vistas y conexión a API. La API es la que tendrá conexión a la base de
datos.

🧩Módulos Principales
1. Autenticación y Seguridad
  a. Login / Logout
  b. Cambio de contraseña
  c. Roles: Admin, Broker, Cliente
  d. Permisos por rol
    i. Admin: Acceso a todo el sistema
    ii. Broker: Cotizar y visualizar todos los clientes
    iii. Cliente: Cotizar y visualizar información propia (solo de él)

2. Gestión de Pólizas
  a. Cotizar póliza con el modelo de datos siguiente:
    i. Numero de póliza
    ii. Tipo de póliza tipo catalogo (vida, auto, hogar, salud)
    iii. Cliente (Nombre, Apellido Materno, Apellido Paterno, Edad, País
          de nacimiento, Gerero, correo electrónico, teléfono)
    iv. Fecha inicio de la poliza / Fecha fin (calcular fecha mensual al día
        en que se está creando)
    v. Monto de la prima
  b. Modificar póliza con los estatus:
    i. Cotizada,
    ii. Autorizada,
    iii. Rechazada
  c. Consultar póliza
🖥️Pantallas/Interfaces
1. Login
2. Formulario de Cotización
3. Lista de Pólizas
4. Editor de Póliza
5. Detalle de Póliza
