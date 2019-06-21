function errorEx(s, e) {
    if (s.xhr != null) { //si el error es desde un grid de telerik
        ValidateXHRError(s.xhr);
        //s.sender.read();
        s.sender.cancelChanges();
    } else if (s.responseJSON != null) {//si el error es desde una accion en cualquier parte del formulario
        var json = s.responseJSON;
        Mensaje(json.tiempo, json.tipo, json.message, json.titulo);
    } else if (s.Data != null) {//si el error es desde una accion en cualquier parte del formulario
        var json = s.Data;
        Mensaje(json.tiempo, json.tipo, json.message, json.titulo);
    }
    else {
        error(s);
    }
}

function ValidateXHRError(xhr) {
    if (xhr.responseJSON != null) {
        if (xhr.responseJSON.Excepcion != null) {
            var json = xhr.responseJSON.Excepcion;
            MensajeJSON(json);
        }
    } else {
        Mensaje(0, 'error', 'Este error no se encuentra documentado, por favor consultar con el administrador para mayor información.', "Error");
    }
}

function error(args) {
    $("div.k-tooltip-validation").hide();

    if (args.errors) {
        $(".k-grid").each(function () {
            var grid = $(this).data("kendoGrid");
            if (grid !== null && grid.dataSource === args.sender) {

                if (!grid.editable) { //Si es eliminar cancela los cambios.
                    grid.cancelChanges();
                }

                grid.one("dataBinding", function (e) {
                    e.preventDefault();
                    toastr.clear();

                    for (var key in args.errors) {                      
                        if (grid.editable) {
                            if (args.errors[key].errors.length > 1) {
                                printModelStateErrorToast(key,args.errors[key].errors);
                            } else {
                                showMessageField(grid.editable.element, key, args.errors[key].errors);
                            }
                        } else {                            
                            printModelStateErrorToast(key,args.errors[key].errors);
                        }                        
                    }
                });
            }
        });
    }
}

function updateGridViews() {    
    $(".k-grid").each(function () {
        var grid = $(this).data("kendoGrid");
        grid.dataSource.read();
    });
}
function printModelStateErrorToast(key, errors) {
    var message="";
    $.each(errors, function () {
        message += this + "</br>";
    });

    Mensaje("error", "error", message, "Error " + key);
}

function showMessageField(container, name, errors) {
    var validationMessageTmpl = kendo.template($("#message").html());
    //add the validation message to the form
    container.find("[data-valmsg-for=" + name + "],[data-val-msg-for=" + name + "]")
        .replaceWith(validationMessageTmpl({ field: name, message: errors[0] }))
}

function MensajeJSON(json) {
    Mensaje(json.tiempo, json.tipo, json.message, json.titulo);
}

function Mensaje(tiempoEspera, tipoMensaje, mensaje, titulo) {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-bottom-full-width",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": tiempoEspera,
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    toastr[tipoMensaje](mensaje, titulo);
}

function disableDates(date, datesDisabled) {
    if (date && compareDates(date, datesDisabled)) {
        return true;
    }
    else if (date && disableSaturdayAndSunday(date)) {
        return true;
    }
    else {
        return false;
    }
}

function disableSaturdayAndSunday(date) {
    if (date !== null) {
        if (date.getDay() === 6 || date.getDay() === 0) {
            return true;
        }
    }
}

function compareDates(date, dates) {
    for (var i = 0; i < dates.length; i++) {
        if (dates[i].getDate() === date.getDate() &&
            dates[i].getMonth() === date.getMonth() &&
            dates[i].getYear() === date.getYear()) {
            return true;
        }
    }
}

function LoadingStart(text, id) {
    if (id === null || id === undefined) {
        id = 'resultLoading';
    }
    let altoObjecto = 90;

    var elementoImagen = "";

    var elemento = "<div id=\'" + id + "\' style=\'display:none\'>" +
        "<div><div>" + text + "</div>" +
        "<i class=\'fa fa fa-refresh fa-spin\' style=\'font-size:48px; color: #337ab7'\>" +
        "</i>" +
        "</div>" +
        "<div class=\'clLoading\'></div>" +
        "</div>";

    if (jQuery('body').find('#' + id).attr('id') !== id) {
        jQuery('body').append(elemento);
    } else {
        jQuery('body').find('#' + id).replaceWith(elemento);
    }

    jQuery('#' + id).css({
        'width': '100%',
        'height': '100%',
        'position': 'fixed',
        'z-index': '10000000',
        'top': '0',
        'left': '0',
        'right': '0',
        'bottom': '0',
        'margin': 'auto'
    });
    jQuery('#' + id + ' .clLoading').css({
        'background': 'white',
        'opacity': '0.95',
        'width': '100%',
        'height': '100%',
        'position': 'absolute',
        'top': '0'
    });
    jQuery('#' + id + '>div:first').css({
        'width': '350px',
        'height': altoObjecto + 'px',
        'text-align': 'center',
        'position': 'fixed',
        'top': '0',
        'left': '0',
        'right': '0',
        'bottom': '0',
        'margin': 'auto',
        'font-size': '18px',
        'z-index': '10',
        'color': 'black'
    });

    jQuery('#' + id + ' .clLoading').height('100%');
    jQuery('#' + id).show().fadeIn(300);
    jQuery('body').css('cursor', 'wait');
}

function LoadingStop(id) {
    if (id === null || id === undefined) {
        id = 'resultLoading';
    }

    jQuery('#' + id + ' .clLoading').height('100%');
    jQuery('#' + id).fadeOut(300);
    jQuery('body').css('cursor', 'default');
}

function onRequestStart(e) {
    if (e.type === "create") {
        LoadingStart('Creando registro.');
    } else if (e.type === "destroy") {
        LoadingStart('Eliminando registro.');
    } else if (e.type === "update") {
        LoadingStart('Actualizando registro.');
    } else {
        LoadingStart('Recuperando información.');
    }
}

function onRequestEnd(e) {       
    if (e.type === "create" || e.type === "update") {
        if (e.response === null || !e.response.Errors) {           
            $(".k-grid").data("kendoGrid").dataSource.read();
        }
    }
    LoadingStop();
}

function getSelectedPersons() {
    var multiSelect = $("#PersonsId").data("kendoMultiSelect");

    return {
        personsIds: multiSelect.value()
    };
}

function getDateMultiple() {
    var selectDates = $("#Date_multipleDates").data("kendoCalendar");

    return {
        dates: selectDates.selectDates()
    };
}

function onGridEdit(e) {    
    $("a.k-grid-update")[0].innerHTML = "<span class='k-icon k-i-check'></span>Aceptar";

    if (e.model.isNew() === false) {
        var multiSelect = $("#PersonsId").data("kendoMultiSelect");
        if (multiSelect) {
            multiSelect.enable(false);
        }
    }
}

function onDataBoundRemoveButtonsPreviousDate(e) {
    var grid = this;

    grid.tbody.find('>tr').each(function (s, e) {
        var dataItem = grid.dataItem(e);
        if (dataItem) {
            if (dataItem.IsPreviousDate === true) {
                $(e).find(".k-grid-edit").remove();
                $(e).find(".k-grid-delete").remove();
            }
        }
    });       
    
    //if (grid.dataSource.group().length > 0) {
    //    $(".k-grouping-row").each(function () {
    //        grid.collapseGroup(this);
    //    });
    //}            
}

function onRefreshPopUp(s) {
    s.sender.open();
    s.sender.center();
}