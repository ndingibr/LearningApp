'use strict';

angular
  .module('onlineClientApp')
  .factory('roleService',
  [
    '$http', '$q', 'ngAuthSettings',
    function($http, $q, ngAuthSettings) {

      var serviceBase = ngAuthSettings.apiServiceBaseUri;

      var service = {
        getRoles: getRoles,
        createRole: createRole,
        updateRole: updateRole,
        deleteRole: deleteRole
      };

      return service;

      function getRoles() {
        return $http.get(serviceBase + 'api/account/roles')
          .then(function(results) {
            return results;
          });
      }

      function createRole(role) {
        return $http.get(serviceBase + 'api/role/create', role)
          .then(function(results) {
            return results;
          });
      }

      function updateRole(role) {
        return $http.get(serviceBase + 'api/role/update', role)
          .then(function(results) {
            return results;
          });
      }
      
      function deleteRole(roleId) {
        return $http.get(serviceBase + 'api/role/delete?roleId=', roleId)
          .then(function(results) {
            return results;
          });
      }

    }
  ]);
