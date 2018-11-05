'use strict';

angular
  .module('onlineClientApp')
  .controller('MachinelearningCtrl', ['$scope', '$location', '$routeParams', '$timeout', 'predictService',
    function ($scope, $location, $routeParams, $timeout, predictService) {

      $scope.attributes = []

      predictService.getAttributes()
        .then(function (results) {
          $scope.attributes = results.data;
        });

      $scope.updateAttributes = function () {
        predictService.submitAttributes($scope.attributes).then(function (response) {
          $scope.savedSuccessfully = true;
        })
      };

     }]);
