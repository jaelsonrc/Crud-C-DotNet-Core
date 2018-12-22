app.controller('clientesController', ['$scope', '$http', '$location', '$routeParams', function($scope, $http, $location, $routeParams){
    console.log("chamando controller")
    $scope.required = true;
    $scope.disable=false;
    $scope.totalClientes=0;
    $scope.getClientes = function(){
		$http.get('/api/Clientes').then(function(response){
            $scope.clientes = response.data;
            $scope.totalClientes= response.data.length;
		});
    };
    
    $scope.getCliente = function(){       
        var id = $routeParams.id;
        if(id != undefined)
		$http.get('/api/Clientes/'+id).then(function(response){
            $scope.cliente = response.data;         
		});
    };

    function showError(response) {
        $scope.message = response.data.Message != undefined ? response.data.Message : "Falha ao salvar tente novamente";
        $scope.message = $scope.message.replace("Validation failed:", "").replace("--", "").trim();
        $scope.alert = true;
    }

    
	$scope.addCliente = function(){	
    
		$http.post('/api/Clientes', $scope.cliente).then(function(response){
            $scope.getClientes();
            $location.path('/clientes');  		
        }).catch(function (fallback) {
            $scope.disable = false;
            showError(fallback);
        });
	}

	$scope.updateCliente = function(){
        var id = $routeParams.id;
        if(id != undefined){         
          
           $http.put('/api/Clientes', $scope.cliente).then(function(response){
                 $scope.disable=false;
                 $scope.getClientes();
                $location.path('/clientes');  
           }).catch(function (fallback) {
               $scope.disable = false;
               showError(fallback);
           });        
        }
	}

    
    $scope.saveCliente = function(){
        $scope.disable=true;
        if(window.location.href.indexOf("clientes/cadastro/add") > 0)
             $scope.addCliente();
        else
            $scope.updateCliente();
    }

	$scope.removeCliente = function(){
        $('#modalDelete').modal('hide');
        id=$scope.idRemove;
        if(id != undefined)
		$http.delete('/api/Clientes/'+id).then(function(response){
            $scope.disable=false;
            $scope.getClientes();
        }).catch(function (fallback) {
            $scope.disable = false;
            showError(fallback);
        });
    }

    
    $scope.confirmaRemove=function(id){
        console.log(id)
        $scope.message="Deseja remover o Cliente?"
        $scope.idRemove=id;
        $('#modalDelete').modal('toggle');
    };
 

    
}]);