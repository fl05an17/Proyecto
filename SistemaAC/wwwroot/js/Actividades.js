var localStorage = window.localStorage;
class Actividades {
    constructor(nombre, cantidad, descripcion, estado, action) {
        this.nombre = nombre;
        this.cantidad = cantidad;
        this.descripcion = descripcion;
        this.estado = estado;
        this.action = action
    }
    agregarActividad(id, funcion) {
        if (this.nombre == "") {
            document.getElementById("Nombre").focus();
        } else {
            if (this.cantidad == "") {
                document.getElementById("Cantidad").focus();
            } else {
                if (this.descripcion == "") {
                    document.getElementById("Descripcion").focus();
                } else {
                    if (this.estado == "0") {
                        document.getElementById("mensaje").innerHTML = "Seleccione un estado";
                    } else {
                        var nombre = this.nombre;
                        var cantidad = this.cantidad;
                        var descripcion = this.descripcion;
                        var estado = this.estado;
                        var action = this.action;
                        var mensaje = '';
                        $.ajax({
                            type: "POST",
                            url: action,
                            data: { id, nombre, cantidad, descripcion, estado, funcion },
                            success: (response) => {
                                $.each(response, (index, val) => {
                                    mensaje = val.code;

                                });
                                if (mensaje == "Save") {
                                    this.restablecer();
                                } else {
                                    document.getElementById("mensaje").innerHTML = "No se puede guardar la actividad";
                                }
                                //console.log(response);
                            }
                        });
                    }
                }
            }
        }
    }
    filtrarDatos(numPagina, order) {
        var valor = this.nombre;
        var action = this.action;
        if (valor == "") {
            valor = "null";
        }
        $.ajax({
            type: "POST",
            url: action,
            data: { valor, numPagina, order },
            success: (response) => {
                console.log(response);
                $.each(response, (index, val) => {

                    $("#resultSearch").html(response[0][0]);
                    $("#paginado").html(response[0][1]);
                });

            }
        });
    }
    qetActividad(id, funcion) {
        var action = this.action;
        $.ajax({
            type: "POST",
            url: action,
            data: { id },
            success: (response) => {
                console.log(response);
                if (funcion == 0) {
                    if (response[0].estado) {
                        document.getElementById("titleActividad").innerHTML = "Esta seguro de desactivar la actividad " + response[0].nombre;
                    } else {
                        document.getElementById("titleActividad").innerHTML = "Esta seguro de activar la actividad " + response[0].nombre;
                    }
                } else {
                    document.getElementById("Nombre").value = response[0].nombre;
                    document.getElementById("Cantidad").value = response[0].cantidad;
                    document.getElementById("Descripcion").value = response[0].descripcion;
                    if (response[0].estado) {
                        document.getElementById("Estado").selectedIndex = 1;
                    } else {
                        document.getElementById("Estado").selectedIndex = 2;
                    }
                }
                localStorage.setItem("actividad", JSON.stringify(response));
            }
        });
    }
    editarActividad(id, funcion) {
        var action = this.action;
        var response = JSON.parse(localStorage.getItem("actividad"));
        var nombre = response[0].nombre;
        var cantidad = response[0].cantidad;
        var descripcion = response[0].descripcion;
        var estado = response[0].estado;
        localStorage.removeItem("actividad");
        $.ajax({
            type: "POST",
            url: action,
            data: { id, nombre, cantidad, descripcion, estado, funcion },
            success: (response) => {
                console.log(response);
                this.restablecer();
            }
        });
    }
    restablecer() {
        document.getElementById("Nombre").value = "";
        document.getElementById("Cantidad").value = "";
        document.getElementById("Descripcion").value = "";
        document.getElementById("mensaje").value = "";
        document.getElementById("Estado").selectedIndex = 0;
        $('#modalAC').modal('hide');
        $('#ModaEstado').modal('hide');
        filtrarDatos(1, "nombre");
    }
}