
class Tarifas {
    constructor(valorEst, valorEmp, valorFam, valorGrad, actividad, action) {
        this.valorEst = valorEst;
        this.valorEmp = valorEmp;
        this.valorFam = valorFam;
        this.valorGrad = valorGrad;
        this.actividad = actividad;
        this.action = action;
    }
    getActividadesT(id, funcion) {
        var action = this.action;
        var count = 1;
        $.ajax({
            type: "POST",
            url: action,
            data: {},
            success: (response) => {
                //console.log(response);
                document.getElementById('ActividadTarifas').options[0] = new Option("Seleccione un curso", 0);
                if (0 < response.length) {
                    for (var i = 0; i < response.length; i++) {
                        if (0 == funcion) {
                            document.getElementById('ActividadTarifas').options[count] = new Option(response[i].nombre, response[i].actividadesID);
                            count++;
                        } else {
                            if (id == response[i].categoriaID) {
                                document.getElementById('ActividadTarifas').options[0] = new Option(response[i].nombre, response[i].actividadesID);
                                document.getElementById('ActividadTarifas').selectedIndex = 0;
                                break;
                            }
                        }

                    }
                }
            }
        });
    }
    agregarTarifa(id, funcion) {
        if (this.valorEst == "") {
            document.getElementById("ValorEst").focus();
        } else {
            if (this.valorEmp == "") {
                document.getElementById("ValorEmp").focus();
            } else {
                if (this.valorFam == "") {
                    document.getElementById("ValorFam").focus();
                } else {
                    if (this.valorGrad == "") {
                        document.getElementById("ValorGrad").focus();
                    } else {
                            if (this.actividad == "0") {
                                document.getElementById("mensaje").innerHTML = "Seleccione un curso";
                            } else {
                                var valorEst = this.valorEst;
                                var valorEmp = this.valorEmp;
                                var valorFam = this.valorFam;
                                var valorGrad = this.valorGrad;
                                var actividad = this.actividad;
                                var action = this.action;
                                //console.log(nombre);
                                $.ajax({
                                    type: "POST",
                                    url: action,
                                    data: {
                                        id, valorEst, valorEmp, valorFam, valorGrad, actividad, funcion
                                    },
                                    success: (response) => {
                                        if ("Save" == response[0].code) {
                                            this.restablecer();
                                        } else {
                                            document.getElementById("mensaje").innerHTML = "No se puede guardar la tarifa";
                                        }
                                    }
                                });
                            }
                        
                    }
                }
            }
        }
    }
    filtrarTarifa(numPagina, order) {
        var valor = this.valorEst;
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
    getTarifas(id, funcion) {
        var action = this.action;
        $.ajax({
            type: "POST",
            url: action,
            data: { id },
            success: (response) => {
                console.log(response);
                if (funcion == 0) {
                    promesa = Promise.resolve({
                        id: response[0].tarifaID,
                        valorEst: response[0].valorEst,
                        valorEmp: response[0].valorEmp,
                        valorFam: response[0].valorFam,
                        valorGrad: response[0].valorGrad,
                        actividad: response[0].actividadesID
                    });
                } else {
                    document.getElementById("ValorEst").value = response[0].valorEst;
                    document.getElementById("ValorEmp").value = response[0].valorEmp;
                    document.getElementById("ValorFam").value = response[0].valorFam;
                    document.getElementById("ValorGrad").value = response[0].valorGrad;
                    getActividadesT(response[0].actividadesID, 1);
                }
            }
        });
    }
    editarTarifa(id, funcion) {
        var valorEst, valorEmp, valorFam, valorGrad, actividad;
        var action = this.action;
        promesa.then(data => {
            // id = data.id;
            valorEst = data.valorEst
            valorEmp = data.valorEmp;
            valorFam = data.valorFam;
            valorGrad = data.valorGrad;
            actividad = data.actividadesID;
            $.ajax({
                type: "POST",
                url: action,
                data: { id, valorEst, valorEmp, valorFam, valorGrad, actividad, funcion },
                success: (response) => {
                    if (response[0].code == "Save") {
                        this.restablecer();
                    } else {
                        document.getElementById("titleTarifa").innerHTML = response[0].description;
                    }
                }
            });
        });
    }
    deleteTarifa(id, action) {
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
        document.getElementById("ValorEst").value = "";
        document.getElementById("ValorEmp").value = "";
        document.getElementById("ValorFam").value = "";
        document.getElementById("ValorGrad").value = "";
        document.getElementById('ActividadTarifas').selectedIndex = 0;
        document.getElementById("mensaje").innerHTML = "";
        filtrarTarifa(1, "valorEst");
        $('#modalES').modal('hide');
        $('#ModalTarifas').modal('hide');
        $('#ModalDeleteES').modal('hide');
    }
}
