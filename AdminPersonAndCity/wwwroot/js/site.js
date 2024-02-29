var reportSelected = "simple-report"
$(document).ready(function () {
    var personId = null
    var buttonDownload = document.getElementById("download-visible");

    getDatatable("#table-persons")
    getDatatable("#table-cities")



    $('#open-modal-createcity').click(function () {

        personId = $(this).attr('person-id');
        $('#modal-create-city').modal("show");

    });
    $('#button-createcity-modal').click(function () {
        var city = document.getElementById("CityName")
        var state = document.getElementById("CityState")
        console.log(!city.value && city.value === "")
        console.log(city.value)
        console.log(state.value)
        if (!city.value && !state.value) {
            alertErrorCity("Cidade e Estado é obrigatório. ")
        }
        else if (!city.value || city.value === "") {
            alertErrorCity("Cidade é obrigatório. ")
        }
        else if (!state.value || city.value === "") {
            alertErrorCity("Estado é obrigatório. ")
        } else {
            $.ajax({
                type: 'POST',
                url: '/City/CreateInModal/',
                data: {
                    Name: city.value,
                    State: state.value,
                },
                success: function (result) {
                    var select = document.getElementById("select-city");
                    var newOption = document.createElement("option");
                    newOption.value = result.id
                    newOption.text = result.name
                    newOption.selected = true
                    select.appendChild(newOption);

                    var alertSuccess = document.getElementById("alert-success");

                    alertSuccess.style.display = "block"

                    setTimeout(() => {
                        $('#modal-create-city').modal("hide");
                    }, 1500)

                }, error: (xhr, status, error) => {
                    console.log(xhr.responseText)
                    alertErrorCity(xhr.responseText)

                }
            })
        }
    })

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
    $(document).on("input", "#cpfCnpjInput", function () {
        var personType = document.getElementById("personType").value
        var textError = document.getElementById("input-cpf-error")
        var cpfCnpj = $(this).val()
        if (personType === "FI") {
            textError.textContent = validToCpf(cpfCnpj)
        } else if (personType === "JU") {
            textError.textContent = validCnpj(cpfCnpj)
        } else {
            textError.textContent = "Selecione o tipo de pessoal"  
        }
    });
})

$('.close-alert').click(function () {
    $('.alert').hide('hide');
})

function alertErrorCity(message) {
    var alertError = document.getElementById("alert-error");
    var alertErrorText = document.getElementById("alert-error-text");

    alertError.style.display = "block"
    alertErrorText.textContent = message
}

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

var elements = document.querySelectorAll('.cpfCnpj');

function applyMask() {
    console.log('carreguei')
    elements.forEach(function (element) {
        console.log(element)
        var cpfCnpj = element.textContent;
        if (cpfCnpj.length === 11) {
            var cpfMask = applyCpfMaskInText(cpfCnpj);
            element.textContent = cpfMask
        } else if (cpfCnpj.length === 14) {
            var cnpjMask = applyCnpjMaskInText(cpfCnpj);
            element.textContent = cnpjMask;
        }
    });
}


// Mascara para Cpf para texto
function applyCpfMaskInText(cpf) {

    cpf = cpf.replace(/\D/g, "");
    cpf = cpf.replace(/(\d{3})(\d)/, "$1.$2");
    cpf = cpf.replace(/(\d{3})(\d)/, "$1.$2");
    cpf = cpf.replace(/(\d{3})(\d{1,2})$/, "$1-$2");
    return cpf
}
// Mascara para Cnpj para texto
function applyCnpjMaskInText(cnpj) {
    cnpj = cnpj.replace(/\D/g, "");
    cnpj = cnpj.replace(/^(\d{2})(\d)/, "$1.$2");
    cnpj = cnpj.replace(/^(\d{2})\.(\d{3})(\d)/, "$1.$2.$3");
    cnpj = cnpj.replace(/\.(\d{3})(\d)/, ".$1/$2");
    cnpj = cnpj.replace(/(\d{4})(\d)/, "$1-$2");
    return cnpj
}


function validToCpf(value) {
    if (value === null) return "Cpf é obrigatório.";

    let cpf = value.toString().replace(/\D/g, '');

    if (new Set(cpf).size === 1) return "Cpf inválido.";
    if (!/^\d+$/.test(cpf)) return "Cpf/Cnpj deve conter apenas dígitos.";
    if (cpf.length !== 11) return "Cpf/Cnpj deve conter 11 dígitos.";

    if (cpf[9] !== calcDigtCpf(cpf, 9)) return "Cpf inválido.";

    if (cpf[10] !== calcDigtCpf(cpf, 10)) return "Cpf inválido.";

    // return "Validação bem-sucedida.";
}

function calcDigtCpf(cpf, digitNumber) {
    let digit = 0;
    for (let i = 0; i < digitNumber; i++) {
        digit += parseInt(cpf[i]) * (digitNumber + 1 - i);
    }

    digit = (11 - (digit % 11)) > 9 ? 0 : (11 - (digit % 11));

    return digit.toString();
}

function validCnpj(value) {
    if (value === null) return "Cnpj é obrigatório.";
    let cnpj = value.replace(/\D/g, '');

    if (new Set(cnpj).size === 1) return "Cnpj inválido.";
    if (!/^\d+$/.test(cnpj)) return "Cnpj deve conter apenas dígitos.";
    if (cnpj.length !== 14) return "Cnpj deve conter 14 dígitos.";

    let firstWeights = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
    let secondWeights = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

    if (cnpj[12] !== calcDigtCnpj(cnpj, 12, firstWeights)) return "Cnpj inválido.";
    if (cnpj[13] !== calcDigtCnpj(cnpj, 13, secondWeights)) return "Cnpj inválido.";

   // return "Validação bem-sucedida.";
}

function calcDigtCnpj(cnpj, digitPosition, weights) {
    let sum = 0;
    for (let i = 0; i < digitPosition; i++) {
        sum += parseInt(cnpj[i]) * weights[i];
    }
    let remainder = sum % 11;
    let digit = (remainder < 2) ? 0 : (11 - remainder);
    return digit.toString();
}

applyMask()