'use strict';

angular
  .module('onlineClientApp')
  .factory('customerService',
  [
    '$http', '$q', 'ngAuthSettings', 
    function($http, $q, ngAuthSettings) {

      var serviceBase = ngAuthSettings.apiServiceBaseUri;

      var service = {
        searchCustomer: searchCustomer
      };

      return service;

      function searchCustomer(search) {
        var url = serviceBase +
          'api/Customer?email=' +
          search.email +
          '&firstName=' +
          search.firstName +
          '&lastName=' +
          search.lastName +
          '&dateofBirth=' +
          search.dateOfBirth +
          '&ipAddress=' +
          search.ipAddress;
        return $http.get(url).then(function (results) {
          return results;
        });
        }
    }
  ]);