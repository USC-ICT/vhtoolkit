mergeInto(LibraryManager.library, {
    $RideLinkHandler: {
        ptrToOffset: function (ptr) {
            return typeof ptr === "bigint" ? Number(ptr) : ptr;
        },

        utf8ToString: function (ptr) {
            return UTF8ToString(RideLinkHandler.ptrToOffset(ptr));
        }
    },

    OpenWindow: function (url) {
        window.open(RideLinkHandler.utf8ToString(url), "_blank");
    }
});

autoAddDeps(LibraryManager.library, "$RideLinkHandler");
