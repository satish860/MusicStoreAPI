/// <reference path="../jquery-1.8.0.js" />
/// <reference path="../backbone.js" />
/// <reference path="../underscore-min.js" />

var ProductView = Backbone.View.extend({
    render: function() {
        alert(JSON.stringify(this.model));
        this.$el.html(_.template(productTemplate,this.model));
        return this;
    }
});