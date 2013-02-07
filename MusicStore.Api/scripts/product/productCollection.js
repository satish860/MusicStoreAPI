/// <reference path="../jquery-1.8.0.js" />
/// <reference path="../backbone.js" />

var ProductCollection = Backbone.Collection.extend({
    url:'api/products/getAll',
    model:Product
});