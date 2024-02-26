$(document).ready(function () {
    var personId = null
    var buttonDownload = document.getElementById("download-visible");

    getDatatable("#table-persons")
    getDatatable("#table-cities")

    $('.report').click(function () {

        personId = $(this).attr('person-id');
        $('#modal-report').modal("show");
        $("#report-content").html('<h5 class="text-center">Selecione seu Modelo de relatório!</h5>')
    });

    $('#button-report-simple').click(function () {
        $.ajax({
            type: 'GET',
            url: 'Person/GetPerson/' + personId + "?pertionView=_SimpleReport", success: function (result) {
                $("#report-content").html(result);
                buttonDownload.style.display = "block";
            }
        })
    })
    $('#button-report-complet').click(function () {
            $.ajax({
                type: 'GET',
                url: 'Person/GetPerson/' + personId + "?pertionView=_CompletReport", success: function (result) {
                    $("#report-content").html(result);
                    buttonDownload.style.display = "block";
                }
            })
        })
})

$('.close-alert').click(function () {
    $('.alert').hide('hide');
})

function applyCpfOrCnpjMask(input) {
    var personType = document.getElementById("personType").value
    if (personType === "FI") {
        document.getElementById("cpfCnpjInput").maxLength = 14
        applyCpfMask(input)
    } else {
        document.getElementById("cpfCnpjInput").maxLength = 18
        applyCnpjMask(input)
    }
}

// Masca para cpf
function applyCpfMask(input) {

    var value = input.value;
    value = value.replace(/\D/g, "");
    value = value.replace(/(\d{3})(\d)/, "$1.$2");
    value = value.replace(/(\d{3})(\d)/, "$1.$2");
    value = value.replace(/(\d{3})(\d{1,2})$/, "$1-$2");
    input.value = value;
}

function applyCnpjMask(input) {
    var value = input.value;
    value = value.replace(/\D/g, "");
    value = value.replace(/^(\d{2})(\d)/, "$1.$2");
    value = value.replace(/^(\d{2})\.(\d{3})(\d)/, "$1.$2.$3");
    value = value.replace(/\.(\d{3})(\d)/, ".$1/$2");
    value = value.replace(/(\d{4})(\d)/, "$1-$2");
    input.value = value;
}

// Mascara para telefone
function applyPhoneMask(input) {
    var value = input.value;
    value = value.replace(/\D/g, "")                
    value = value.replace(/^(\d\d)(\d)/g, "($1) $2") 
    value = value.replace(/(\d{5})(\d)/, "$1-$2")
    input.value = value;
}

function resetCpfCnpj() {
    var input = document.getElementById("cpfCnpjInput")
    input.value = ""

    var personType = document.getElementById("personType").value
    if (personType === "FI") {
        input.placeholder = "000.000.000-00"
    }
    else {
        input.placeholder = "00.000.000/000"
    }
}

function generatePDF() {
    const report = document.getElementById('report-content');
    const options = {
        margin: [10, 10, 10, 10],
        filename: "Relatório.pdf",
        html2canvas: { scale: 2 },
        jsPDF: { unit: "mm", format: "a4", orientation: "portrait" }
    }
    html2pdf().set(options).from(report).save();
}

function getDatatable(id) {
    $(id).DataTable({
        "ordering": true,
        "paging": true,
        "searching": true,
        "columnDefs": "#table-persons" === id ?
            [
                { "searchable": false, "targets": [0, 1, 2, 6,7] },
            ] :
            [
                { "searchable": false, "targets": [0, 3] },
            ],
        "oLanguage": {
            "sEmptyTable": "Nenhum registro encontrado na tabela",
            "sInfo": "Mostrar _START_ até _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrar 0 até 0 de 0 Registros",
            "sInfoFiltered": "(Filtrar de _MAX_ total registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Mostrar _MENU_ registros por pagina",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Pesquisar",
            "oPaginate": {
                "sNext": "Proximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "Ultimo"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        }
    });
}
