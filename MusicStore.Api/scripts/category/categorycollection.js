﻿/// <reference path="../backbone.js" />
/// <reference path="../jquery-1.7.1.js" />
/// <reference path="../require-jquery.js" />
var CategoryCollection = Backbone.Collection.extend({
    url: 'api/category',
    model: Category
});
