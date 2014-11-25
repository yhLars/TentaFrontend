(function () {
    "use strict";

    angular.module("app", ["ngRoute"])
        .config(function ($routeProvider) {
            $routeProvider
                .when('/', {
                    templateUrl: 'Partials/genres.html',
                    controller: 'GenresController'
                })
            .when('/Edit/:GenreId', {
                templateUrl: 'Partials/editGenre.html',
                controller: 'EditGenreController'
            })
            .when('/Delete/:GenreId', {
                templateUrl: 'Partials/deleteGenre.html',
                controller: 'DeleteGenreController'
            });
        });
})();