var reportSelected = "simple-report"
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
        reportSelected = "simple-report"
        getPerson(personId, "_SimpleReport")
        buttonDownload.style.display = "block";
    })
    $('#button-report-complet').click(function () {
        reportSelected = "report-complet"
        getPerson(personId, "_CompletReport")
        buttonDownload.style.display = "block";
    })

    $(document).on("input", "#input-cep", function () {
        var cep = $(this).val()
        console.log(cep)
        if (cep.length === 8) { // Verifica se o CEP tem 8 dígitos
            checkCep(cep);
        } 
    });
})

$('.close-alert').click(function () {
    $('.alert').hide('hide');
})

function getPerson(personId ,pationView) {
    $.ajax({
        type: 'GET',
        url: 'Person/GetPerson/' + personId + "?pertionView=" + pationView, success: function (result) {
            $("#report-content").html(result);
 
        }
    })
}

// Verifica o cep
function checkCep(cep) {
    $.ajax({
        type: 'GET',
        url: `https://viacep.com.br/ws/${cep}/json/`, success: function (result) {
            $("#report-content").html(result);
            let addressInput = document.getElementById("input-address")
            let complementInput = document.getElementById("input-complement")
            let districtInput = document.getElementById("input-district")
            let selectCities = document.getElementById("select-city")

            if (result.logradouro)
                addressInput.value = result.logradouro
            if (result.bairro)
                districtInput.value = result.bairro
            if (result.complemento)
                complementInput.value = result.complemento


            for (var i = 0; i < selectCities.options.length; i++) {
                if (selectCities.options[i].text === result.localidade) {
                    selectCities.selectedIndex = i;
                    break; 
                }
            }
        }
    })
}

// Gerencia a mascara e aplica para cpf/cnpj
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

// Masca para Cpf
function applyCpfMask(input) {

    var value = input.value;
    value = value.replace(/\D/g, "");
    value = value.replace(/(\d{3})(\d)/, "$1.$2");
    value = value.replace(/(\d{3})(\d)/, "$1.$2");
    value = value.replace(/(\d{3})(\d{1,2})$/, "$1-$2");
    input.value = value;
}
// Mascara para Cnpj
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

// Mascara para Reseta mascara
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

// Gera pdf com base em um html
function generatePDF() {
    const report = document.getElementById(reportSelected);
    const options = {
        margin: [10, 10, 10, 10],
        filename: "Relatório.pdf",
        html2canvas: { scale: 2 },
        jsPDF: { unit: "mm", format: "a4", orientation: "portrait" }
    }
    html2pdf().set(options).from(report).save();
}

// Aplica DataTable na tabela com id passado
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
