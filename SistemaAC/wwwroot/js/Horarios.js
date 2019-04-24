var promesa = new Promise((resolve, reject) => {

});
class Horarios {
    constructor(dia, hora, actividad, action) {
        this.dia = dia;
        this.hora = hora;
        this.actividad = actividad;
        this.action = action;
    }
    getActividades(id, funcion) {
        var action = this.action;
        var count = 1;
        $.ajax({
            type: "POST",
            url: action,
            data: {},
            success: (response) => {
                //console.log(response);
                document.getElementById('ActividadHorarios').options[0] = new Option("Seleccione un curso", 0);
                if (0 < response.length) {
                    for (var i = 0; i < response.length; i++) {
                        if (0 == funcion) {
                            document.getElementById('ActividadHorarios').options[count] = new Option(response[i].nombre, response[i].actividadesID);
                            count++;
                        } else {
                            if (id == response[i].actividadesID) {
                                document.getElementById('ActividadHorarios').options[0] = new Option(response[i].nombre, response[i].actividadesID);
                                document.getElementById('ActividadHorarios').selectedIndex = 0;
                                break;
                            }
                        }
                        
                    }
                }
            }
        });
    }
    agregarHorario(id, funcion) {
        if (this.dia == "") {
            document.getElementById("Dia").focus();
        } else {
            if (this.hora == "") {
                document.getElementById("Hora").focus();
            } else {
                if (this.actividad == "0") {
                    document.getElementById("mensaje").innerHTML = "Seleccione una actividad";
                } else {
                    var dia = this.dia;
                    var hora = this.hora;
                    var actividad = this.actividad;
                    var action = this.action;
                    //console.log(dia);
                    $.ajax({
                        type: "POST",
                        url: action,
                        data: {
                            id, dia, hora, actividad, funcion
                        },
                        success: (response) => {
                            if ("Save" == response[0].code) {
                                this.restablecer();
                            } else {
                                document.getElementById("mensaje").innerHTML = "No se puede guardar el curso";
                            }
                        }
                    });
                }
            }
        }
    }
    filtrarHorario(numPagina, order) {
        var valor = this.dia;
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
    getHorario(id, funcion) {
        var action = this.action;
        $.ajax({
            type: "POST",
            url: action,
            data: { id },
            success: (response) => {
                console.log(response);
                if (funcion == 0) {
                    promesa = Promise.resolve({
                        id: response[0].horarioID,
                        dia: response[0].dia,
                        hora: response[0].hora,
                        actividad: response[0].actividadesID
                    });
                } else {
                    document.getElementById("Dia").value = response[0].dia;
                    document.getElementById("Hora").value = response[0].hora;
                    getActividades(response[0].actividadesID, 1);
                }
            }
        });
    }
    editarHorarios(id, funcion) {
        var dia, hora, actividad;
        var action = this.action;
        promesa.then(data => {
            //id = data.id;
            dia = data.dia;
            hora = data.hora;
            actividad = data.actividadesID;
            $.ajax({
                type: "POST",
                url: action,
                data: { id, dia, hora, actividad, funcion },
                success: (response) => {
                    if (response[0].code == "Save") {
                        this.restablecer();
                    } else {
                        document.getElementById("titleHorario").innerHTML = response[0].description;
                    }
                }
            });
        });
    }
    deleteHorario(id, action) {
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
        document.getElementById("Dia").value = "";
        document.getElementById("Hora").value = "";
        document.getElementById('ActividadHorarios').selectedIndex = 0;
        document.getElementById("mensaje").innerHTML = "";
        filtrarHorario(1, "dia");
        $('#modalCS').modal('hide');
        $('#ModalHorario').modal('hide');
        $('#ModalDeleteCS').modal('hide');
    }
}