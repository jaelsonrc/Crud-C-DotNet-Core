app.controller('agendaController', ['$scope', '$http', '$location', '$routeParams',
    function ($scope, $http, $location, $routeParams) {



        const formatData = "DD/MM/YYYY";
        const formatHora = 'hh:ii:ss';
        $scope.required = true;
        $scope.disable = false;
        $scope.totalAgenda = 0;
        $scope.message = "";
        $scope.alert = false;
        setDataHora(new Date());
        $scope.agenda = {};



        function setDataHora(data) {
            $scope.data = moment(data, formatData);
            $scope.hora = moment(data, formatHora);
        }
        function getDataHora() {
            let data = moment($scope.data, formatData).toDate();
            let strData = moment(data).format('YYYY-MM-DD') + "T" + $scope.hora+":00";
            return strData;
        }

        $scope.getAgendamentos = function () {
            $http.get('/api/Agenda').then(function (response) {
                $scope.agendamentos = response.data;
                $scope.totalAgenda = response.data.length;
            });
        };

        $scope.getClientes = function () {
            $scope.alert = false;
            $http.get('/api/Clientes').then(function (response) {
                $scope.clientes = response.data;
                $scope.totalClientes = response.data.length;
                $scope.getAgenda();
            });

        };

        function setCliente(id) {
            $scope.clientes.forEach(element => {
                if (element.id == id)
                    $scope.agenda.cliente = element;
            });
        }


        $scope.getAgenda = function () {
            var id = $routeParams.id;
            if (id != undefined)
                $http.get('/api/Agenda/' + id).then(function (response) {
                    $scope.agenda = response.data;
                    setCliente(response.data.cliente.id);
                    setDataHora(new Date(response.data.data));
                    $scope.agenda.modalidade = $scope.agenda.modalidade + "";
                });
        };

        function showError(response) {
            $scope.message = response.data.Message != undefined ? response.data.Message : "Falha ao salvar tente novamente";
            $scope.message = $scope.message.replace("Validation failed:", "").replace("--", "").trim();
            $scope.alert = true;
        }

        $scope.addAgenda = function () {
            $scope.alert = false;
            $scope.agenda.data = getDataHora();
            $http.post('/api/Agenda', $scope.agenda).then(function (response) {
                $scope.getAgendamentos();
                $location.path('/');
            }).catch(function (fallback) {
                $scope.disable = false;
                showError(fallback);
            });
        }

        $scope.updateAgenda = function () {
            var id = $routeParams.id;
            if (id != undefined) {
                $scope.agenda.data = getDataHora();
                $http.put('/api/Agenda', $scope.agenda).then(function (response) {
                    $scope.disable = false;
                    $scope.getAgendamentos();
                    $location.path('/');
                }).catch(function (fallback) {
                    $scope.disable = false;
                    showError(fallback);
                });
            }
        }

        $scope.saveAgenda = function () {
            $scope.disable = true;
            if (window.location.href.indexOf("/agenda/add") > 0)
                $scope.addAgenda();
            else
                $scope.updateAgenda();
        }

        $scope.verificarData = function (data) {
            if (data.substring(0, 10) == new Date().toISOString().substring(0, 10)) return true;
            return false;
        }

        $scope.removeAgenda = function () {
            $('#modalAgenda').modal('hide');
            agenda = $scope.agendaRemove;
            if (agenda != undefined)
                $http.delete(`/api/Agenda/${agenda.id}/${agenda.data.substring(0, 10)}`).then(function (response) {
                    $scope.disable = false;
                    $scope.getAgendamentos();
                }).catch(function (fallback) {
                    $scope.disable = false;
                    showError(fallback);
                });;
        }


        $scope.confirmaRemove = function (agenda) {
            $scope.message = `Deseja cancelar o agendamento do Cliente: ${agenda.cliente.nome}?`;
            $scope.agendaRemove = agenda;
            $('#modalAgenda').modal('toggle');
        };

    }]);