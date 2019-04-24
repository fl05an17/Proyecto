var promesa = new Promise((resolve, reject) => {

});
class Maquinaria {
    constructor(nombre, cantidad, actividad, action) {
        this.nombre = nombre;
        this.cantidad = cantidad;
        this.actividad = actividad;
        this.action = action;
    }
    getActividadesM(id, funcion) {
        var action = this.action;
        var count = 1;
        $.ajax({
            type: "POST",
            url: action,
            data: {},
            success: (response) => {
                //console.log(response);
                document.getElementById('ActividadMaquinarias').options[0] = new Option("Seleccione una actividad", 0);
                if (0 < response.length) {
                    for (var i = 0; i < response.length; i++) {
                        if (0 == funcion) {
                            document.getElementById('ActividadMaquinarias').options[count] = new Option(response[i].nombre, response[i].actividadesID);
                            count++;
                        } else {
                            if (id == response[i].categoriaID) {
                                document.getElementById('ActividadMaquinarias').options[0] = new Option(response[i].nombre, response[i].actividadesID);
                                document.getElementById('ActividadMaquinarias').selectedIndex = 0;
                                break;
                            }
                        }

                    }
                }
            }
        });
    }
    agregarMaquinaria(id, funcion) {
        if (this.nombre == "") {
            document.getElementById("Nombre").focus();
        } else {
            if (this.cantidad == "") {
                document.getElementById("Cantidad").focus();
            } else {
                if (this.actividad == "0") {
                    document.getElementById("mensaje").innerHTML = "Seleccione una actividad";
                } else {
                    var nombre = this.nombre;
                    var cantidad = this.cantidad;
                    var actividad = this.actividad;
                    var action = this.action;
                    //console.log(nombre);
                    $.ajax({
                        type: "POST",
                        url: action,
                        data: {
                            id, nombre, cantidad, actividad, funcion
                        },
                        success: (response) => {
                            if ("Save" == response[0].code) {
                                this.restablecer();
                            } else {
                                document.getElementById("mensaje").innerHTML = "No se puede guardar la maquina";
                            }
                        }
                    });
                }
            }
        }
    }
    filtrarMaquinaria(numPagina, order) {
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
                $("#resultSearch").html(response[0][0]);
                $("#paginado").html(response[0][1]);
            }
        });
    }
    getMaquinaria(id, funcion) {
        var action = this.action;
        $.ajax({
            type: "POST",
            url: action,
            data: { id },
            success: (response) => {
                console.log(response);
                if (funcion == 0) {
                    promesa = Promise.resolve({
                        id: response[0].maquinariasID,
                        nombre: response[0].nombre,
                        cantidad: response[0].cantidad,
                        actividad: response[0].actividadesID
                    });
                } else {
                    document.getElementById("Nombre").value = response[0].nombre;
                    document.getElementById("Cantidad").value = response[0].cantidad;
                    getActividades(response[0].actividadesID, 1);
                }
            }
        });
    }
    editarMaquinaria(id, funcion) {
        var nombre, cantidad, actividad;
        var action = this.action;
        promesa.then(data => {
            // id = data.id;
            nombre = data.nombre
            cantidad = data.cantidad;
            actividad = data.actividadesID;
            $.ajax({
                type: "POST",
                url: action,
                data: { id, nombre, cantidad, actividad, funcion },
                success: (response) => {
                    if (response[0].code == "Save") {
                        this.restablecer();
                    } else {
                        document.getElementById("titleMaquinaria").innerHTML = response[0].description;
                    }
                }
            });
        });
    }
    deleteMaquinaria(id, action) {
        $.post(
            action,
            {
                id
            },
            (response) => {

                console.log(response);
                this.restablecer();
            });
    }
    restablecer() {
        document.getElementById("Nombre").value = "";
        document.getElementById("Cantidad").value = "";
        document.getElementById('ActividadMaquinarias').selectedIndex = 0;
        document.getElementById("mensaje").innerHTML = "";
        filtrarMaquinaria(1, "nombre");
        $('#modalDS').modal('hide');
        $('#ModalMaquinaria').modal('hide');
        $('#ModalDeleteDS').modal('hide');
    }
}
