(function () {
    "use strict";

    angular.module("app").controller("EditGenreController", function ($scope, $http, $routeParams, $location) {

        $http.get("odata/Genres('" + $routeParams.GenreId + "')").success(function (genre) {
            $scope.genre = genre;
        });

        $scope.updateCancel = function () {
            $location.path('/');
        }

        $scope.updateGenre = function (genre) {

            $http.put("odata/Genres('" + $routeParams.GenreId + "')", genre).success(function () {
                $location.path('/');
            });
        }
    });
})();