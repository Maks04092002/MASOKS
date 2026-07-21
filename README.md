# 🚀 MASOKS TECH - Sistema Web para la Gestión de Ventas e Inventario

Plataforma web escalable desarrollada para optimizar y automatizar los procesos de compra, venta y control de inventario de accesorios y suministros de telefonía móvil.

---

## 🏛️ Arquitectura del Sistema

El proyecto está diseñado bajo los principios de **Clean Architecture** (Arquitectura Limpia), promoviendo la separación de responsabilidades, la independencia de frameworks y la mantenibilidad del código:

```text
MASOKS TECH ERP
│
├── 🧱 Core / Domain          # Entidades del Negocio (Productos, Usuarios, Ventas, Reglas)
├── ⚙️ Application           # Casos de Uso, DTOs, Interfaces de Servicios
├── 🔌 Infrastructure        # Persistencia de Datos, Entity Framework Core, PostgreSQL
└── 🌐 WebAPI / Presentation  # Controladores REST, Autenticación JWT, Swagger, Dashboard HTML/JS
📋 Metodología y Enfoque de Desarrollo
Metodología Ágil: Scrum (Gestión por Sprints, Product Backlog e Historias de Usuario).

Desarrollo Guiado por Especificaciones: Specification-Driven Development (SDD) mediante Spec Kit.

🛠️ Stack Tecnológico
Backend & API REST
Lenguaje & Framework: C# con ASP.NET Core 10 (.NET 10)

ORM: Entity Framework Core 10

Base de Datos: PostgreSQL

Seguridad: Autenticación y Autorización basada en JSON Web Tokens (JWT)

Documentación API: OpenAPI / Swagger

Pruebas: xUnit (Pruebas Unitarias y de Integración)

Frontend
Interfaz: HTML5, CSS3, JavaScript Vanilla (Fetch API / Asíncrono)

Framework Estilos: Bootstrap 5.3 + Bootstrap Icons

Tipografía: Plus Jakarta Sans

📋 Módulos Funcionales
📦 Gestión de Inventario:

Registro, consulta, actualización y eliminación de productos.

Monitoreo en tiempo real de stock disponible y precios.

👥 Gestión de Usuarios y Roles:

Registro de clientes y administradores.

Asignación de roles (Admin / Cliente) y control de acceso.

🧾 Módulo de Ventas e Historial:

Registro de órdenes de compra con estado de pago y métodos (Efectivo, Yape, Plin, Tarjeta).

Cálculo automatizado de ingresos y métricas globales (KPIs).

📁 Estructura General del Repositorio
Plaintext
MASOKS TECH/
├── src/
│   ├── MasoksTech.Domain/          # Entidades y objetos de valor
│   ├── MasoksTech.Application/     # Casos de uso e interfaces
│   ├── MasoksTech.Infrastructure/  # Acceso a base de datos PostgreSQL y EF Core
│   └── MasoksTech.API/             # Controladores API REST y Swagger
├── wwwroot/                        # Interfaz Frontend (Dashboard Admin, Login)
│   ├── admin.html
│   └── login.html
├── tests/                          # Pruebas unitarias e integración (xUnit)
└── README.md
