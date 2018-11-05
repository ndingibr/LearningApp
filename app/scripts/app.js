'use strict';
var serviceBase = 'http://localhost:54887/';

angular
  .module('onlineClientApp',
  [
    'ngRoute',
    'LocalStorageModule'
    ])
  .config(function($routeProvider) {
    $routeProvider
      .when('/',
      {
        templateUrl: 'views/main.html',
        controller: 'MainCtrl',
        controllerAs: 'main'

      })
      .when('/about',
      {
        templateUrl: 'views/about.html',
        controller: 'AboutCtrl',
        controllerAs: 'about'
      })
      .when('/machinelearning',
      {
        templateUrl: 'views/machinelearning.html',
        controller: 'MachinelearningCtrl',
        controllerAs: 'machinelearning'
      })
      .when('/signup',
      {
        templateUrl: 'views/signup.html',
        controller: 'SignupCtrl',
        controllerAs: 'signup'
        })
        .when('/data',
        {
            templateUrl: 'views/data.html',
            controller: 'DataCtrl',
            controllerAs: 'data'
        })
        .when('/incident',
        {
            templateUrl: 'views/incident.html',
            controller: 'IncidentCtrl',
            controllerAs: 'incident'
        })
        .when('/detail/:latitude',
        {
            templateUrl: 'views/detail.html',
            controller: 'DetailCtrl',
            controllerAs: 'detail'
        })
        .when('/signup/:email/:isAdmin',
      {
        templateUrl: 'views/signup.html',
        controller: 'SignupCtrl',
        controllerAs: 'signup'
        })
        .when('/updateprofile/',
        {
            templateUrl: 'views/updateprofile.html',
            controller: 'UpdateProfileCtrl',
            controllerAs: 'updateprofile'
        })
      .when('/login',
      {
        templateUrl: 'views/login.html',
        controller: 'LoginCtrl',
        controllerAs: 'login'
      })
      .when('/users',
      {
        templateUrl: 'views/users.html',
        controller: 'UsersCtrl',
        controllerAs: 'users'
        })
        .when('/upload',
        {
            templateUrl: 'views/upload.html',
            controller: 'uploadCtrl',
            controllerAs: 'upload'
        })
      .when('/role', {
        templateUrl: 'views/role/list.html',
        controller: 'RoleCtrl',
        controllerAs: 'role'
      })
      .otherwise({
        redirectTo: '/'
      });
  })
  .constant('ngAuthSettings',
  {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
  });


