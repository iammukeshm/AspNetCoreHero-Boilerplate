$("#ProductCategoryId").select2({
    placeholder: "Select a Category",
    theme: "bootstrap4",
    escapeMarkup: function (m) {
        return m;
    }
});
