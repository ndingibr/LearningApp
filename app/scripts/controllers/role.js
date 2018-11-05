'use strict';

angular.module('onlineClientApp')
  .controller('RoleCtrl',
  [
    '$scope', '$location', 'roleService',
    function($scope, $location, roleService) {

      $scope.role = {
        Id: "",
        Name: ""
      };
      $scope.message = "";

      $scope.roles = [];
      //Use one service - authservice please
      roleService.getRoles()
        .then(function(results) {
            $scope.roles = results.data;
          },
          function(error) {
            alert(error.data.message);
          });
    }
  ]);
