



var app = angular.module('app', ['ngRoute', 'moment-picker', 'ui.mask', 'brasil.filters']);



app.config(function($routeProvider) {	    


    $routeProvider
        .when('/', {
            templateUrl : 'js/angularjs/app/views/home.html',
            controller  : 'agendaController'
        })             
   
        .when('/agenda/add', {
            templateUrl : 'js/angularjs/app/views/agenda.html',
            controller  : 'agendaController'
        })
        .when('/agenda/edit/:id', {
            templateUrl : 'js/angularjs/app/views/agenda.html',
            controller  : 'agendaController'
        })
        .when('/clientes', {
            templateUrl : 'js/angularjs/app/views/clientes.html',
            controller  : 'clientesController'
        })
        .when('/clientes/cadastro/add', {
            templateUrl : 'js/angularjs/app/views/cadastro.html',
            controller  : 'clientesController'
        })  
        .when('/clientes/cadastro/edit/:id', {
            templateUrl : 'js/angularjs/app/views/cadastro.html',
            controller  : 'clientesController'
        })  
        .when('/clientes/cadastro/detail/:id', {
            templateUrl : 'js/angularjs/app/views/cadastro.html',
            controller  : 'clientesController'
        })  
        .otherwise({
		    redirectTo: "/"
        });
        
});