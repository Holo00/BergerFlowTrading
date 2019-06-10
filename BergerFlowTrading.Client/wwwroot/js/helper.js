var wasmHelper = {};

wasmHelper.setLocalStorage = function (key, value) {
    localStorage.setItem(key, value);
};

wasmHelper.getLocalStorage = function (key) {
    return localStorage.getItem(key);
};

wasmHelper.removeLocalStorage = function (key) {
    return localStorage.removeItem(key);
};
