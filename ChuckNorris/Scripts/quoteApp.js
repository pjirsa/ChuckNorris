(function (angular) {
    'use strict';

    var app = angular.module('angularApp', ['ngResource']);

    app.factory('ChuckNorrisDB', ['$resource',
        function ($resource) {
            var Quote = $resource('http://api.icndb.com/jokes/random?limitTo=[:category]', { category: '@category' });

            return Quote;
        }]);

    app.controller('quoteController', ['$scope', 'ChuckNorrisDB', function ($scope, db) {
        $scope.message = '';

        function init() {
            db.get({ category: 'nerdy' }, function (joke) {
                $scope.message = joke.value.joke;
            });
        }

        init();
    }]);

})(window.angular);