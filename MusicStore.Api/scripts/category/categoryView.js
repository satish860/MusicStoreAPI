var CategoryView = Backbone.View.extend({
    tagName:'li',
    render: function () {
        this.$el.html(_.template(categoryTemplate,this.model.toJSON()));
        return this;
    }
});
