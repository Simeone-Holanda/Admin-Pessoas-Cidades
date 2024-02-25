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