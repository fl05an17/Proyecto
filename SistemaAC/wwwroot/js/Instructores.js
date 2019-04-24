var idInstructor = 0;
class Instructores {
    constructor() {

    }
    guardarInstructor(id, funcion, ...data) {
        var action = data[0];
        var response = new Array({
            apellidos: data[3], especialidad: data[1], documento: data[4], email: data[5],
            estado: data[7], nombres: data[2], id: id, telefono: data[6]
        });
        if (data[1] == "") {
            document.getElementById("Especialidad").focus()
        } else {
            if (data[2] == "") {
                document.getElementById("Nombre").focus()
            } else {
                if (data[3] == "") {
                    document.getElementById("Apellidos").focus()
                } else {
                    if (data[4] == "") {
                        document.getElementById("Documento").focus()
                    } else {
                        if (data[5] == "") {
                            document.getElementById("Email").focus()
                        } else {
                            if (data[6] == "") {
                                document.getElementById("Telefono").focus()
                            } else {
                                $.post(
                                    action,
                                    {
                                        response, funcion
                                    },
                                    (response) => {
                                        if ("1" == response[0].code) {
                                            this.restablecer();
                                        } else {
                                            document.getElementById("mensaje").innerHTML = "No se puede guardar el instructor";
                                        }
                                    }
                                );
                            }
                        }
                    }
                }
            }
        }
    }
    filtrarInstructores(numPagina, valor, order, action) {
        valor = (valor == "") ? "null" : valor;//Ternario
        $.post(
            action,
            { valor, numPagina, order },
            (response) => {
                $("#resultSearch").html(response[0][0]);
                $("#paginado").html(response[0][1]);
            });
    }
    getInstructor(id, funcion, action) {
        $.post(
            action,
            {
                id
            },
            (response) => {
                console.log(response);
                if (funcion == 1) {
                    idInstructor = response[0].id;
                    document.getElementById("Especialidad").value = response[0].especialidad;
                    document.getElementById("Nombre").value = response[0].nombres;
                    document.getElementById("Apellidos").value = response[0].apellidos;
                    document.getElementById("Documento").value = response[0].documento;
                    document.getElementById("Email").value = response[0].email;
                    document.getElementById("Telefono").value = response[0].telefono;
                    document.getElementById("Estado").checked = response[0].estado;
                }
                var action = 'Instructores/guardarInstructor';
                this.editarInstructor(response, funcion, action)
            });
    }
    editarInstructor(response, funcion, action) {
        $.post(
            action,
            {
                response, funcion
            },
            (response) => {
                if (funcion == 0) {
                    this.restablecer();
                }
                console.log(response);

            });
    } 
    deleteInstructor(id, action) {
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
        document.getElementById("Especialidad").value = "";
        document.getElementById("Nombre").value = "";
        document.getElementById("Apellidos").value = "";
        document.getElementById("Documento").value = "";
        document.getElementById("Email").value = "";
        document.getElementById("Telefono").value = "";
        document.getElementById("Estado").checked = false;
        filtrarInstructores(1, "nombre");
        $('#modalAS').modal('hide');
        $('#ModalDeleteAS').modal('hide');
    }
}
