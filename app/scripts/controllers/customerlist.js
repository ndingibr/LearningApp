
'use strict';

angular.module('onlineClientApp')
  .controller('CustomerlistCtrl',
  [
    '$scope', '$location', '$timeout', 'customerService', 'authService',
    function($scope, $location, $timeout, customerService, authService) {

      $scope.savedSuccessfully = false;
      $scope.message = "";
 
      $scope.search = {
        email: "",
        firstName: "",
        lastName: "",
        dateofBirth: "",
        ipAddress: "",
        roles: []

      };
      $scope.customers = "";

      $scope.roles = [
        {
          "Users": [],
          "Id": "Admin",
          "Name": "Administrator"
        },
        {
          "Users": [],
          "Id": "buyer",
          "Name": "buyer"
        }
      ];

      authService.getRoles()
        .then(function(results) {
            $scope.message = "Everything was successful";
            $scope.roles = results.data;
          },
          function(error) {
            alert(error.data.message);
          });

      $scope.search = function() {
        customerService.searchCustomer($scope.search)
          .then(function(results) {
              $scope.savedSuccessfully = true;
              $scope.customers = results.data;
            },
            function(error) {
              var errors = [];
              for (var key in error.data.ModelState) {
                for (var i = 0; i < error.data.ModelState[key].length; i++) {
                  errors.push(error.data.ModelState[key][i]);
                }
              }
              $scope.message = "Failed to search due to :" + errors.join(' ');
            });
      };


    }
  ]);
