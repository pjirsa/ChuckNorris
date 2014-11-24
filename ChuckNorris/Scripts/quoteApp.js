(function (angular) {
    'use strict';

    var app = angular.module('angularApp', []);

    app.constant('ChuckNorrisDB', 'Hello from Chuck Norris!');

    app.controller('quoteController', ['$scope', 'ChuckNorrisDB', function ($scope, db) {
        $scope.message = db;
    }]);

})(window.angular);