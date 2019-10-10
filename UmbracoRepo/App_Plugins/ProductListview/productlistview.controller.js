//creates the actual controller, name and function included. Called in layout file (html)
angular.module("umbraco").controller("My.ListView.ProductLayoutController", 
    function ($scope, listViewHelper, $location, mediaResource, mediaHelper) {

        "use strict";

        var vm = this;
        
        vm.toggleItem = toggleItem;
        vm.clickItem = clickItem;

        //init the controller
        function activate() {
            
            //Load background image for each item
            angular.forEach($scope.items, function (item) {
                getBackgroundImage(item);
            });
        
        }

        //Load background image
        function getBackgroundImage(item) {
            mediaResource.getById(item.photos)
                .then(function (media) {
                    //find the image thumbnail
                    item.imageThumbnail = mediaHelper.resolveFile(media, true);
                });
        }

        // Toggle if item is selected
        function toggleItem(item) {

            //use the list view helper to select the item
            if (item.selected) {
                listViewHelper.deselectItem(item, $scope.selection);
            } else {
                listViewHelper.selectItem(item, $scope.selection);
            }
        }

        //Item click handler
            function clickItem(item) {
                $location.url(item.editPath);
            }


        activate();

    })