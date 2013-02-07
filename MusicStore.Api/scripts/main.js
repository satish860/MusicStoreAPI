/// <reference path="require-jquery.js" />

requirejs.config({
    'paths': {
        "backbone":"backbone",
        "underscore":"underscore-min",
        "app":"app/app",
        "itemCollection":"collection/itemcollection",
        "categoryCollection":"collection/categorycollection",
        "category":"model/category",
        "item":"model/item"
    },
    'shim': {
        backbone: {
            'deps': ['jquery', 'underscore'],
            'exports':'Backbone'
        },
        underscore: {
            'exports':'_'
        }
    }

});

require(['jquery', 'backbone', 'app'],
    function ($, Backbone, app) {
        app.init();
    }); 