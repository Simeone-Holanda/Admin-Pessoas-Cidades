# Gestão Eficiente de Pessoas e Cidades

## Pré-requisitos
- [.NET Core SDK](https://dotnet.microsoft.com/pt-br/download/dotnet/6.0) - Certifique-se de ter o SDK do .NET Core instalado na sua máquina. Para esse projeto foi usado a versão 6.0

## Funcionalidades
* Crud de Cidades
* Crud de Pessoas
* Validar se o CPF/CNPJ é válido conforme o tipo de pessoa
* Validar e-mail
* Não permitir cadastrar o mesmo CPF/CNPJ
* Permitir cadastrar uma cidade caso não esteja na lista.
* Permitir a pesquisa de pessoas por tipo (Todos, Físicas, Jurídicas), cidade e estado.
* Exibir lista com Id, nome, CPF/CNPJ, tipo de pessoa, cidade, estado, data de cadastro e idade.
* Download de Pdf e previsualização de relatório de clientes básico e completos
* Api de Cep para buscar dados de endereço da pessoa

## Tecnologias: <br>

* Asp.Net Core MVC
* Entity Framework(Postgres)
* Boostrap 
* Razor Pages
* Jquery
* Javascrip

## Execução e Configuração
* git clone https://github.com/Simeone-Holanda/Admin-Pessoas-Cidades.git
* Copie o arquivo appsettings.json.example e crie um novo arquivo appsettings.json preenchendo suas configurações. 
* No seu Package Manager Console no Visual Studio use o comando a seguir para subir as migrations: Update-Database -Context ConnectionContext
* Agora so iniciar seu projeto!!