/// <reference path="../backbone.js" />
/// <reference path="../jquery-1.7.1.js" />
/// <reference path="../require-jquery.js" />

var Category = Backbone.Model.extend({
    url: 'api/category/',
    idAttribute: 'categoryName'
});