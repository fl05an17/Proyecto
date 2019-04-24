// Write your JavaScript code.
$('#modalEditar').on('shown.bs.modal', function () {
  $('#myInput').focus()
})
$('#modalAC').on('shown.bs.modal', function () {
    $('#Nombre').focus()
})
/** Codigo de Usuarios */
function getUsuario(id,action) {
    $.ajax({
        type: "POST",
        url: action,
        data: { id },
        success: function (response) {
            mostrarUsuario(response);
        }
    });
}

var items;
var j = 0;

var id;
var userName;
var email
var phoneNumber;
var role;
var selectRole;

var accessFailedCount;
var concurrencyStamp;
var emailConfirmed;
var lockoutEnabled;
var lockoutEnd;
var normalizedUserName;
var normalizedEmail;
var passwordHash;
var phoneNumberConfirmed;
var securityStamp;
var twoFactorEnabled;

function mostrarUsuario(response) {
    items = response;
    for (var i = 0; i < 3; i++) {
        var x = document.getElementById('Select');
        x.remove(i);
    }

    $.each(items, function (index, val) {
        $('input[name=Id]').val(val.id);
        $('input[name=UserName]').val(val.userName);
        $('input[name=Email]').val(val.email);
        $('input[name=PhoneNumber]').val(val.phoneNumber);
        document.getElementById('Select').options[0] = new Option(val.role, val.roleId);

        $("#dEmail").text(val.email);
        $("#dUserName").text(val.userName);
        $("#dPhoneNumber").text(val.phoneNumber);
        $("#dRole").text(val.role);

        $("#eUsuario").text(val.email);
        $('input[name=EIdUsuario]').val(val.id);
    });
}

function getRoles(action) {
    $.ajax({
        type: "POST",
        url: action,
        data: {},
        success: function (response) {
            if (j == 0) {
                for (var i = 0; i < response.length; i++) {
                    document.getElementById('Select').options[i] = new Option(response[i].text, response[i].value);
                    document.getElementById('SelectNuevo').options[i] = new Option(response[i].text, response[i].value);
                }
                j = 1;
            }
        }
    });
}

function editarUsuario(action) {
    id = $('input[name=Id]')[0].value;
    email = $('input[name=Email]')[0].value;
    phoneNumber = $('input[name=PhoneNumber]')[0].value;
    role = document.getElementById('Select');
    selectRole = role.options[role.selectedIndex].text;

    $.each(items, function (index, val) {
        accessFailedCount = val.accessFailedCount;
        concurrencyStamp = val.concurrencyStamp;
        emailConfirmed = val.emailConfirmed;
        lockoutEnabled = val.lockoutEnabled;
        lockoutEnd = val.lockoutEnd;
        userName = val.userName;
        normalizedUserName = val.normalizedUserName;
        normalizedEmail = val.normalizedEmail;
        passwordHash = val.passwordHash;
        phoneNumberConfirmed = val.phoneNumberConfirmed;
        securityStamp = val.securityStamp;
        twoFactorEnabled = val.twoFactorEnabled;
    });
    $.ajax({
        type: "POST",
        url: action,
        data: {
            id, userName, email, phoneNumber, accessFailedCount,
            concurrencyStamp, emailConfirmed,
            lockoutEnabled, lockoutEnd, normalizedEmail, normalizedUserName,
            passwordHash, phoneNumberConfirmed, securityStamp, twoFactorEnabled, selectRole
        },
        success: function (response) {
            if (response === "Save") {
                window.location.href = "Usuarios";
            }
            else {
                alert("No se puede editar los datos del Usuario");
            }
        }
    });

}

function ocultarDetalleUsuario() {
    $("#modalDetalle").modal("hide");
}

function eliminarUsuario(action) {
    var id = $('input[name=EIdUsuario]')[0].value;
    $.ajax({
        type: "POST",
        url: action,
        data: { id },
        success: function (response) {
            if (response == "Delete") {
                window.location.href = "Usuarios";
            }
            else {
                alert("No se puede eliminar el registro");
            }
        }
    });
}

function crearUsuario(action) {
    email = $('input[name=EmailNuevo]')[0].value;
    phoneNumber = $('input[name=PhoneNumberNuevo]')[0].value;
    passwordHash = $('input[name=PasswordHashNuevo]')[0].value;
    role = document.getElementById('SelectNuevo');
    selectRole = role.options[role.selectedIndex].text;

    if (email == "") {
        $('#EmailNuevo').focus();
        alert("Ingrese el email del usuario");
    } else {
        if (passwordHash == "") {
            $('#PasswordHashNuevo').focus();
            alert("Ingrese la contraseña del usuario");
        } else {
            $.ajax({
                type: "POST",
                url: action,
                data: {
                    email, phoneNumber, passwordHash, selectRole
                }, success: function (response) {
                    if (response == "Save") {
                        window.location.href = "Usuarios";
                    }
                    else {
                        $('#mensajenuevo').html("No se puede guardar el usuario. <br/>Seleccione un rol. <br/> Ingrese un email correcto. <br/> La contraseña debe tener de 6-100 caracteres, al menos un caracter especial, una letra mayúscula y un número");
                    }
                }
            });
        }
    }
}

$().ready(() => {
    var URLactual = window.location;
    document.getElementById("filtrar").focus();
    switch (URLactual.pathname) {
        case "/Actividades":
            filtrarDatos(1, "nombre");
            break;
        case "/Horarios":
            getActividades(0, 0);
            filtrarHorario(1, "dia");
            break;
        case "/Maquinarias":
            getActividadesM(0, 0);
            filtrarMaquinaria(1, "nombre");
            break;
        case "/Tarifas":
            getActividadesT(0, 0);
            filtrarTarifa(1, "valorEst");
            break;
        case "/Instructores":
            filtrarInstructores(1, "nombre");
            break;
    }
});
$('#modalCS').on('shown.bs.modal', function () {
    $('#Dia').focus()
})
$('#modalDS').on('shown.bs.modal', function () {
    $('#Nombre').focus()
})
$('#modalES').on('shown.bs.modal', function () {
    $('#ValorEst').focus()
})
$('#modalAS').on('shown.bs.modal', function () {
    $('#Especialidad').focus()
})

var idActividad, funcion = 0, idHorario, idMaquinaria, idTarifa;
var idInstructor = 0;
/** Codigo de Actividades */
var agregarActividad = () => {
    var nombre = document.getElementById("Nombre").value;
    var cantidad = document.getElementById("Cantidad").value;
    var descripcion = document.getElementById("Descripcion").value;
    var estados = document.getElementById('Estado');
    var estado = estados.options[estados.selectedIndex].value;
    if (funcion == 0) {
        var action = 'Actividades/guardarActividad';
    } else {
        var action = 'Actividades/editarActividad';
    }
    var actividad = new Actividades(nombre, cantidad, descripcion, estado, action);
    actividad.agregarActividad(idActividad, funcion);
    funcion = 0;
}

var filtrarDatos = (numPagina, order) => {
    var valor = document.getElementById("filtrar").value;
    var action = 'Actividades/filtrarDatos';
    var actividad = new Actividades(valor, "", "", "", action);
    actividad.filtrarDatos(numPagina, order);
}

var editarEstado = (id, fun) => {
    idActividad = id;
    funcion = fun;
    var action = 'Actividades/getActividades';
    var actividad = new Actividades("", "", "", "", action);
    actividad.qetActividad(id, funcion);
}

var editarActividad = () => {
    var action = 'Actividades/editarActividad';
    var actividad = new Actividades("", "", "", "", action);
    actividad.editarActividad(idActividad, funcion);
}

/** Codigo de Horarios */
var getActividades = (id, fun) => {
    var action = 'Horarios/getActividades';
    var horarios = new Horarios("", "", "", action);
    horarios.getActividades(id, fun);
}
var agregarHorario = () => {
    if (funcion == 0) {
        var action = 'Horarios/agregarHorario';
    } else {
        var action = 'Horarios/editarHorario';
    }
    var dia = document.getElementById("Dia").value;
    var hora = document.getElementById("Hora").value;
    var actividades = document.getElementById('ActividadHorarios');
    var actividad = actividades.options[actividades.selectedIndex].value;
    var horarios = new Horarios(dia, hora, actividad, action);
    horarios.agregarHorario(idHorario, funcion);
    funcion = 0;
}
var filtrarHorario = (numPagina, order) => {
    var valor = document.getElementById("filtrar").value;
    var action = 'Horarios/filtrarHorario';
    var horarios = new Horarios(valor, "", "", action);
    horarios.filtrarHorario(numPagina, order);
}

var editarHorario = (id, fun) => {
    funcion = fun;
    idHorario = id;
    var action = 'Horarios/getHorario';
    var horarios = new Horarios("", "", "", action);
    horarios.getHorario(id, fun);
}

var editarHorario1 = () => {
    var action = 'Horarios/editarHorario';
    var horarios = new Horarios("", "", "", action);
    horarios.editarHorario(idHorario, funcion);
}

var restablecer = () => {
    var horarios = new Horarios("", "", "", "");
    horarios.restablecer();
}
var deleteHorario = (id) => {
    idHorario = id;
}
var deleteHorarios = () => {
    var action = 'Horarios/deleteHorario';
    var horarios = new Horarios("", "", "", action);
    horarios.deleteHorario(idHorario, action);
    idHorario = 0;
}

/** Codigo de Maquinarias */
var getActividadesM = (id, fun) => {
    var action = 'Maquinarias/getActividadesM';
    var maquinaria = new Maquinaria("", "", "", action);
    maquinaria.getActividadesM(id, fun);
}
var agregarMaquinaria = () => {
    if (funcion == 0) {
        var action = 'Maquinarias/agregarMaquinaria';
    } else {
        var action = 'Maquinarias/editarMaquinaria';
    }

    var nombre = document.getElementById("Nombre").value;
    var cantidad = document.getElementById("Cantidad").value;
    var actividades = document.getElementById('ActividadMaquinarias');
    var actividad = actividades.options[actividades.selectedIndex].value;
    var maquinaria = new Maquinaria(nombre, cantidad, actividad, action);
    maquinaria.agregarMaquinaria(idMaquinaria, funcion);
    funcion = 0;
}
var filtrarMaquinaria = (numPagina, order) => {
    var valor = document.getElementById("filtrar").value;
    var action = 'Maquinarias/filtrarMaquinaria';
    var maquinarias = new Maquinaria(valor, "", "", action);
    maquinarias.filtrarMaquinaria(numPagina, order);
}
var editarMaquinaria = (id, fun) => {
    funcion = fun;
    idMaquinaria = id;
    var action = 'Maquinarias/getMaquinaria';
    var maquinarias = new Maquinaria("", "", "", action);
    maquinarias.getMaquinaria(id, fun);
}
var editarMaquinaria1 = () => {
    var action = 'Maquinarias/editarMaquinaria';
    var maquinaras = new Maquinaria("", "", "", action);
    maquinaras.editarMaquinaria(idMaquinaria, funcion);
}
var restablecer = () => {
    var maquinarias = new Maquinaria("", "", "", "");
    maquinarias.restablecer();
}
var deleteMaquinaria = (id) => {
    idMaquinaria = id;
}
var deleteMaquinarias = () => {
    var action = 'Maquinarias/deleteMaquinaria';
    var maquinaras = new Maquinaria("", "", "", action);
    maquinaras.deleteMaquinaria(idMaquinaria, action);
    idMaquinaria = 0;
}

/** Codigo de Tarifas */
var getActividadesT = (id, fun) => {
    var action = 'Tarifas/getActividadesT';
    var tarifa = new Tarifas("", "", "", "", "", action);
    tarifa.getActividadesT(id, fun);
}
var agregarTarifa = () => {
    if (funcion == 0) {
        var action = 'Tarifas/agregarTarifa';
    } else {
        var action = 'Tarifas/editarTarifa';
    }

    var valorEst = document.getElementById("ValorEst").value;
    var valorEmp = document.getElementById("ValorEmp").value;
    var valorFam = document.getElementById("ValorFam").value;
    var valorGrad = document.getElementById("ValorGrad").value;
    var actividades = document.getElementById('ActividadTarifas');
    var actividad = actividades.options[actividades.selectedIndex].value;
    var tarifa = new Tarifas(valorEst, valorEmp, valorFam, valorGrad, actividad, action);
    tarifa.agregarTarifa(idTarifa, funcion);
    funcion = 0;
}
var filtrarTarifa = (numPagina, order) => {
    var valor = document.getElementById("filtrar").value;
    var action = 'Tarifas/filtrarTarifa';
    var tarifa = new Tarifas(valor, "", "", "", "", action);
    tarifa.filtrarTarifa(numPagina, order);
}
var editarTarifa = (id, fun) => {
    funcion = fun;
    idTarifa = id;
    var action = 'Tarifas/getTarifas';
    var tarifa = new Tarifas("", "", "", "", "", action);
    tarifa.getTarifas(id, fun);
}
var editarTarifa1 = () => {
    var action = 'Tarifas/editarTarifa';
    var tarifa = new Tarifas("", "", "", "", "", action);
    tarifa.editarTarifa(idTarifa, funcion);
}
var restablecer = () => {
    var tarifa = new Tarifas("", "", "", "", "", "");
    tarifa.restablecer();
}
var deleteTarifa = (id) => {
    idTarifa = id;
}
var deleteTarifas = () => {
    var action = 'Tarifas/deleteTarifa';
    var tarifa = new Tarifas("", "", "", "", "", action);
    tarifa.deleteTarifa(idTarifa, action);
    idTarifa = 0;
}

/** Codigo de Instructores */
var instructor = new Instructores();  
var guardarInstructor = () => {

    var action = 'Instructores/guardarInstructor';
    var especialidad = document.getElementById("Especialidad").value;
    var nombre = document.getElementById("Nombre").value;
    var apellido = document.getElementById("Apellidos").value;
    var documento = document.getElementById("Documento").value;
    var email = document.getElementById("Email").value;
    var telefono = document.getElementById("Telefono").value;
    var estado = document.getElementById("Estado").checked
    instructor.guardarInstructor(idInstructor, funcion, action, especialidad, nombre, apellido, documento, email, telefono, estado);
    idInstructor = 0;  
}
var filtrarInstructores = (numPagina, order) => {
    var valor = document.getElementById("filtrar").value;
    var action = 'Instructores/filtrarInstructores';
    instructor.filtrarInstructores(numPagina, valor, order, action);
} 
var editarInstructor = (id, fun) => {
    idInstructor = id;
    funcion = fun;
    var action = 'Instructores/getInstructor';
    instructor.getInstructor(id, fun, action);  
}
var deleteInstructor = (id) => {
    idInstructor = id;
}
var deleteInstructores = () => {
    var action = 'Instructores/deleteInstructor';
    instructor.deleteInstructor(idInstructor, action);
    idInstructor = 0;
}
var restablecerInstructores = () => {
    instructor.restablecer();
}