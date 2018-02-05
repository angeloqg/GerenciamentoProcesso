/// <reference path="C:\Users\angel\Desktop\Prova Técnica_Perlink\Prova_Perlink\Sistema_Gerenciamento_Processos\LayerApplication\Scripts/angular.js" />
/// <reference path="C:\Users\angel\Desktop\Prova Técnica_Perlink\Prova_Perlink\Sistema_Gerenciamento_Processos\LayerApplication\Scripts/jquery-1.10.2.js" />

var app = angular.module("MyApp", []);

app.controller("MyCtrl", function ($scope, $http) {
    ListarProcessos($scope, $http);

    $scope.PesquisarProcessos = function () {
        var id = "";
        var empresa = $("#campo1").val();
        var situacao = $("#campo2").val();
        var estado = $("#campo3").val();
        var tipo = $("#campo4").val();
        var optBuscaData = $("#campo5").val();
        var dia = $("#campo6").val();
        var mes = $("#campo7").val();
        var ano = $("#campo8").val();
        var oprBusca = $("#campo9").val();
        var valorProcesso = $("#campo10").val();
        
        $http.post("/home/carregaProcessosFiltro", {id, tipo, situacao, empresa, oprBusca, valorProcesso, optBuscaData, dia, mes, ano, estado})
        .success(function (data) {
            $scope.ListaProcessos = data.Dados;
            $scope.NumeroProcessos = data.NumeroProcessos;
            $scope.ValorTotal = data.ValorTotal;
            $scope.ValorMedio = data.ValorMedio;
        });
    }

    $scope.ExibirTodos = function () {
        ListarProcessos($scope, $http);

        $("#campo1").val("");
        $("#campo2").val("");
        $("#campo3").val("");
        $("#campo4").val("");
        $("#campo5").val("");
        $("#campo6").val("");
        $("#campo7").val("");
        $("#campo8").val("");
        $("#campo9").val("");
        $("#campo10").val("");
    }

    $scope.NovoProcesso = function () {

        LimparCampos();

        $('#myModal').modal('show')
        $("#titulo").text("Novo Processo");
        $("#titulo").css("color", "orange");
        $('#cadastroProcesso').show();
        $('#excluirProcesso').hide();
        $('#novo').show();
        $('#editar').hide();
        $('#excluir').hide();
    }

    $scope.EditarProcesso = function (id) {

        LimparCampos();

        $http.post("/home/RetornaDadosProcesso", {id})
        .success(function (data) {
            $("#codigoProcesso").val(data.Codigo);
            $("#nomeEmpresa").val(data.Empresa);
            $("#cnpjEmpresa").val(data.Cnpj);
            $("#estadoEmpresa").val(data.EstadoEmpresa);
            $("#tipoProcesso").val(data.Tipo);
            $("#estado").val(data.Estado);
            $("#situacaoProcesso").val(data.Situacao);
            $("#dataInicio").val(data.DataInicio);
            $("#valorProcesso").val(data.Valor);

            $("#titulo").text("Editar Processo - código: " + data.Processo);
        });

        $('#myModal').modal('show')
        
        $("#titulo").css("color", "blue");
        $('#cadastroProcesso').show();
        $('#excluirProcesso').hide();
        $('#novo').hide();
        $('#editar').show();
        $('#excluir').hide();
    }

    $scope.ExcluirProcesso = function (codigo, processo) {

        LimparCampos();

        $("#codigoProcesso").val(codigo);
        $("#codigo").text(processo);

        $('#myModal').modal('show');
        $("#titulo").text("Excluir Processo");
        $("#titulo").css("color", "red");
        $('#cadastroProcesso').hide();
        $('#excluirProcesso').show();
        $('#novo').hide();
        $('#editar').hide();
        $('#excluir').show();
    }

    $scope.ConfirmarExclusao = function () {
        var codigo = $("#codigoProcesso").val();
        
        $("#tituloAviso").text("Exclusão de Processo");

        $scope.ListaProcessos = null;
        $scope.NumeroProcessos = null;
        $scope.ValorTotal = null;
        $scope.ValorMedio = null;

        $http.post("/home/excluirProcesso", {codigo})
        .success(function (data) {
            var resultado = "" + data.Mensagem;

            if (resultado.indexOf("sucesso") > -1) {
                $("#sucesso").show();
                $("#erro").hide();
                $("#msgSucesso").text(resultado);
            } else {
                $("#sucesso").hide();
                $("#erro").show();
                $("#msgErro").text(resultado);
            }

            $("#modalAviso").modal("show");

            ListarProcessos($scope, $http);
        });       
    }

    $scope.ConfirmarEdicao = function () {

        var codigo = $("#codigoProcesso").val();
        var empresa = $("#nomeEmpresa").val();
        var cnpj = $("#cnpjEmpresa").val();
        var estadoEmpresa = $("#estadoEmpresa").val();
        var tipo = $("#tipoProcesso").val();
        var estado = $("#estado").val();
        var situacao = $("#situacaoProcesso").val();
        var dataInicio = $("#dataInicio").val();
        var valor = $("#valorProcesso").val();

        $("#tituloAviso").text("Edição de Processo");

        $scope.ListaProcessos = null;
        $scope.NumeroProcessos = null;
        $scope.ValorTotal = null;
        $scope.ValorMedio = null;

        $http.post("/home/alterarProcesso", { codigo, empresa, cnpj, estadoEmpresa, tipo, situacao, estado, dataInicio, valor})
        .success(function (data) {
            var resultado = "" + data.Mensagem;

            if (resultado.indexOf("sucesso") > -1) {
                $("#sucesso").show();
                $("#erro").hide();
                $("#msgSucesso").text(resultado);
            } else {
                $("#sucesso").hide();
                $("#erro").show();
                $("#msgErro").text(resultado);
            }

            $("#modalAviso").modal("show");

            ListarProcessos($scope, $http);
        });
    }

    $scope.ConfirmarInclusao = function () {

        var empresa = $("#nomeEmpresa").val();
        var cnpj = $("#cnpjEmpresa").val();
        var estadoEmpresa = $("#estadoEmpresa").val();
        var tipo = $("#tipoProcesso").val();
        var estado = $("#estado").val();
        var situacao = $("#situacaoProcesso").val();
        var dataInicio = $("#dataInicio").val();
        var valor = $("#valorProcesso").val();

        $("#tituloAviso").text("Edição de Processo");

        $scope.ListaProcessos = null;
        $scope.NumeroProcessos = null;
        $scope.ValorTotal = null;
        $scope.ValorMedio = null;

        $http.post("/home/incluirNovoProcesso", { empresa, cnpj, estadoEmpresa, tipo, situacao, estado, dataInicio, valor})
        .success(function (data) {
            var resultado = "" + data.Mensagem;

            if (resultado.indexOf("sucesso") > -1) {
                $("#sucesso").show();
                $("#erro").hide();
                $("#msgSucesso").text(resultado);
            } else {
                $("#sucesso").hide();
                $("#erro").show();
                $("#msgErro").text(resultado);
            }

            $("#modalAviso").modal("show");

            ListarProcessos($scope, $http);
        });
    }
});

function ListarProcessos($scope, $http) {
    $http.get("/home/carregaProcessos")
    .success(function (data) {
        $scope.ListaProcessos = data.Dados;
        $scope.NumeroProcessos = data.NumeroProcessos;
        $scope.ValorTotal = data.ValorTotal;
        $scope.ValorMedio = data.ValorMedio;
    });
}

function LimparCampos() {

    $("#nomeEmpresa").val("");
    $("#cnpjEmpresa").val("");
    $("#estadoEmpresa").val("");
    $("#tipoProcesso").val("");
    $("#estado").val("");
    $("#situacaoProcesso").val("");
    $("#dataInicio").val("");
    $("#valorProcesso").val("");
}
