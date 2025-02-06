export function setLocalStorage(key, value) {
    window.localStorage.setItem(key, value);
}

export function getLocalStorage(key) {
    return window.localStorage.getItem(key) || null;
}

export function removeLocalStorage(key) {
    window.localStorage.removeItem(key);
}

export function clearLocalStorage() {
    window.localStorage.clear();
}


export function alertTest(test) {
    window.alert(test);
}