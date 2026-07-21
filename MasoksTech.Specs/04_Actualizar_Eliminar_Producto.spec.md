# Spec Kit: Actualizar y Eliminar Producto

## 1. Metadatos
* **Módulo:** Inventario
* **Autor:** MASOKS TECH Team

## 2. Especificación Técnica
* **Endpoints:** `PUT /api/productos/{id}` y `DELETE /api/productos/{id}`
* **Respuestas Esperadas:**
  * **204 No Content:** Cuando la actualización o eliminación es exitosa (es el estándar HTTP para éxito sin retornar datos).
  * **404 Not Found:** Si el producto a modificar/eliminar no existe.
  * **400 Bad Request:** Si en el PUT, el ID de la URL no coincide con el del JSON.

## 3. Criterios de Aceptación
**Escenario 1: Actualización Exitosa**
* **Dado** que existe un producto
* **Cuando** el administrador envía datos modificados
* **Entonces** el sistema actualiza la base de datos y retorna 204 No Content.

**Escenario 2: Eliminación Exitosa**
* **Dado** que existe un producto
* **Cuando** el administrador solicita eliminarlo por su ID
* **Entonces** el sistema lo borra de la base de datos y retorna 204 No Content.