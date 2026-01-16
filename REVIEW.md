<div align="center">
📋 Revisor

Adonis Mercado Hidalgo
Matrícula: 2023-0604

</div>

# ⚙️ Funcionamiento del Programa

Al iniciar el sistema, es necesario registrarse para poder acceder al inventario.
El formulario de registro funciona correctamente, permitiendo crear nuevos usuarios sin inconvenientes.
Una vez creada la cuenta, el sistema inicia sesión automáticamente, lo que mejora la experiencia del usuario.

Con la sesión iniciada, se puede acceder a los registros de productos y entradas sin problemas.

# 📦 Productos

El registro de productos muestra correctamente la siguiente información:

- ID

- Descripción

- Costo

- Precio

- Existencia

## ✔️ Funcionalidades

- Se permite crear nuevos productos.

- La validación de datos funciona correctamente:

- Descripción, costo y precio son obligatorios.

Al guardar un producto:

- Se crea correctamente.

- La existencia se inicializa en 0.

## ✏️ Edición

- No permite guardar si algún campo está vacío (muy correcto).

- Modifica correctamente los campos editados.

## 🗑️ Eliminación

Al intentar eliminar un producto:

- Aparece un card descriptivo con toda la información.

- Solicita confirmación antes de eliminar.

Al confirmar, el producto se elimina correctamente.

## 🔍 Filtro

El filtro del registro de productos funciona correctamente, permitiendo filtrar por:

- ID

- Descripción

- Costo

- Precio

- Existencia

# 📥 Entradas

El registro de entradas muestra:

- ID

- Fecha

- Concepto

- Total de la entrada

## ✔️ Creación y validación

- Se pueden crear nuevas entradas.

Validaciones correctas:

- El concepto y el detalle son obligatorios.

- El botón Guardar se deshabilita hasta que se agregue un producto.

No permite guardar:

- Si no hay producto seleccionado.

- Si la cantidad o el costo están vacíos.

Esto es una muy buena práctica, ya que guía al usuario a completar correctamente la entrada.

## ✏️ Edición

Permite:

- Agregar productos.

- Remover productos.

No permite editar:

- Cantidad o costo de un producto ya guardado.

> Esto puede resultar incómodo, ya que obliga a eliminar el producto y volver a ingresarlo.
> Aun así, la edición funciona correctamente y guarda los cambios sin errores.

## 🗑️ Eliminación

- Muestra un card descriptivo con todos los datos de la entrada.

- Solicita confirmación antes de eliminar.

Al confirmar, elimina la entrada correctamente.

## 🔍 Filtro

Funciona correctamente, permitiendo filtrar por:

- Rango de fechas

- ID

- Concepto

# 🧠 Lógica del Inventario

## ➕ Agregar

El método Agregar en productos y entradas funciona correctamente.

Valida:

- Campos obligatorios vacíos.

- Caracteres no permitidos.

Al agregar una entrada:

- Se suma la existencia de los productos incluidos.

> Cumple con los estándares requeridos para la aplicación.

## ✏️ Editar

### Productos

- Valida campos obligatorios.

- No permite caracteres inválidos.

- Edita correctamente los datos.

### Entradas

Funciona correctamente, pero tiene un punto a mejorar:

- No permite editar cantidad o costo de un producto ya ingresado.

> Obliga a eliminar y volver a agregar el producto.

Aun así:

- Edita correctamente la entrada.

- Ajusta la existencia según los cambios realizados.

## 🗑️ Eliminar

El método Eliminar funciona correctamente en productos y entradas.

- Solicita confirmación al usuario.

- Hace la aplicación más segura.

- Ajusta correctamente la existencia:

- Resta la cantidad eliminada del inventario.

# ✅ Conclusión

> En general, es una aplicación muy bien codificada por parte del compañero Ronnel, cumpliendo con todos los estándares solicitados por el maestro.

La aplicación es:

- ✔️ Bien estructurada

- ✔️ Fácil de usar

- ✔️ Intuitiva

- ✔️ Funcional

> Demuestra un buen dominio de la lógica de inventario y una correcta validación de datos.
