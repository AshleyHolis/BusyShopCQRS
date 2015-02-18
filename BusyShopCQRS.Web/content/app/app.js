var shopApp = angular.module('shopApp', ['ngRoute', 'ui.bootstrap']);

shopApp.baseUrl = "/api/";

shopApp.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
        when('/customer', {
            templateUrl: '/app/customer/customer.html',
            controller: 'CustomerCtrl'
        }).
        when('/product', {
            templateUrl: '/app/product/product.html',
            controller: 'ProductCtrl'
        }).
        when('/order', {
            templateUrl: '/app/order/order.html',
            controller: 'OrderCtrl'
        }).
        otherwise({
            redirectTo: '/'
        });
  }]);

shopApp.directive('commandContainer', function () {
    return {
        restrict: 'E',
        transclude: 'true',
        scope: {
            submitText: "@",
            commandName: "@",
            command: "="
        },
        template: '<form role="form" ng-submit="sendCommand(command, commandName)">' +
            '<div class="my-transclude"></div>' +
            '<input type="submit" class="btn btn-success" value="{{submitText}}" />' +
            '</div>',
        controller: 'CommandContainerController',
        link: function (scope, element, attrs, ctrl, transclude) {
            transclude(scope.$parent, function (clone) {
                element.find(".my-transclude").replaceWith(clone);
            });
        }
    };
});

shopApp.controller('CommandContainerController', function ($scope, $http) {
    $scope.sendCommand = function (command, commandName) {
        var url = shopApp.baseUrl + commandName;

        var guid = (function () {
            function s4() {
                return Math.floor((1 + Math.random()) * 0x10000)
                           .toString(16)
                           .substring(1);
            }
            return function () {
                return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
                       s4() + '-' + s4() + s4() + s4();
            };
        })();

        command.Id = guid();
        $http.post(url, command).success(function (data) {
            $scope.command = {};
        }).error(function () {
            alert("Failed to execute command: " + commandName);
            console.log(command);
        });
    };
});

shopApp.controller('CustomerCtrl', ['$scope',
    function($scope) {
    }
]);

shopApp.controller('ProductCtrl', ['$scope', '$http',
  function ($scope) {
  }]);

shopApp.controller('OrderCtrl', ['$scope', '$http', 'debounce',
    function($scope, $http, debounce) {
        $scope.message = "This is a order thingy";
        $scope.order = {
            Items: []
        };
        $scope.addItem = function() {
            $scope.order.Items.push({});
        };

        var searchForRecommendations = function () {
            var productIds = $scope.order.Items.map(function(item) {
                return item.ProductId;
            });
            var query = { ProductIds: productIds };
            $http.post(shopApp.baseUrl + "recommendation", query).then(function(response) {
                $scope.recommendations = response.data;
            });
        };

        var getPreviousOrders = function(customerId) {
            $http.get(shopApp.baseUrl + "customers/getPreviousOrders?query=" + customerId).then(function(response) {
                $scope.order.previousOrders = response.data;
            });
        };

        var lazyGetCustomers = debounce(function (filter) {
            return $http.get(shopApp.baseUrl + "customers/get?query=" + filter).then(function(response) {
                return response.data;
            });
        }, 400);
        $scope.getCustomers = function(filter) {
            return lazyGetCustomers(filter);
        };
        $scope.customerSelected = function(order, $item) {
            order.CustomerId = $item.id;
            getPreviousOrders(order.CustomerId);
        };

        var lazyGetProducts = debounce(function (filter) {
            return $http.get(shopApp.baseUrl + "products/get?query=" + filter).then(function (response) {
                return response.data;
            });
        }, 400);
        $scope.getProducts = function (filter) {
            return lazyGetProducts(filter);
        };
        $scope.productSelected = function (item, $item) {
            item.ProductId = $item.Id;
            searchForRecommendations();
        };

        $scope.deleteItem = function (item) {
            var index = $scope.order.Items.indexOf(item);
            if (index > -1) {
                $scope.order.Items.splice(index, 1);
            }
            searchForRecommendations();
        };
    }
]);

shopApp.factory('debounce', ['$timeout', '$q', function ($timeout, $q) {
    // The service is actually this function, which we call with the func
    // that should be debounced and how long to wait in between calls
    return function debounce(func, wait, immediate) {
        var timeout;
        // Create a deferred object that will be resolved when we need to
        // actually call the func
        var deferred = $q.defer();
        return function () {
            var context = this, args = arguments;
            var later = function () {
                timeout = null;
                if (!immediate) {
                    deferred.resolve(func.apply(context, args));
                    deferred = $q.defer();
                }
            };
            var callNow = immediate && !timeout;
            if (timeout) {
                $timeout.cancel(timeout);
            }
            timeout = $timeout(later, wait);
            if (callNow) {
                deferred.resolve(func.apply(context, args));
                deferred = $q.defer();
            }
            return deferred.promise;
        };
    };
}]);

shopApp.controller('ESPollingController', function ($scope, $http) {
    $scope.events = [];
    var Base64 = { _keyStr: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=", encode: function (e) { var t = ""; var n, r, i, s, o, u, a; var f = 0; e = Base64._utf8_encode(e); while (f < e.length) { n = e.charCodeAt(f++); r = e.charCodeAt(f++); i = e.charCodeAt(f++); s = n >> 2; o = (n & 3) << 4 | r >> 4; u = (r & 15) << 2 | i >> 6; a = i & 63; if (isNaN(r)) { u = a = 64 } else if (isNaN(i)) { a = 64 } t = t + this._keyStr.charAt(s) + this._keyStr.charAt(o) + this._keyStr.charAt(u) + this._keyStr.charAt(a) } return t }, decode: function (e) { var t = ""; var n, r, i; var s, o, u, a; var f = 0; e = e.replace(/[^A-Za-z0-9\+\/\=]/g, ""); while (f < e.length) { s = this._keyStr.indexOf(e.charAt(f++)); o = this._keyStr.indexOf(e.charAt(f++)); u = this._keyStr.indexOf(e.charAt(f++)); a = this._keyStr.indexOf(e.charAt(f++)); n = s << 2 | o >> 4; r = (o & 15) << 4 | u >> 2; i = (u & 3) << 6 | a; t = t + String.fromCharCode(n); if (u != 64) { t = t + String.fromCharCode(r) } if (a != 64) { t = t + String.fromCharCode(i) } } t = Base64._utf8_decode(t); return t }, _utf8_encode: function (e) { e = e.replace(/\r\n/g, "\n"); var t = ""; for (var n = 0; n < e.length; n++) { var r = e.charCodeAt(n); if (r < 128) { t += String.fromCharCode(r) } else if (r > 127 && r < 2048) { t += String.fromCharCode(r >> 6 | 192); t += String.fromCharCode(r & 63 | 128) } else { t += String.fromCharCode(r >> 12 | 224); t += String.fromCharCode(r >> 6 & 63 | 128); t += String.fromCharCode(r & 63 | 128) } } return t }, _utf8_decode: function (e) { var t = ""; var n = 0; var r = c1 = c2 = 0; while (n < e.length) { r = e.charCodeAt(n); if (r < 128) { t += String.fromCharCode(r); n++ } else if (r > 191 && r < 224) { c2 = e.charCodeAt(n + 1); t += String.fromCharCode((r & 31) << 6 | c2 & 63); n += 2 } else { c2 = e.charCodeAt(n + 1); c3 = e.charCodeAt(n + 2); t += String.fromCharCode((r & 15) << 12 | (c2 & 63) << 6 | c3 & 63); n += 3 } } return t } };

    function getEvents(stream) {
        var currentLink = stream;
        var config = {
            headers: {
                'Accept': 'application/json',
                'Authorization': 'Basic ' + Base64.encode('admin' + ':' + 'changeit'),
                "ES-LongPoll": 15
            }
        };
        $http
            .get(stream + "?embed=body", config)
            .success(function (data) {
                var events = data.entries.map(function (e) {
                    return JSON.parse(e.data);
                });
                events.reverse().forEach(function (e) {
                    $scope.events.unshift(e);
                });
                var previousLink = data.links.filter(function (l) {
                    return l.relation === "previous";
                });
                currentLink = stream;
                if (previousLink.length > 0) {
                    currentLink = previousLink[0].uri;
                }
                getEvents(currentLink);
            })
        .error(function () {
            getEvents(currentLink);
        });
    }

    getEvents("http://localhost:2113/" + "streams/%24ce-BusyShopCQRS");
});
